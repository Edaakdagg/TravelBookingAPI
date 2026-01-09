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
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }
    }
}