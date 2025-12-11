using TravelBooking.Domain;
using System.Collections.Generic;

namespace TravelBooking.Domain.Entities
{
    /// <summary>
    /// Otel odası bilgilerini tutan varlık. BaseEntity'den miras alır.
    /// </summary>
    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; }
        public string Type { get; set; } // Örn: Tek Kişilik, Çift Kişilik, Suite
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; } // Max kişi sayısı

        // İlişkiler: Oda, hangi otele ait?
        public int HotelId { get; set; } // Foreign Key (FK)
        public Hotel Hotel { get; set; } // Navigation Property

        // İlişkiler: Bir odanın birden fazla rezervasyonu olabilir
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}