using System;
using TravelBooking.Domain;

namespace TravelBooking.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // İlişki: N-1

        public int RoomId { get; set; }
        public Room Room { get; set; } // İlişki: N-1
    }
}