using Backend.Application.Core.Services;
using Backend.Domain.Core.Repositories;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<Sample01DbContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:Sample01Database",
                x => x.MigrationsAssembly("Sample01.Infrastructure")));

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<Sample01DbContext>>();

            using (var dbContext = new Sample01DbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}