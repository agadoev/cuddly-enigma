using Application.Repositories;
using Domain;
using System.Linq;
using System.Collections.Generic;


namespace Infrastructure.InMemoryDataAcces.Repositories {

    public class UserRepository : IUserRepository {

        private readonly InMemoryDbContext _context;

        public UserRepository(InMemoryDbContext context) {
            _context = context;
        }

        public void Add(User user) {
            (_context.Users as List<User>).Add(user);
        }
    }
}