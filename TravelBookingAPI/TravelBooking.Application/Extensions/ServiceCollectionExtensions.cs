using Microsoft.Extensions.DependencyInjection;
using TravelBooking.Application.Interfaces;
using TravelBooking.Application.Services;

namespace TravelBooking.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            // Diğer servisler (IReservationService vb.) buraya eklenecek

            return services;
        }
    }
}