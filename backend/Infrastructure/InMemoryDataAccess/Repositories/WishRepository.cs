using Domain;
using System;
using System.Linq;
using Application.Repositories;

namespace Infrastructure.InMemoryDataAcces.Repositories {
    public class WishRepository :  IWishesRepository{

        private readonly InMemoryDbContext _context;
        public WishRepository(InMemoryDbContext context) {
            _context = context;
        }

        public void Add(Wish wish) {
            _context.Wishes.Add(wish);
        }

        public Wish Get(Guid id) {
            return _context.Wishes.First(x => x.Id == id);
        }
    }
}