using Domain;
using System.Data;
using System;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.UseCases.CreateWish;
using Application.Repositories;

namespace Tests {
    public class CreateWishUseCaseTest {

        private IUseCase<CreateWishInput, CreateWishOutput> _useCase;

        private Mock<IUserRepository> _userRepositoryMock;

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

            // сказать, что репозиторий будет содержать одного пользователя с пустым вишлистом
            _userRepositoryMock
                .Setup(r => r.Get(_userGuid))
                .Returns(() => _user);

            _useCase = new CreateWishUseCase(_userRepositoryMock.Object);
        }

        [Test]
        public void WishHasTitleAndUserExists_ShouldReturnSuccesEqualsTrueInOutput() {
            
            // создать Input 
            var input = new CreateWishInput() {
                UserId = _userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };

            // добавить wish через useCase 
            var output = _useCase.Execute(input);

            // проверить, что output.Success == true
            Assert.IsTrue(output.Success);

            // проверить, что был вызван метод Add в репозитории с заданым юзером в качестве параметра
            _userRepositoryMock.Verify(r => r.Get(_userGuid));
        }


        [Test]
        public void InputWithoutTitle_ShouldThrowArgumentException() {
            // arrange
            // создать Input 
            var input = new CreateWishInput() {
                UserId = _userGuid,
                WishTitle = null,
                WishUrl = "https://ozon.ru/..."
            };

            // act, assert
            Assert.That(() => _useCase.Execute(input), Throws.ArgumentNullException);
        }

        [Test]
        public void UserDoesNotExists_ShouldThrowNotFoundException() {

            _userRepositoryMock
                .Setup((r) => r.Get(_userGuid))
                .Returns(() => null);

            var input = new CreateWishInput() {
                UserId = _userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };

            Assert.That(() => _useCase.Execute(input), Throws.InstanceOf<RowNotInTableException>());
        }
    }
}