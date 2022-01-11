using Domain;
using System;
using System.Data;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.Repositories;
using Infrastructure.InMemoryDataAcces;
using Infrastructure.InMemoryDataAcces.Repositories;
using Tests.Builders;

namespace Tests.UnitTests {
    public class RemoveWishHandlerTest {

        private readonly WishBuilder _wishBuilder = new WishBuilder();


        // удалять может только тот пользователь, который создавал Wish
        // Поэтому если Id пользователя, который удаляет и Id пользователя в Wish
        // не совпадают, надо выкидывать Exception (На самом деле правильнее настроить авторизацию, но я не умею..) 
        [Test]
        public void UserIdDoesNotMatch_ShouldThrowException() {

            // arrange
            var context = new InMemoryDbContext();
            var userRepository = new UserRepository(context);
            var wishesRepository = new WishRepository(context);
            var handler = new RemoveWishHandler(userRepository, wishesRepository);

            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            var wishId = Guid.NewGuid();

            var wish = _wishBuilder
                .WithId(wishId) 
                .WithUserId(user1Id) // у wish userId  
                .WithTitle(string.Empty)
                .WithUrl(string.Empty)
                .WithReserved(false)
                .Build();

            wishesRepository.Add(wish);

            var command = new RemoveWishCommand() {
                UserId = user2Id,
                WishId = wishId
            };

            // act
            Action execute = () => handler.Execute(command);

            // assert
            Assert.That(execute, Throws.InstanceOf<UnauthorizedAccessException>());
        }

        // Проверяем, что объект действительно удаляется
        [Test]
        public void CommandCorrect_ShouldCallRemoveMethodOfWishesRepository() {

            // arrange
            var context = new InMemoryDbContext();
            var userRepository = new UserRepository(context);
            var wishesRepository = new WishRepository(context);
            var handler = new RemoveWishHandler(userRepository, wishesRepository);

            var userId = Guid.NewGuid();
            var wish1Id = Guid.NewGuid();
            var wish2Id = Guid.NewGuid();

            var wish1 = _wishBuilder
                .WithId(wish1Id)
                .WithTitle(string.Empty)
                .WithUserId(userId)
                .WithUrl(string.Empty)
                .WithReserved(false)
                .Build();
            
            var wish2 = _wishBuilder
                .WithId(wish2Id)
                .WithTitle(string.Empty)
                .WithUserId(userId)
                .WithUrl(string.Empty)
                .WithReserved(false)
                .Build();

            var command = new RemoveWishCommand() {
                UserId = userId,
                WishId = wish1Id // будем удалять wish1
            };

            wishesRepository.Add(wish1);
            wishesRepository.Add(wish2);

            // act
            handler.Execute(command);

            // assert
            Assert.That(wishesRepository.Get(wish1Id), Is.Null);
        }


        // В случае, если не найдено, должны выкинуть Exception
        [Test]
        public void WishNotFound_ShouldThrowObjectNotFoundException() {
            
            // arrange
            var context = new InMemoryDbContext();
            var userRepository = new UserRepository(context);
            var wishesRepository = new WishRepository(context);
            var handler = new RemoveWishHandler(userRepository, wishesRepository);

            var userId = Guid.NewGuid();
            var wish1Id = Guid.NewGuid();
            var wish2Id = Guid.NewGuid();

            var wish1 = _wishBuilder
                .WithId(wish1Id)
                .WithTitle(string.Empty)
                .WithUserId(userId)
                .WithUrl(string.Empty)
                .WithReserved(false)
                .Build();

             wishesRepository.Add(wish1);

            var command = new RemoveWishCommand() {
                UserId = userId,
                WishId = wish2Id 
            };

            Action execute = () => handler.Execute(command);

            Assert.That(execute, Throws.InstanceOf<RowNotInTableException>());
        }
    }
}