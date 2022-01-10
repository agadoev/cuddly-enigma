using System.Collections.Generic;
using Infrastructure.Entities;
using Infrastructure;
using Domain;

namespace Infrastructure.InMemoryDataAcces {

    public class InMemoryDbContext {
        
        public virtual ICollection<User> Users {get; set;}

        public ICollection<Wish> Wishes {get; set;}

        public ICollection<Reservation> Reservations {get; set;}

        public InMemoryDbContext() {
            Users = new HashSet<User>();
            Wishes = new HashSet<Wish>();
            Reservations = new HashSet<Reservation>();
        }
    }
}