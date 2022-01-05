using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Application.UseCases;
using Application.Repositories;
using Infrastructure.InMemoryDataAcces.Repositories;
using Infrastructure.InMemoryDataAcces;
using Infrastructure.EntityFrameworkDataAccess;
using Infrastructure.EntityFrameworkDataAccess.Repositories;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);

            // handlers
            services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserHandler>();
            services.AddScoped<ICommandHandler<CreateWishCommand>, CreateWishHandler>();
            services.AddScoped<ICommandHandler<RemoveWishCommand>, RemoveWishHandler>();
            services.AddScoped<ICommandHandler<ReserveWishCommand>, ReserveWishHandler>();
            services.AddScoped<ICommandHandler<GetWishesByUserCommand>, GetWishesByUserHandler>();

            // repositories
            services.AddScoped<IUserRepository, Infrastructure.EntityFrameworkDataAccess.Repositories.UserRepository>();
            services.AddScoped<IWishesRepository, Infrastructure.EntityFrameworkDataAccess.Repositories.WishesRepository>();
            services.AddScoped<IReservationRepository, Infrastructure.EntityFrameworkDataAccess.Repositories.ReservationRepository>();

            services.AddSingleton<InMemoryDbContext>(new InMemoryDbContext());
            services.AddSingleton<EFDbContext>(new ContextFactory().CreateDbContext(new string[] {Configuration.GetConnectionString("DEV")}));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(
                options => 
                    options
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
            );
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
