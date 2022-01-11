using Domain;
using System.Data;
using System;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.Repositories;

/**
    Лондонский подход
    Использование ObjectMother
*/

namespace Tests.UnitTests {


    public class CreateWishCommandObjectMother {


        public Guid GetGuid1() => new Guid("e153e265-cf55-4b89-b917-e4bc24e30754");
        public Guid GetGuid2() => new Guid("e113e265-cf55-4b89-b917-e4bc24e30754");
        public Guid GetGuid3() => new Guid("e163e265-cf55-4b89-b917-e4bc24e30754");

        public CreateWishCommand GetCommandWithoutTitle(Guid userGuid) {
            var command = new CreateWishCommand() {
                UserId = userGuid,
                WishTitle = null,
                WishUrl = "https://ozon.ru/..."
            };

            return command;
        }

        public CreateWishCommand GetCorrectCommand(Guid userGuid) {
            var command = new CreateWishCommand() {
                UserId = userGuid,
                WishTitle = "Подарочек",
                WishUrl = "https://ozon.ru/..."
            };
            
            return command;
        }

    }


    public class CreateWishHandlerTest {

        private ICommandHandler<CreateWishCommand> _handler;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IWishesRepository> _wishRepositoryMock;

        private readonly CreateWishCommandObjectMother _mother = new CreateWishCommandObjectMother();

        // пользователь, который создает Wish
        private readonly User _user = new User() {
            Name = "Alex"
        };
        
        private Guid _userGuid;

        [SetUp]
        public void Setup() {

            _user.Id = _mother.GetGuid1();

            // Мок репозитория
            _userRepositoryMock = new Mock<IUserRepository>();
            _wishRepositoryMock = new Mock<IWishesRepository>();

            // сказать, что репозиторий будет содержать одного пользователя с пустым вишлистом
            _userRepositoryMock
                .Setup(r => r.Get(_user.Id))
                .Returns(() => _user);

            _handler = new CreateWishHandler(_userRepositoryMock.Object, _wishRepositoryMock.Object);
        }

        [Test]
        public void WishHasTitleAndUserExists_ShouldReturnSuccesEqualsTrueInOutput() {
            
            // arrange
            var command = _mother.GetCorrectCommand(_user.Id);

            // добавить wish через useCase 
            _handler.Execute(command);

            // проверить, что output.Success == true
            Assert.IsTrue(command.Success);
        }


        [Test]
        public void InputWithoutTitle_ShouldThrowArgumentException() {
            // arrange
            // создать Input 
            var command = _mother.GetCommandWithoutTitle(_user.Id);

            // act
            Action execute = () => _handler.Execute(command);

            // assert
            Assert.That(execute, Throws.ArgumentNullException);
        }

        [Test]
        public void UserDoesNotExists_ShouldThrowNotFoundException() {

            // arrange
            _userRepositoryMock
                .Setup((r) => r.Get(It.IsAny<Guid>()))
                .Returns(() => null);

            var command = _mother.GetCorrectCommand(_userGuid);

            // act
            Action execute = () => _handler.Execute(command);


            // assert
            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<RowNotInTableException>());
        }


        [Test]
        public void CommandCorrect_ShouldExecuteAddMethodOfWishRepository() {
            // arrange
            var command = _mother.GetCorrectCommand(_user.Id);


            // act
            _handler.Execute(command);


            // assert
            _wishRepositoryMock
                .Verify(r => r.Add(It.IsAny<Wish>()), Times.Once);
        }

        [Test]
        public void ShouldReturnGeneratedGuidInCommand() {
            var command = _mother.GetCorrectCommand(_user.Id);

            _handler.Execute(command);

            Assert.That(command.CreatedWishId, Is.InstanceOf<Guid>());

        }
    }
}