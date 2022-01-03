using System.Collections.Generic;
using Infrastructure.Entities;
using Domain;

namespace Infrastructure.InMemoryDataAcces {

    public class InMemoryDbContext {
        
        public IEnumerable<User> Users {get; set;}

        public InMemoryDbContext() {
            Users = new List<User>();
        } }
}