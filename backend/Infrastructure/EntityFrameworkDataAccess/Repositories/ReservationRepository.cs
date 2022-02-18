using Domain;
using System;
using Infrastructure.Entities;
using Application.Repositories;
using Infrastructure.EntityFrameworkDataAccess;
using Infrastructure.EntityFrameworkDataAccess.Mappers;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories {

    public class ReservationRepository : IReservationRepository {
        private EFDbContext _context;
        private IMapper<ReservationEntity, Reservation> _mapper;

        public ReservationRepository(EFDbContext context) {
            _context = context;
            _mapper = new ReservationMapper();
        }

        public void Add(Reservation reservation) {
            var entity = _mapper.Map(reservation);
            
            _context.Reservations.Add(entity);

            _context.SaveChanges();
        }

        public Reservation GetByWishId(Guid wishId) => throw new NotImplementedException();

        public Reservation Get(Guid id) {
            var entity = _context.Reservations.Find(id);

            return _mapper.Map(entity);
        }

    }
}