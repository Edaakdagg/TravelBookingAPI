using System.Collections.Generic;

namespace TravelBooking.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Hotel> Hotels { get; set; } // İlişki: 1-N [cite: 16]
    }
}