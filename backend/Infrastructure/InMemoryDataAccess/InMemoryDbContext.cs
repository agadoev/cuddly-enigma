using System.Collections.Generic;
using Infrastructure.Entities;
using Domain;

namespace Infrastructure.InMemoryDataAcces {

    public class InMemoryDbContext {
        
        public IEnumerable<User> Users {get; set;}

        public ICollection<Wish> Wishes {get; set;}

        public ICollection<Reservation> Reservations {get; set;}

        public InMemoryDbContext() {
            Users = new List<User>();
            Wishes = new HashSet<Wish>();
            Reservations = new HashSet<Reservation>();
        }
    }
}