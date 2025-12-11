using TravelBooking.Domain;
using System.Collections.Generic;

namespace TravelBooking.Domain.Entities
{
   
    public class Hotel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; } // 1'den 5'e kadar yıldız

        
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
