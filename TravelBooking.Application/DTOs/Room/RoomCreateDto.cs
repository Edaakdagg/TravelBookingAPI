
namespace TravelBooking.Application.DTOs.Room
{
    public class RoomCreateDto
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
    }
}
