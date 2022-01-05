using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.EntityFrameworkDataAccess {
    public class EFDbContext : DbContext {
        public EFDbContext(DbContextOptions options) : base(options) {}

        public DbSet<UserEntity> Users {get; set;}

        public DbSet<WishEntity> Wishes {get; set;}

        public DbSet<ReservationEntity> Reservations {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=wish.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserEntity> ()
                .ToTable ("Users");
        }
    }
}