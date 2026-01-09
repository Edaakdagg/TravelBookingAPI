using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities; 
using TravelBooking.Domain; // BaseEntity için
using System.Threading;     
using System.Threading.Tasks; 
using System.Linq; 
using System;

namespace TravelBooking.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // DbSet'ler
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Soft Delete için Global Query Filter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<City>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Hotel>().HasQueryFilter(h => !h.IsDeleted);
            modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Reservation>().HasQueryFilter(r => !r.IsDeleted);

            // İlişki Konfigürasyonları:

            // Hotel - City (1-N)
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Room - Hotel (1-N)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation - Room (1-N)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Reservation - User (1-N)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // CreatedAt/UpdatedAt ve Soft Delete Mantığı
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var utcNow = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = utcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = utcNow;
                }
                else if (entry.State == EntityState.Deleted) // Soft Delete
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.UpdatedAt = utcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}