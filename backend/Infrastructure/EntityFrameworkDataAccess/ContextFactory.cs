using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.EntityFrameworkDataAccess {

    public class ContextFactory : IDesignTimeDbContextFactory<EFDbContext> {
        public EFDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            optionsBuilder.UseSqlite("Data Source=/Users/gadoevalex/wishlist/backend/Infrastructure/wish.db");

            return new EFDbContext(optionsBuilder.Options);
        }
    }
}