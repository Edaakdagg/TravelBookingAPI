
using TravelBooking.Application.DTOs.Hotel;

namespace TravelBooking.Application.DTOs.Room
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
        // public HotelResponseDto Hotel { get; set; } // Optional: Circular dependency risk if not careful, but usually okay for read.
    }
}
