using Bogus;
using Jp.Domain.CommandHandlers;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using JpProject.Domain.Tests.UserTests.Fakers;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JpProject.Domain.Tests.UserTests
{
    public class UserCommandHandlerTests
    {
        private Faker _faker;
        private CancellationTokenSource _tokenSource;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private UserCommandHandler _commandHandler;
        private readonly Mock<IUserService> _userService;

        public UserCommandHandlerTests()
        {
            _faker = new Faker();
            _tokenSource = new CancellationTokenSource();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _userService = new Mock<IUserService>();
            _commandHandler = new UserCommandHandler(_uow.Object, _mediator.Object, _notifications.Object, _userService.Object);

        }

        [Fact]
        public async Task ShouldNotAddNewUser_AfterSuccessfulLoginThrough_ExternalProvider_IfHisEmailAlreadyExist()
        {
            var command = UserCommandFaker.GenerateRegisterNewUserWithoutPassCommand().Generate();

            _userService.Setup(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email))).ReturnsAsync(UserFaker.GenerateUser().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            _userService.Verify(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email)), Times.Once);
            Assert.False(result);
        }


        [Fact]
        public async Task ShouldNotAddNewUser_AfterSuccessfulLoginThrough_ExternalProvider_IfHisNameAlreadyExist()
        {
            var command = UserCommandFaker.GenerateRegisterNewUserWithoutPassCommand().Generate();

            _userService.Setup(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email))).ReturnsAsync((User)null);
            _userService.Setup(s => s.FindByNameAsync(It.Is<string>(n => n == command.Username))).ReturnsAsync(UserFaker.GenerateUser().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            _userService.Verify(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email)), Times.Once);
            _userService.Verify(s => s.FindByNameAsync(It.Is<string>(n => n == command.Username)), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotAddLoginIfUserDoesntExist()
        {
            var command = UserCommandFaker.GenerateAddLoginCommand().Generate();

            _userService.Setup(s => s.AddLoginAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Guid?)null);

            var result = await _commandHandler.Handle(command, _tokenSource.Token);

            _userService.Verify(s => s.AddLoginAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(result);
        }


        [Fact]
        public async Task ShouldNotAddNewUserIfItEmailAlreadyExist()
        {
            var command = UserCommandFaker.GenerateRegisterNewUserCommand().Generate();

            _userService.Setup(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email))).ReturnsAsync(UserFaker.GenerateUser().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            _userService.Verify(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email)), Times.Once);
            Assert.False(result);
        }


        [Fact]
        public async Task ShouldNotAddNewUserIfItNameAlreadyExist()
        {
            var command = UserCommandFaker.GenerateRegisterNewUserCommand().Generate();

            _userService.Setup(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email))).ReturnsAsync((User)null);
            _userService.Setup(s => s.FindByNameAsync(It.Is<string>(n => n == command.Username))).ReturnsAsync(UserFaker.GenerateUser().Generate());

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            _userService.Verify(s => s.FindByEmailAsync(It.Is<string>(e => e == command.Email)), Times.Once);
            _userService.Verify(s => s.FindByNameAsync(It.Is<string>(n => n == command.Username)), Times.Once);
            Assert.False(result);
        }


        [Fact]
        public async Task ShouldNotAddNewUserIfPasswordDifferFromConfirmation()
        {
            var command = UserCommandFaker.GenerateRegisterNewUserCommand(_faker.Internet.Password()).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
        }

        [Fact]
        public async Task ShouldNotGenerateResetLinkIfNotProvideUsername()
        {
            var command = UserCommandFaker.GenerateSendResetLinkCommand(email: string.Empty).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
        }
        [Fact]
        public async Task ShouldNotGenerateResetLinkIfNotProvideEmail()
        {
            var command = UserCommandFaker.GenerateSendResetLinkCommand(email: string.Empty).Generate();

            var result = await _commandHandler.Handle(command, _tokenSource.Token);


            Assert.False(result);
        }
    }
}
