using Autofac;
using Core.UseCases;
using Core;
using Data;

namespace Api {
    public class AutofacModule : Module
    {
        public IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            Load(builder);
            return builder.Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IDbContext<User>>().As<DbContext<User>>().InstancePerLifetimeScope();
            builder.RegisterType<IDbContext<Item>>().As<DbContext<Item>>().InstancePerLifetimeScope();
            builder.RegisterType<IDbContext<Wishlist>>().As<DbContext<Wishlist>>().InstancePerLifetimeScope();
        }
    }
}