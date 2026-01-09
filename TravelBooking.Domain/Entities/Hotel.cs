using System.Collections.Generic;
using TravelBooking.Domain;

namespace TravelBooking.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }

        public int CityId { get; set; } 
        public City City { get; set; } // İlişki: N-1
        public ICollection<Room> Rooms { get; set; } // İlişki: 1-N [cite: 16]
    }
}