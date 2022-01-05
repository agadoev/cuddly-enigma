using Domain;
using System.Data;
using System;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.Repositories;

namespace Tests {
    public class CreateWishUseCaseTest {

        private ICommandHandler<CreateWishCommand> _handler;

        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IWishesRepository> _wishRepositoryMock;

        private User _user;

        private Guid _userGuid;

        [SetUp]
        public void Setup() {
            // создаем пользователя
            _userGuid = new Guid("e153e265-cf55-4b89-b917-e4bc24e30754");
            _user = new User() {
                Name = "Alex"
            };

            // Мок репозитория
            _userRepositoryMock = new Mock<IUserRepository>();
            _wishRepositoryMock = new Mock<IWishesRepository>();

            // сказать, что репозиторий будет содержать одного пользователя с пустым вишлистом
            _userRepositoryMock
                .Setup(r => r.Get(_userGuid))
                .Returns(() => _user);

            _handler = new CreateWishHandler(_userRepositoryMock.Object, _wishRepositoryMock.Object);
        }

        [Test]
        public void WishHasTitleAndUserExists_ShouldReturnSuccesEqualsTrueInOutput() {
            
            var command = new CreateWishCommand() {
                UserId = _userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };

            // добавить wish через useCase 
            var output = _handler.Execute(command);

            // проверить, что output.Success == true
            Assert.IsTrue(output.Success);

            // проверить, что был вызван метод Add в репозитории с заданым юзером в качестве параметра
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
        }


        [Test]
        public void InputWithoutTitle_ShouldThrowArgumentException() {
            // arrange
            // создать Input 
            var command = new CreateWishCommand() {
                UserId = _userGuid,
                WishTitle = null,
                WishUrl = "https://ozon.ru/..."
            };

            // act, assert
            Assert.That(() => _handler.Execute(command), Throws.ArgumentNullException);
        }

        [Test]
        public void UserDoesNotExists_ShouldThrowNotFoundException() {

            _userRepositoryMock
                .Setup((r) => r.Get(_userGuid))
                .Returns(() => null);

            var input = new CreateWishCommand() {
                UserId = _userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };

            Assert.That(() => _handler.Execute(input), Throws.InstanceOf<RowNotInTableException>());
        }


        [Test]
        public void CommandCorrect_ShouldExecuteAddMethodOfWishRepository() {
            var command = new CreateWishCommand() {
                UserId = _userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };

            _handler.Execute(command);

            _wishRepositoryMock
                .Verify(r => r.Add(It.IsAny<Wish>()), Times.Once);
        }
    }
}