using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; // ConfigurationBuilder için
using TravelBooking.Infrastructure.Data; // Burası AppDbContext'in bulunduğu yer olmalı
using System.IO; 
using System; 

namespace TravelBooking.Api
{
    // EF Core araçlarının (dotnet ef) DbContext'i tasarım zamanında oluşturmasını sağlayan fabrika
    // AppDbContext'i kullanacak şekilde güncellendi.
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> // AppDbContext kullanıldı
    {
        public AppDbContext CreateDbContext(string[] args) // AppDbContext döndürülüyor
        {
            // 1. Configuration'ı Yükle
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // 2. Bağlantı Dizesini Al
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // 3. DbContextOptions'ı Yapılandır
            var builder = new DbContextOptionsBuilder<AppDbContext>(); // AppDbContext kullanıldı
            
            // Migration'ların Infrastructure katmanında olduğunu belirtiyoruz
            builder.UseSqlite(connectionString, b => 
            {
                b.MigrationsAssembly("TravelBooking.Infrastructure"); // Infrastructure Assembly adı
            });

            // 4. DbContext örneğini döndür
            return new AppDbContext(builder.Options);
        }
    }
}