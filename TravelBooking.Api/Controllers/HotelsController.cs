
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Hotel;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelService.GetAllAsync();
            return Ok(new ApiResponse<object>(hotels, "Hotels retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound(new ApiResponse<object>(null, "Hotel not found."));
            }
            return Ok(new ApiResponse<HotelResponseDto>(hotel, "Hotel retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelCreateDto dto)
        {
            var createdHotel = await _hotelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdHotel.Id }, new ApiResponse<HotelResponseDto>(createdHotel, "Hotel created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HotelUpdateDto dto)
        {
            var updatedHotel = await _hotelService.UpdateAsync(id, dto);
            if (updatedHotel == null)
            {
                return NotFound(new ApiResponse<object>(null, "Hotel not found."));
            }
            return Ok(new ApiResponse<HotelResponseDto>(updatedHotel, "Hotel updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelService.DeleteAsync(id);
            return Ok(new ApiResponse<object>(null, "Hotel deleted successfully."));
        }
    }
}
