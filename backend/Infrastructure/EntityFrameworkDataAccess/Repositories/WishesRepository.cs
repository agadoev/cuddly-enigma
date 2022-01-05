using System;
using System.Linq;
using Application.Repositories;
using Domain;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories {
    public class WishesRepository : IWishesRepository {

        private EFDbContext _context;

        public WishesRepository(EFDbContext context) {
            _context = context;
        }

        public void Add(Wish wish) {
            var wishEntity = new WishEntity {
                Id = wish.Id,
                Title = wish.Title,
                Url = wish.Url,
                UserId = wish.UserId,
            };

            _context.Wishes.Add(wishEntity);

            _context.SaveChanges();

        }

        public Wish Get(Guid id) {
            var wishEntity = _context.Wishes.Find(id);

            var wish = new Wish() {
                Id = wishEntity.Id,
                Title = wishEntity.Title,
                Url = wishEntity.Url,
                UserId = wishEntity.UserId
            };

            return wish;
        }

        public IEnumerable<Wish> GetByUser(Guid userId) {
            
            var wishEntities = _context.Wishes.Where(w => w.UserId == userId);

            var wishes = wishEntities.Select(w => new Wish {
                Id = w.Id,
                Title = w.Title,
                Url = w.Url,
                UserId = w.UserId                
            });

            return wishes;
        }

        public void Remove(Guid id) {
            _context.Wishes.Remove(_context.Wishes.Find(id));

            _context.SaveChanges();
        }
    }
}