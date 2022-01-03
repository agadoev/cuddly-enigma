

namespace Infrastructure.InMemoryDataAcces {

    public sealed class InMemoryContextFactory {

        public InMemoryDbContext CreateContext() {
            var context = new InMemoryDbContext();
            return context;
        }
    }

}