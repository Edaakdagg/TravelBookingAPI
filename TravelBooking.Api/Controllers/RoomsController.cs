
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Room;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(new ApiResponse<object>(rooms, "Rooms retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound(new ApiResponse<object>(null, "Room not found."));
            }
            return Ok(new ApiResponse<RoomResponseDto>(room, "Room retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateDto dto)
        {
            var createdRoom = await _roomService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdRoom.Id }, new ApiResponse<RoomResponseDto>(createdRoom, "Room created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoomUpdateDto dto)
        {
            var updatedRoom = await _roomService.UpdateAsync(id, dto);
            if (updatedRoom == null)
            {
                return NotFound(new ApiResponse<object>(null, "Room not found."));
            }
            return Ok(new ApiResponse<RoomResponseDto>(updatedRoom, "Room updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteAsync(id);
            return Ok(new ApiResponse<object>(null, "Room deleted successfully."));
        }
    }
}
