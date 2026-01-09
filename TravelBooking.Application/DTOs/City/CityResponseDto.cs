
using TravelBooking.Application.DTOs.Hotel;
namespace TravelBooking.Application.DTOs.City
{
    public class CityResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        // public List<HotelResponseDto> Hotels { get; set; } // Optional: include hotels? Maybe simple for now.
    }
}
