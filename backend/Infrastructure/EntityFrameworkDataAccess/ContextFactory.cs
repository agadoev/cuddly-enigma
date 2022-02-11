using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.EntityFrameworkDataAccess {

    public class ContextFactory : IDesignTimeDbContextFactory<EFDbContext> {
        public EFDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            // optionsBuilder.UseSqlite("Data Source=/Users/gadoevalex/wishlist/backend/Infrastructure/wish.db");
            var connString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=gadoevalex;Password=";
            optionsBuilder.UseNpgsql(connString); // args[0] - connection string

            return new EFDbContext(optionsBuilder.Options);
        }
    }
}