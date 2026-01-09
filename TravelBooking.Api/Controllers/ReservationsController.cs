
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Reservation;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(new ApiResponse<object>(reservations, "Reservations retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound(new ApiResponse<object>(null, "Reservation not found."));
            }
            return Ok(new ApiResponse<ReservationResponseDto>(reservation, "Reservation retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
        {
            var createdReservation = await _reservationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, new ApiResponse<ReservationResponseDto>(createdReservation, "Reservation created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReservationUpdateDto dto)
        {
            var updatedReservation = await _reservationService.UpdateAsync(id, dto);
            if (updatedReservation == null)
            {
                return NotFound(new ApiResponse<object>(null, "Reservation not found."));
            }
            return Ok(new ApiResponse<ReservationResponseDto>(updatedReservation, "Reservation updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationService.DeleteAsync(id);
            return Ok(new ApiResponse<object>(null, "Reservation deleted successfully."));
        }
    }
}
