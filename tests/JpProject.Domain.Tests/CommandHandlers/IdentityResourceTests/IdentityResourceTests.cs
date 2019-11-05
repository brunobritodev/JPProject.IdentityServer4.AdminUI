using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using JpProject.Domain.Tests.CommandHandlers.IdentityResourceTests.Fakers;
using Moq;
using Xunit;

namespace JpProject.Domain.Tests.CommandHandlers.IdentityResourceTests
{
    public class IdentityResourceTests
    {
        private Faker _faker;
        private readonly CancellationTokenSource _tokenSource;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly IdentityResourceCommandHandler _commandHandler;
        private readonly Mock<IIdentityResourceRepository> _identityResourceRepository;

        public IdentityResourceTests()
        {
            _faker = new Faker();
            _tokenSource = new CancellationTokenSource();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _identityResourceRepository = new Mock<IIdentityResourceRepository>();
            _commandHandler = new IdentityResourceCommandHandler(_uow.Object, _mediator.Object, _notifications.Object, _identityResourceRepository.Object);
        }

        [Fact]
        public async Task ShouldAddIdentityResource()
        {
            var command = IdentityResourceCommandFaker.GenerateRegisterCommand().Generate();
            _identityResourceRepository.Setup(s => s.GetByName(It.Is<string>(q => q == command.Resource.Name))).ReturnsAsync((IdentityResource)null);
            _identityResourceRepository.Setup(s => s.Add(It.Is<IdentityResource>(i => i.Name == command.Resource.Name)));
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _identityResourceRepository.Verify(s => s.Add(It.IsAny<IdentityResource>()), Times.Once);
            _identityResourceRepository.Verify(s => s.GetByName(It.Is<string>(q => q == command.Resource.Name)), Times.Once);

            result.Should().BeTrue();

        }

        [Fact]
        public async Task ShouldNotUpdateResourceWhenItDoesntExist()
        {
            var command = IdentityResourceCommandFaker.GenerateUpdateCommand().Generate();

            _identityResourceRepository.Setup(s => s.GetByName(It.Is<string>(q => q == command.OldIdentityResourceName))).ReturnsAsync(EntityIdentityResourceFaker.GenerateEntity().Generate());


            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            result.Should().BeFalse();
            _identityResourceRepository.Verify(s => s.GetByName(It.Is<string>(q => q == command.OldIdentityResourceName)), Times.Once);
        }


        [Fact]
        public async Task ShouldNotUpdateResourceWhenNameIsntProvided()
        {
            var command = IdentityResourceCommandFaker.GenerateUpdateCommand().Generate();
            command.Resource.Name = null;

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            result.Should().BeFalse();
            _uow.Verify(v => v.Commit(), Times.Never);

        }

        [Fact]
        public async Task ShouldUpdateResource()
        {
            var oldResourceName = "old-resource-name";
            var command = IdentityResourceCommandFaker.GenerateUpdateCommand(oldIdentityResourceName:  oldResourceName).Generate();
            _identityResourceRepository.Setup(s => s.GetByName(It.Is<string>(q => q == oldResourceName))).ReturnsAsync(EntityIdentityResourceFaker.GenerateEntity().Generate());
            _identityResourceRepository.Setup(s => s.UpdateWithChildrens(It.Is<IdentityResource>(i => i.Name == command.Resource.Name))).Returns(Task.CompletedTask);
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _identityResourceRepository.Verify(s => s.UpdateWithChildrens(It.IsAny<IdentityResource>()), Times.Once);
            _identityResourceRepository.Verify(s => s.GetByName(It.Is<string>(q => q == oldResourceName)), Times.Once);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldNotRemoveResourceWhenNameIsntProvided()
        {
            var command = new RemoveIdentityResourceCommand(null);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            result.Should().BeFalse();
            _uow.Verify(v => v.Commit(), Times.Never);

        }


        [Fact]
        public async Task ShouldNotRemoveResourceWhenItDoesntExist()
        {

            var command = IdentityResourceCommandFaker.GenerateUpdateCommand().Generate();

            _identityResourceRepository.Setup(s => s.GetByName(It.Is<string>(q => q == command.OldIdentityResourceName))).ReturnsAsync((IdentityResource)null);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            result.Should().BeFalse();
            _uow.Verify(v => v.Commit(), Times.Never);
            _identityResourceRepository.Verify(s => s.GetByName(It.Is<string>(q => q == command.OldIdentityResourceName)), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveResource()
        {
            var command = IdentityResourceCommandFaker.GenerateRemoveCommand().Generate();
            _identityResourceRepository.Setup(s => s.GetByName(It.Is<string>(q => q == command.Resource.Name))).ReturnsAsync(EntityIdentityResourceFaker.GenerateEntity().Generate());
            _identityResourceRepository.Setup(s => s.Remove(It.IsAny<int>()));

            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _identityResourceRepository.Verify(s => s.GetByName(It.Is<string>(q => q == command.Resource.Name)), Times.Once);
            _identityResourceRepository.Verify(s => s.Remove(It.IsAny<int>()), Times.Once);

            result.Should().BeTrue();
        }
    }
}
