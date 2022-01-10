using Domain;
using System.Collections.Generic;
using System.Collections;

namespace Infrastructure {
    public interface IDbContext {

        ICollection<Reservation> Reservations {get; set;}
        ICollection<User> Users {get; set;}
        ICollection<Wish> Wishes {get; set;}
    }
}