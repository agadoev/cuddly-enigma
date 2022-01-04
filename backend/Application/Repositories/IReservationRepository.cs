using System;
using Domain;

namespace Application.Repositories {
    public interface IReservationRepository {
        
        void Add(Reservation reservation);

        Reservation Get(Guid id);

    }
}