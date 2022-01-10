using System;
using Domain;
using System.Collections.Generic;
using NUnit.Framework;
using Application.UseCases;
using Application.Repositories;
using Moq;

namespace Tests.UnitTests {
    public class ReserveWishHandlerTest {

        private ICommandHandler<ReserveWishCommand> _handler {get; set;}

        private Mock<IUserRepository> _userRepositoryMock {get; set;}
        private Mock<IWishesRepository> _wishesRepositoryMock {get; set;}
        private Mock<IReservationRepository> _reservationRepositoryMock {get; set;}

        [SetUp]
        public void SetUp() {
            _userRepositoryMock = new Mock<IUserRepository>();
            _wishesRepositoryMock = new Mock<IWishesRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();

            _handler = new ReserveWishHandler(
                _reservationRepositoryMock.Object,
                _userRepositoryMock.Object,
                _wishesRepositoryMock.Object
            );
        }

        [Test]
        public void ShoulCallAddMethodOfReservationRepository() {

            var command = new ReserveWishCommand();
            
            var reserverGuid = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
            var wishGuid = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");

            var wish = new Wish {
                Id = wishGuid,
                Title = "",
                Url = "",
                Reserved = false
            };

            var reserver = new User {
                Id = reserverGuid,
                Name = "Alex",
                Wishlist = new List<Wish>() { wish }
            };

            _userRepositoryMock
                .Setup((r) => r.Get(reserverGuid))
                .Returns(reserver);

            _wishesRepositoryMock
                .Setup((r) => r.Get(wishGuid))
                .Returns(wish);

            command.ReserverId = reserverGuid;
            command.WishId = wishGuid;

            _handler.Execute(command);

            // проверяем, что метод вызвался хотя бы один раз с параметром Reservation 
            _reservationRepositoryMock.Verify((r) => r.Add(It.IsAny<Reservation>()), Times.Once());
        } 


        [Test]
        public void WishAlreadyReserved_ShouldThrowArgumentException() {

            var command = new ReserveWishCommand();
            
            var reserverGuid = new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
            var wishGuid = new Guid("1c34f960-10eb-40d7-a945-fd3f39e1ccfb");
            var reservationId = new Guid("44f5d53b-c697-4d97-aa4c-4f4ee98620a4");

            var wish = new Wish {
                Id = wishGuid,
                Title = "",
                Url = "",
                Reserved = true // важно, что здесь reserved = true
            };

            var reserver = new User {
                Id = reserverGuid,
                Name = "Alex",
                Wishlist = new List<Wish>() { wish }
            };

            var reservation = new Reservation(reserver, wish);
            reservation.Id = reservationId;

            _userRepositoryMock
                .Setup((r) => r.Get(reserverGuid))
                .Returns(reserver);

            _wishesRepositoryMock
                .Setup((r) => r.Get(wishGuid))
                .Returns(wish);

            _reservationRepositoryMock
                .Setup((r) => r.Get(reservationId))
                .Returns(reservation);

            command.ReserverId = reserverGuid;
            command.WishId = wishGuid;

            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<ArgumentException>());
        }
    }
}