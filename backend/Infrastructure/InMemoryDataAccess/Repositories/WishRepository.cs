using Domain;
using System;
using System.Linq;
using System.Collections.Generic;
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

        public IEnumerable<Wish> GetByUser(Guid userId) {
            return _context.Wishes.Where(x => x.UserId == userId);
        }

        public void Remove(Guid id) {
            _context.Wishes.Remove(Get(id));
        }

        public Wish Get(Guid id) {
            try {
                var wish = _context.Wishes.First(x => x.Id == id);
                return wish;
            } catch(Exception ex) {
                return null;
            }
        }
    }
}