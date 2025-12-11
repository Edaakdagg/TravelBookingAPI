using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Infrastructure.Data; // AppDbContext'in namespace'i
using TravelBooking.Infrastructure.Interfaces; 
using TravelBooking.Infrastructure.Repositories; 

namespace TravelBooking.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. DbContext Kaydı (KRİTİK DÜZELTME: AppDbContext kullanılmalı)
            // SQLite veritabanı bağlantı dizesi kullanılarak DbContext servise eklenir.
            services.AddDbContext<AppDbContext>(options => // <-- Sınıf adı AppDbContext olarak düzeltildi
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Generic Repository kaydı (Tüm entity'ler için)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}