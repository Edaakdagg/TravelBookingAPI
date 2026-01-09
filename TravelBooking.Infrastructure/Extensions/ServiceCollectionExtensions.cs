using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Infrastructure.Data;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.Repositories;

namespace TravelBooking.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString)); 

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}