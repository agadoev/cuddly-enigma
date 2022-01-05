using System;
using System.Linq;
using Domain;
using Application.Repositories;
using Infrastructure.Entities;
using Infrastructure.EntityFrameworkDataAccess;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories {
    public class UserRepository : IUserRepository {

        private EFDbContext _context;

        public UserRepository(EFDbContext context) {
            _context = context;
        }

        public void Add(User user) {
            var userEntity = new UserEntity() {
                Id = user.Id,
                Name = user.Name,
                Wishes = user.Wishlist.Select(w => new WishEntity {
                    Id = w.Id,
                    Title = w.Title,
                    Url = w.Url,
                    UserId = w.UserId
                })
            };
            
            _context.Users.Add(userEntity);
        }

        public User Get(Guid id) {
            var userEntity = _context.Users.Find(id);

            // вынести эту хуйню в отдельный класс маппер
            var user = new User {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Wishlist = userEntity.Wishes.Select(w => new Wish {
                    Id = w.Id,
                    Url = w.Url,
                    Title = w.Title,
                    UserId = w.UserId
                }).ToList()
            };

            return user;
        }
    }
}