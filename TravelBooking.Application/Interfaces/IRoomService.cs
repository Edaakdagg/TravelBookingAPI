
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.Room;

namespace TravelBooking.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponseDto>> GetAllAsync();
        Task<RoomResponseDto> GetByIdAsync(int id);
        Task<RoomResponseDto> CreateAsync(RoomCreateDto dto);
        Task<RoomResponseDto> UpdateAsync(int id, RoomUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
