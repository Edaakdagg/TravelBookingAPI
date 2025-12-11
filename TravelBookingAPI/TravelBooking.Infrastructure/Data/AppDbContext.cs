using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Hotel> Hotels { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.Username).IsRequired().HasMaxLength(100);

                user.HasQueryFilter(u => !u.IsDeleted);

                user.HasMany(u => u.Reservations)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired(false);
            });

            // Hotel
            modelBuilder.Entity<Hotel>(hotel =>
            {
                hotel.HasKey(h => h.Id);
                hotel.Property(h => h.Name).IsRequired().HasMaxLength(150);

                hotel.HasMany(h => h.Reservations)
                     .WithOne(r => r.Hotel)
                     .HasForeignKey(r => r.HotelId)
                     .IsRequired();
            });

            // Room
            modelBuilder.Entity<Room>(room =>
            {
                room.HasKey(r => r.Id);

                room.HasMany(r => r.Reservations)
                    .WithOne(rs => rs.Room)
                    .HasForeignKey(rs => rs.RoomId);
            });

            // Reservation
            modelBuilder.Entity<Reservation>(reservation =>
            {
                reservation.HasKey(r => r.Id);

                reservation.HasOne(r => r.Hotel)
                           .WithMany(h => h.Reservations)
                           .HasForeignKey(r => r.HotelId);

                reservation.HasOne(r => r.Room)
                           .WithMany(room => room.Reservations)
                           .HasForeignKey(r => r.RoomId);

                reservation.HasOne(r => r.User)
                           .WithMany(u => u.Reservations)
                           .HasForeignKey(r => r.UserId);

                reservation.ToTable(t => t.HasCheckConstraint(
                    "CK_Reservation_CheckDates",
                    "CheckInDate < CheckOutDate"
                ));
            });
        }
    }
}
