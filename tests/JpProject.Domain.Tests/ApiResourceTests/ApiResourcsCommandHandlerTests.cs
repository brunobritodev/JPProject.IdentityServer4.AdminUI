using Bogus;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using JpProject.Domain.Tests.ApiResourceTests.Fakers;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JpProject.Domain.Tests.ApiResourceTests
{
    public class ApiResourcsCommandHandlerTests
    {
        private readonly ApiResourceCommandHandler _commandHandler;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IApiResourceRepository> _apiResourceRepository;
        private readonly Mock<IApiScopeRepository> _apiScopeRepository;
        private readonly Mock<IApiSecretRepository> _apiSecretRepository;
        private readonly CancellationTokenSource _tokenSource;
        private readonly Faker _faker;
        public ApiResourcsCommandHandlerTests()
        {
            _faker = new Faker();
            _tokenSource = new CancellationTokenSource();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _apiResourceRepository = new Mock<IApiResourceRepository>();
            _apiSecretRepository = new Mock<IApiSecretRepository>();
            _apiScopeRepository = new Mock<IApiScopeRepository>();
            _commandHandler = new ApiResourceCommandHandler(_uow.Object, _mediator.Object, _notifications.Object, _apiResourceRepository.Object, _apiSecretRepository.Object, _apiScopeRepository.Object);
        }


        [Fact]
        public async Task ShouldNotSaveResourceWhenItAlreadyExist()
        {
            var command = ResourceCommandFaker.GenerateRegisterApiResourceCommand().Generate();

            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name))).ReturnsAsync(EntityResourceFaker.GenerateResource().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name)), Times.Once);
            _uow.Verify(v => v.Commit(), Times.Never);

        }

        [Fact]
        public async Task ShouldSaveResource()
        {
            var command = ResourceCommandFaker.GenerateRegisterApiResourceCommand().Generate();
            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name))).ReturnsAsync((ApiResource)null);
            _apiResourceRepository.Setup(s => s.Add(It.Is<ApiResource>(i => i.Name == command.Resource.Name)));
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _apiResourceRepository.Verify(s => s.Add(It.IsAny<ApiResource>()), Times.Once);
            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name)), Times.Once);

            Assert.True(result);
        }


        [Fact]
        public async Task ShouldNotUpdateResourceWhenItDoesntExist()
        {
            var command = ResourceCommandFaker.GenerateUpdateApiResourceCommand().Generate();

            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == command.OldResourceName))).ReturnsAsync(EntityResourceFaker.GenerateResource().Generate());


            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == command.OldResourceName)), Times.Once);
        }


        [Fact]
        public async Task ShouldNotUpdateResourceWhenNameIsntProvided()
        {
            var command = ResourceCommandFaker.GenerateUpdateApiResourceCommand().Generate();
            command.Resource.Name = null;

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _uow.Verify(v => v.Commit(), Times.Never);

        }

        [Fact]
        public async Task ShouldUpdateResource()
        {
            var oldResourceName = "old-resource-name";
            var command = ResourceCommandFaker.GenerateUpdateApiResourceCommand(oldResourceName: oldResourceName).Generate();
            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == oldResourceName))).ReturnsAsync(EntityResourceFaker.GenerateResource().Generate());
            _apiResourceRepository.Setup(s => s.UpdateWithChildrens(It.Is<ApiResource>(i => i.Name == command.Resource.Name))).Returns(Task.CompletedTask);
            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _apiResourceRepository.Verify(s => s.UpdateWithChildrens(It.IsAny<ApiResource>()), Times.Once);
            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == oldResourceName)), Times.Once);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldNotRemoveResourceWhenNameIsntProvided()
        {
            var command = new RemoveApiResourceCommand(null);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            Assert.False(result);
            _uow.Verify(v => v.Commit(), Times.Never);

        }


        [Fact]
        public async Task ShouldNotRemoveResourceWhenItDoesntExist()
        {

            var command = ResourceCommandFaker.GenerateUpdateApiResourceCommand().Generate();

            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == command.OldResourceName))).ReturnsAsync((ApiResource)null);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
            _uow.Verify(v => v.Commit(), Times.Never);
            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == command.OldResourceName)), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveResource()
        {
            var command = ResourceCommandFaker.GenerateRemoveApiResourceCommand().Generate();
            _apiResourceRepository.Setup(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name))).ReturnsAsync(EntityResourceFaker.GenerateResource().Generate());
            _apiResourceRepository.Setup(s => s.Remove(It.IsAny<int>()));

            _uow.Setup(s => s.Commit()).Returns(true);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _apiResourceRepository.Verify(s => s.GetResource(It.Is<string>(q => q == command.Resource.Name)), Times.Once);
            _apiResourceRepository.Verify(s => s.Remove(It.IsAny<int>()), Times.Once);

            Assert.True(result);
        }
    }
}
