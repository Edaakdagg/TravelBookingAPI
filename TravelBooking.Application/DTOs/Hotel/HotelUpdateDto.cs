
namespace TravelBooking.Application.DTOs.Hotel
{
    public class HotelUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }
        public int CityId { get; set; }
    }
}
