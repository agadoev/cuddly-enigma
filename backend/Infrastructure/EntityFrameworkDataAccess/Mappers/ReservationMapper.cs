using Infrastructure.Entities;
using Domain;

namespace Infrastructure.EntityFrameworkDataAccess.Mappers {

    public class ReservationMapper : IMapper<ReservationEntity, Reservation> {

        public Reservation Map (ReservationEntity entity) {


            var user = new User {
                Id = entity.Reserver.Id,
                Name = entity.Reserver.Name
            };

            var wish = new Wish {
                Id = entity.WishId,
                Title = entity.Wish.Title,
                Url = entity.Wish.Url
            };

            var reservation = new Reservation(user, wish) {
                Id = entity.Id,
            };


            return reservation;
        }

        public ReservationEntity Map(Reservation reservation) {
            var entity = new ReservationEntity();

            entity.Id = reservation.Id;
            entity.ReserverId = reservation.User.Id;
            entity.Reserver = new UserEntity {
                Id = reservation.User.Id,
                Name = reservation.User.Name
            };
            entity.WishId = reservation.Wish.Id;
            entity.Wish = new WishEntity {
                Id = reservation.Wish.Id,
                Title = reservation.Wish.Title,
                Url = reservation.Wish.Url
            };

            return entity;
        }

    }
}