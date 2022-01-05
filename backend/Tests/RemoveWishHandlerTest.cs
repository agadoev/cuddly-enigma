using Domain;
using System;
using System.Data;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.Repositories;

namespace Tests {
    public class RemoveWishHandlerTest {

        private Mock<IWishesRepository> _wishesRepository;
        private Mock<IUserRepository> _usersRepository;
        private ICommandHandler<RemoveWishCommand> _handler;


        [SetUp]
        public void SetUp() {
            _wishesRepository = new Mock<IWishesRepository>();
            _usersRepository = new Mock<IUserRepository>();

            _handler = new RemoveWishHandler(_usersRepository.Object, _wishesRepository.Object);
        }


        [Test]
        public void UserIdDoesNotMatch_ShouldThrowException() {

            // arrange
            var user1Id = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var user2Id = new Guid("2c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var wishId = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");

            var command = new RemoveWishCommand() {
                UserId = user1Id,
                WishId = wishId
            };

            var wishStub = new Wish() {
                Id = wishId,
                UserId = user2Id 
            };

            _wishesRepository
                .Setup(r => r.Get(wishId))
                .Returns(wishStub);
            
            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<UnauthorizedAccessException>());
        }

        [Test]
        public void CommandCorrect_ShouldCallGetMethodOfWishesRepository() {

            // arrange
            var userId = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var wishId = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");

            var command = new RemoveWishCommand() {
                UserId = userId,
                WishId = wishId
            };

            _wishesRepository
                .Setup(r => r.Get(wishId))
                .Returns(new Wish { UserId = userId, Id = wishId });

            // act
            _handler.Execute(command);

            // assert
            _wishesRepository.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Test]
        public void CommandCorrect_ShouldCallRemoveMethodOfWishesRepository() {

            // arrange
            var userId = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var wishId = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");

            var command = new RemoveWishCommand() {
                UserId = userId,
                WishId = wishId
            };

            _wishesRepository
                .Setup(r => r.Get(wishId))
                .Returns(new Wish { UserId = userId, Id = wishId });

            _handler.Execute(command);

            _wishesRepository.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Test]
        public void WishNotFound_ShouldThrowObjectNotFoundException() {
            
            // arrange
            var userId = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var wishId = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");

            var command = new RemoveWishCommand() {
                UserId = userId,
                WishId = wishId
            };

            _wishesRepository
                .Setup(r => r.Get(wishId))
                .Returns((Wish)null);

            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<RowNotInTableException>());
            _wishesRepository.Verify(r => r.Get(It.IsAny<Guid>()));
        }
    }
}