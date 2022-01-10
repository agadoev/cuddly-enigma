using System;
using Domain;
using System.Collections.Generic;
using NUnit.Framework;
using Application.UseCases;
using Application.Repositories;
using Moq;
using Tests.Builders;

/** 
    Использование DataBuilder
*/

namespace Tests.UnitTests {

    public class ObjectMother {
        public Guid GetGuid1() => new Guid("1a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
        public Guid GetGuid2() => new Guid("2a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
        public Guid GetGuid3() => new Guid("3a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
        public Guid GetGuid4() => new Guid("4a3b9b40-58d5-4b25-8e30-3c78a8359c4f");
    }


    public class ReserveWishHandlerTest {

        private ICommandHandler<ReserveWishCommand> _handler {get; set;}
        private Mock<IUserRepository> _userRepositoryMock {get; set;}
        private Mock<IWishesRepository> _wishesRepositoryMock {get; set;}
        private Mock<IReservationRepository> _reservationRepositoryMock {get; set;}

        private readonly ObjectMother _mother = new ObjectMother();
        private readonly ReserveWishCommandBuilder _commandBuilder = new ReserveWishCommandBuilder();
        private readonly WishBuilder _wishBuilder = new WishBuilder();
        private readonly UserBuilder _userBuilder = new UserBuilder();
        private readonly ReservationBuilder _reservationBuilder = new ReservationBuilder();

        [SetUp]
        public void SetUp() {
            _userRepositoryMock = new Mock<IUserRepository>();
            _wishesRepositoryMock = new Mock<IWishesRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();

            _wishesRepositoryMock
                .Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(It.IsAny<Wish>());

            _handler = new ReserveWishHandler(
                _reservationRepositoryMock.Object,
                _userRepositoryMock.Object,
                _wishesRepositoryMock.Object
            );
        }

        [Test]
        public void ShoulCallAddMethodOfReservationRepository() {

            var reserverGuid = _mother.GetGuid1();
            var wishGuid = _mother.GetGuid2();

            _userRepositoryMock
                .Setup((r) => r.Get(reserverGuid))
                .Returns(_userBuilder
                    .WithId(reserverGuid)
                    .WithName("Alex")
                    .WithList(new List<Wish>())
                    .Build());

            _wishesRepositoryMock
                .Setup((r) => r.Get(wishGuid))
                .Returns(_wishBuilder
                    .WithId(wishGuid)
                    .WithReserved(false)
                    .WithTitle("Wish")
                    .WithUrl("http://...")
                    .WithUserId(reserverGuid)
                    .Build());

            var command = _commandBuilder
                .WithReserverId(reserverGuid)
                .WithWishId(wishGuid)
                .Build();

            _handler.Execute(command);

            // проверяем, что метод вызвался хотя бы один раз с параметром Reservation 
            _reservationRepositoryMock.Verify((r) => r.Add(It.IsAny<Reservation>()), Times.Once());
        } 


        [Test]
        public void WishAlreadyReserved_ShouldThrowArgumentException() {
            var command = _commandBuilder
                .WithDone(false)
                .WithSuccess(false)
                .WithReserverId(It.IsAny<Guid>())
                .WithWishId(It.IsAny<Guid>())
                .Build();

            _reservationRepositoryMock
                .Setup((r) => r.GetByWishId(It.IsAny<Guid>()))
                .Returns<Reservation>(x => x);

            Assert.That(() => _handler.Execute(command), Throws.InstanceOf<ArgumentException>());
        }
    }
}