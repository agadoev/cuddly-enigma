using System;
using System.Linq;
using Domain;
using Application.Repositories;

// TODO: можно сделать базовый класс репозитория..
// TODO: или вообще пользоваться тупо напрямую контекстом..

namespace Infrastructure.InMemoryDataAcces.Repositories {

    public class ReservationRepository : IReservationRepository {

        private readonly InMemoryDbContext _context;

        public ReservationRepository(InMemoryDbContext context) {
            _context = context;
        }

        public void Add(Reservation reservarion) {
            _context.Reservations.Add(reservarion);
        }

        public Reservation Get(Guid id) {
            return _context.Reservations.First(x => x.Id == id);
        }
    }
}