
using TravelBooking.Application.DTOs.City;
namespace TravelBooking.Application.DTOs.Hotel
{
    public class HotelResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }
        public int CityId { get; set; }
        public CityResponseDto City { get; set; }
    }
}
