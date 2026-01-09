using System.Collections.Generic;
using TravelBooking.Domain;

namespace TravelBooking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } // İlişki: N-1
        public ICollection<Reservation> Reservations { get; set; } // İlişki: 1-N [cite: 16]
    }
}