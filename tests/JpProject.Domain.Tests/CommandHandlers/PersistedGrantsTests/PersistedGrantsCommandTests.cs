using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using JpProject.Domain.Tests.CommandHandlers.PersistedGrantsTests.Fakers;
using Moq;
using Xunit;

namespace JpProject.Domain.Tests.CommandHandlers.PersistedGrantsTests
{
    public class PersistedGrantsCommandTests
    {
        private Faker _faker;
        private readonly CancellationTokenSource _tokenSource;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly PersistedGrantCommandHandler _commandHandler;
        private readonly Mock<IPersistedGrantRepository> _persistedGrantRepository;

        public PersistedGrantsCommandTests()
        {
            _faker = new Faker();
            _tokenSource = new CancellationTokenSource();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _persistedGrantRepository = new Mock<IPersistedGrantRepository>();
            _commandHandler = new PersistedGrantCommandHandler(_uow.Object, _mediator.Object, _notifications.Object, _persistedGrantRepository.Object);
        }

        [Fact]
        public async Task ShouldRemovePersistedGrant()
        {
            var command = PersistedGrantCommandFaker.GenerateRemoveCommand().Generate();
            _persistedGrantRepository.Setup(s => s.Remove(command.Key));

            _uow.Setup(v => v.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _uow.Verify(v => v.Commit(), Times.Once);

            result.Should().BeTrue();
        }


        [Fact]
        public async Task ShouldNotRemovePersistedGrantWhenKeyIsNull()
        {
            var command = new RemovePersistedGrantCommand(null);


            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            result.Should().BeFalse();
        }


        [Fact]
        public async Task ShouldNotRemovePersistedGrantWhenKeyDoesntExist()
        {
            var command = PersistedGrantCommandFaker.GenerateRemoveCommand().Generate();
            _persistedGrantRepository.Setup(s => s.Remove(command.Key));

            _uow.Setup(v => v.Commit()).Returns(false);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _uow.Verify(v => v.Commit(), Times.Once);

            result.Should().BeFalse();
        }
    }
}
