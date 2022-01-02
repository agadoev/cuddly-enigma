using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data {

    public class SqliteContext : DbContext {

        public DbSet<UserEntity> Users {get; set;}
        public DbSet<WishEntity> Wishes {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=wish.db");
    }
}