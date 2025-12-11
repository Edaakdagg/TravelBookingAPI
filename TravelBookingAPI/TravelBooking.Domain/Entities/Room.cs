using TravelBooking.Domain;
using System.Collections.Generic;

namespace TravelBooking.Domain.Entities
{

    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; }
        public string Type { get; set; } 
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; } 

        
        public int HotelId { get; set; } 
        public Hotel Hotel { get; set; } 

        
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}