using Domain;
using System;
using Infrastructure.Entities;
using Application.Repositories;
using Infrastructure.EntityFrameworkDataAccess;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories {

    public class ReservationRepository : IReservationRepository {
        private EFDbContext _context;

        public ReservationRepository(EFDbContext context) {
            _context = context;
        }

        public void Add(Reservation reservation) {
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
            
            _context.Reservations.Add(entity);
        }

        public Reservation Get(Guid id) {
            var entity = _context.Reservations.Find(id);

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

    }
}