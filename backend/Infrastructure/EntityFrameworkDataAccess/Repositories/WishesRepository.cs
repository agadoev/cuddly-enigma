using System;
using System.Linq;
using Application.Repositories;
using Domain;
using System.Collections.Generic;
using Infrastructure.Entities;
using Infrastructure.EntityFrameworkDataAccess.Mappers;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories {
    public class WishesRepository : IWishesRepository {

        private EFDbContext _context;

        private IMapper<Wish, WishEntity> _mapper;

        public WishesRepository(EFDbContext context) {
            _context = context;
            _mapper = new WishMapper();
        }

        public void Add(Wish wish) {
            var entity = _mapper.Map(wish);

            _context.Wishes.Add(entity);

            _context.SaveChanges();
        }

        public Wish Get(Guid id) {
            var wishEntity = _context.Wishes.Find(id);

            return _mapper.Map(wishEntity);
        }

        public IEnumerable<Wish> GetByUser(Guid userId) {
            
            var wishEntities = _context.Wishes.Where(w => w.UserId == userId).AsEnumerable().ToList();

            var wishes = wishEntities.Select(w => _mapper.Map(w));

            return wishes;
        }

        public void Remove(Guid id) {
            _context.Wishes.Remove(_context.Wishes.Find(id));

            _context.SaveChanges();
        }
    }
}