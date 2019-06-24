using Bogus;
using IdentityServer4.EntityFramework.Entities;
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
        private readonly ClientCommandHandler _commandHandler;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IClientClaimRepository> _clientClaimRepository;
        private readonly Mock<IClientPropertyRepository> _clientPropertyRepository;
        private readonly Mock<IClientSecretRepository> _clientSecretRepository;
        private readonly CancellationTokenSource _tokenSource;
        private readonly Faker _faker;

        public ClientCommandHandlerTests()
        {
            _faker = new Faker();
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
            _uow.Verify(v => v.Commit(), Times.Never);
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

        [Fact]
        public async Task ShouldNotAcceptNegativeAbsoluteRefreshTokenLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(absoluteRefreshTokenLifetime: _faker.Random.Int(max: 0)).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAcceptNegativeIdentityTokenLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(identityTokenLifetime: _faker.Random.Int(max: 0)).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAcceptNegativeAccessTokenLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(accessTokenLifetime: _faker.Random.Int(max: 0)).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAcceptNegativeAuthorizationCodeLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(authorizationCodeLifetime: _faker.Random.Int(max: 0)).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAcceptNegativeSlidingRefreshTokenLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(slidingRefreshTokenLifetime: _faker.Random.Int(max: 0)).Generate();
            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAcceptNegativeDeviceCodeLifetime()
        {
            var command = ClientCommandFaker.GenerateUpdateClientCommand(deviceCodeLifetime: _faker.Random.Int(max: 0)).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
        }

        [Fact]
        public async Task ShouldUpdateClient()
        {
            var oldClientId = "my-old-client-name";
            var command = ClientCommandFaker.GenerateUpdateClientCommand(oldClientId: oldClientId).Generate();
            _clientRepository.Setup(s => s.UpdateWithChildrens(It.Is<Client>(a => a.ClientId == command.Client.ClientId))).Returns(Task.CompletedTask);
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == oldClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            _clientRepository.Verify(s => s.UpdateWithChildrens(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(q => q == oldClientId)), Times.Once);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldRemoveClient()
        {
            var command = ClientCommandFaker.GenerateRemoveClientCommand().Generate();
            _clientRepository.Setup(s => s.Remove(It.Is<Client>(a => a.ClientId == command.Client.ClientId)));
            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(a => a == command.Client.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldNotRemoveSecretWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateRemoveClientSecretCommand().Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _uow.Verify(v => v.Commit(), Times.Never);
        }

        [Fact]
        public async Task ShouldNotRemoveSecretWhenSecretIdIsDifferent()
        {
            var command = ClientCommandFaker.GenerateRemoveClientSecretCommand().Generate();
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _uow.Verify(v => v.Commit(), Times.Never);
        }

        [Fact]
        public async Task ShouldRemoveClientSecret()
        {
            var clientSecret = EntityClientFaker.GenerateClient(clientSecrets: _faker.Random.Int(1, 3)).Generate();
            var command = ClientCommandFaker.GenerateRemoveClientSecretCommand(_faker.PickRandom(clientSecret.ClientSecrets).Id).Generate();

            _uow.Setup(s => s.Commit()).Returns(true);
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(clientSecret);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldNotSaveClientSecretWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateSaveClientSecretCommand().Generate();
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(a => a == command.ClientId)), Times.Once);
            _uow.Verify(v => v.Commit(), Times.Never);
        }

        [Fact]
        public async Task ShouldEncryptedValueBeCorrect()
        {
            var command = ClientCommandFaker.GenerateSaveClientSecretCommand().Generate();
            var valueEncryptedMustBe = command.GetValue();

            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());
            _clientSecretRepository.Setup(s => s.Add(It.Is<ClientSecret>(cs => cs.Value == valueEncryptedMustBe)));
            _uow.Setup(s => s.Commit()).Returns(true);


            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(a => a == command.ClientId)), Times.Once);
            _clientSecretRepository.Verify(s => s.Add(It.Is<ClientSecret>(cs => cs.Value == valueEncryptedMustBe)), Times.Once);
            _uow.Verify(s => s.Commit(), Times.Once);
        }

        [Fact]
        public async Task ShouldNotEncryptedValueBeCorrect()
        {
            var command = ClientCommandFaker.GenerateSaveClientSecretCommand().Generate();
            var valueEncryptedMustBe = command.GetValue();

            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());
            _clientSecretRepository.Setup(s => s.Add(It.Is<ClientSecret>(cs => cs.Value == valueEncryptedMustBe)));
            _uow.Setup(s => s.Commit()).Returns(true);


            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(a => a == command.ClientId)), Times.Once);
            _clientSecretRepository.Verify(s => s.Add(It.Is<ClientSecret>(cs => cs.Value == valueEncryptedMustBe)), Times.Once);
            _uow.Verify(s => s.Commit(), Times.Once);
        }

        [Fact]
        public async Task ShouldNotRemovePropertyWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateRemovePropertyCommand().Generate();


            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
        }


        [Fact]
        public async Task ShouldNotRemovePropertyWhenIdIsDifferent()
        {
            var command = ClientCommandFaker.GenerateRemovePropertyCommand().Generate();
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveProperty()
        {
            var properties = EntityClientFaker.GenerateClient(clientProperties: _faker.Random.Int(1, 3)).Generate();
            var command = ClientCommandFaker.GenerateRemovePropertyCommand(_faker.PickRandom(properties.Properties).Id).Generate();

            _uow.Setup(s => s.Commit()).Returns(true);
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(properties);
            _clientPropertyRepository.Setup(s => s.Remove(It.Is<int>(a => a == command.Id)));

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
            _clientPropertyRepository.Verify(s => s.Remove(It.Is<int>(a => a == command.Id)), Times.Once);
        }


        [Fact]
        public async Task ShouldNotSavePropertyWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateSavePropertyCommand().Generate();
            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId))).ReturnsAsync((Client)null);


            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId)), Times.Once);
        }

        [Fact]
        public async Task ShouldSaveProperty()
        {
            var command = ClientCommandFaker.GenerateSavePropertyCommand().Generate();
            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate()).Verifiable();
            _clientPropertyRepository.Setup(s => s.Add(It.IsAny<ClientProperty>()));

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _clientPropertyRepository.Verify(s => s.Add(It.IsAny<ClientProperty>()), Times.Once);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId)), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotRemoveClaimWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateRemoveClaimCommand().Generate();


            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
        }


        [Fact]
        public async Task ShouldNotRemoveClaimWhenIdIsDifferent()
        {
            var command = ClientCommandFaker.GenerateRemoveClaimCommand().Generate();
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveClaim()
        {
            var properties = EntityClientFaker.GenerateClient(clientClaim: _faker.Random.Int(1, 3)).Generate();
            var command = ClientCommandFaker.GenerateRemoveClaimCommand(_faker.PickRandom(properties.Claims).Id).Generate();

            _uow.Setup(s => s.Commit()).Returns(true);
            _clientRepository.Setup(s => s.GetClient(It.Is<string>(a => a == command.ClientId))).ReturnsAsync(properties);
            _clientClaimRepository.Setup(s => s.Remove(It.Is<int>(a => a == command.Id)));

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.True(result);
            _clientRepository.Verify(s => s.GetClient(It.Is<string>(a => a == command.ClientId)), Times.Once);
            _clientClaimRepository.Verify(s => s.Remove(It.Is<int>(a => a == command.Id)), Times.Once);
        }

        [Fact]
        public async Task ShouldNotSaveClaimWhenClientDoesntExist()
        {
            var command = ClientCommandFaker.GenerateSaveClaimCommand().Generate();
            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId))).ReturnsAsync((Client)null);


            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId)), Times.Once);
        }

        [Fact]
        public async Task ShouldSaveClaim()
        {
            var command = ClientCommandFaker.GenerateSaveClaimCommand().Generate();
            _clientRepository.Setup(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId))).ReturnsAsync(EntityClientFaker.GenerateClient().Generate()).Verifiable();
            _clientClaimRepository.Setup(s => s.Add(It.IsAny<ClientClaim>()));

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _clientClaimRepository.Verify(s => s.Add(It.IsAny<ClientClaim>()), Times.Once);
            _clientRepository.Verify(s => s.GetByClientId(It.Is<string>(q => q == command.ClientId)), Times.Once);
            Assert.False(result);
        }

    }
}
