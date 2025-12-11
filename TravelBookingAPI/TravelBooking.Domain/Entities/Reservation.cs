using TravelBooking.Domain;
using System;

namespace TravelBooking.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;
        public int TotalGuests { get; set; }
        public decimal TotalPrice { get; set; }

        // Room ilişkisi
        public int RoomId { get; set; }
        public Room Room { get; set; }

        // User ilişkisi (opsiyonel)
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Hotel ilişkisi (zorunlu)
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
