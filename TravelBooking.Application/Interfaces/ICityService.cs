
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.City;

namespace TravelBooking.Application.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponseDto>> GetAllAsync();
        Task<CityResponseDto> GetByIdAsync(int id);
        Task<CityResponseDto> CreateAsync(CityCreateDto dto);
        Task<CityResponseDto> UpdateAsync(int id, CityUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
