
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.Reservation;

namespace TravelBooking.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationResponseDto>> GetAllAsync();
        Task<ReservationResponseDto> GetByIdAsync(int id);
        Task<ReservationResponseDto> CreateAsync(ReservationCreateDto dto);
        Task<ReservationResponseDto> UpdateAsync(int id, ReservationUpdateDto dto);
        Task DeleteAsync(int id);
        // Additional methods if needed, e.g., GetByUserId
    }
}
