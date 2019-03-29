using IdentityServer4.Models;
using Jp.Domain.CommandHandlers;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using JpProject.Domain.Tests.ClientTests.Fakers;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Client = IdentityServer4.EntityFramework.Entities.Client;

namespace JpProject.Domain.Tests.ClientTests
{
    public class ClientCommandHandlerTests
    {
        private ClientCommandHandler _commandHandler;
        private Mock<DomainNotificationHandler> _notifications;
        private Mock<IMediatorHandler> _mediator;
        private Mock<IUnitOfWork> _uow;
        private Mock<IClientRepository> _clientRepository;
        private Mock<IClientClaimRepository> _clientClaimRepository;
        private Mock<IClientPropertyRepository> _clientPropertyRepository;
        private Mock<IClientSecretRepository> _clientSecretRepository;
        private CancellationTokenSource _tokenSource;

        public ClientCommandHandlerTests()
        {
            _tokenSource = new CancellationTokenSource();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _clientRepository = new Mock<IClientRepository>();
            _clientClaimRepository = new Mock<IClientClaimRepository>();
            _clientPropertyRepository = new Mock<IClientPropertyRepository>();
            _clientSecretRepository = new Mock<IClientSecretRepository>();
            _commandHandler = new ClientCommandHandler(_uow.Object, _mediator.Object, _notifications.Object, _clientRepository.Object, _clientSecretRepository.Object, _clientPropertyRepository.Object, _clientClaimRepository.Object);

        }

        [Fact]
        public async Task ShouldNotAddDuplicatedClientId()
        {
            var command = ClientCommandFaker.GenerateSaveClientCommand().Generate();
            _clientRepository.Setup(s =>
                                    s.GetByClientId(It.Is<string>(clientId => clientId.Equals(command.Client.ClientId))))
                                     .ReturnsAsync(EntityClientFaker.GenerateClient().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldClientHaveName()
        {
            var command = ClientCommandFaker.GenerateSaveClientCommand().Generate();
            command.Client.ClientName = string.Empty;

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }


        [Fact]
        public async Task ShouldClientHaveValidId()
        {
            var command = ClientCommandFaker.GenerateSaveClientCommand().Generate();
            command.Client.ClientId = string.Empty;

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }


        [Fact]
        public async Task ShouldNotCopySecrets()
        {
            var command = ClientCommandFaker.GenerateCopyClientCommand().Generate();

            var copyClient = EntityClientFaker.GenerateClient().Generate();

            _clientRepository.Setup(s =>
                    s.GetByClientId(It.Is<string>(clientId => clientId.Equals(command.Client.ClientId))))
                    .ReturnsAsync(copyClient);

            _clientRepository.Setup(s => s.Add(It.Is<Client>(c => !c.ClientSecrets.Any())));

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }


        [Theory]
        [InlineData(GrantType.Implicit, GrantType.AuthorizationCode)]
        [InlineData(GrantType.Implicit, GrantType.Hybrid)]
        [InlineData(GrantType.AuthorizationCode, GrantType.Hybrid)]
        public void ShouldNotAllowCombinationOfGrants(string a, string b)
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand().Generate();

            Assert.Throws<InvalidOperationException>(() => command.Client.AllowedGrantTypes = new List<string>() { a, b });
        }


    }
}
