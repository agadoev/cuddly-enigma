using System;
using Domain;
using NUnit.Framework;
using Application.UseCases;
using Application.Repositories;
using Moq;

/**
    Лондонский подход
*/

namespace Tests.UnitTests {
    public class RegisterUserHandlerTests {
        private ICommandHandler<RegisterUserCommand> _handler;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp() {
            _userRepository = new Mock<IUserRepository>();

            _handler = new RegisterUserHandler(_userRepository.Object);
        }


        [Test]
        public void ShouldSetUserId() {

            // arrange
            var command = new RegisterUserCommand() {
                Username = "sdfsdf",
                Id = null
            };

            // act
            _handler.Execute(command);

            // assert
            Assert.That(command.Id, Is.Not.Empty);
        }


        [Test]
        public void UsernameEmpty_ShouldThrowArgumentNullException() {
            
            // arrange
            var command = new RegisterUserCommand() {
                Username = null,
                Id = null
            };

            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void ShouldCallMethodAddOfUserRepository() {
            // arrange
            var command = new RegisterUserCommand() {
                Username = "sdfsdf",
                Id = null
            };

            // act
            _handler.Execute(command);

            // assert
            _userRepository.Verify((r) => r.Add(It.IsAny<User>()));
        }
    }
}