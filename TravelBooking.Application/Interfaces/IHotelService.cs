
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.Hotel;

namespace TravelBooking.Application.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelResponseDto>> GetAllAsync();
        Task<HotelResponseDto> GetByIdAsync(int id);
        Task<HotelResponseDto> CreateAsync(HotelCreateDto dto);
        Task<HotelResponseDto> UpdateAsync(int id, HotelUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
