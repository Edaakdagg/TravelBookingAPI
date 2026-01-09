
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.City;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(new ApiResponse<object>(cities, "Cities retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound(new ApiResponse<object>(null, "City not found."));
            }
            return Ok(new ApiResponse<CityResponseDto>(city, "City retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto dto)
        {
            var createdCity = await _cityService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, new ApiResponse<CityResponseDto>(createdCity, "City created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityUpdateDto dto)
        {
            var updatedCity = await _cityService.UpdateAsync(id, dto);
            if (updatedCity == null)
            {
                return NotFound(new ApiResponse<object>(null, "City not found."));
            }
            return Ok(new ApiResponse<CityResponseDto>(updatedCity, "City updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteAsync(id);
            return Ok(new ApiResponse<object>(null, "City deleted successfully."));
        }
    }
}
