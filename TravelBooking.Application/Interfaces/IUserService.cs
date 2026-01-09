using TravelBooking.Application.DTOs.User;
using System.Threading.Tasks;

namespace TravelBooking.Application.Interfaces
{
    public interface IUserService // : IGenericService<...>
    {
        Task<UserResponseDto> GetByIdAsync(int id);
        Task<(string Token, UserResponseDto User)> LoginAsync(UserLoginDto dto);
        Task<UserResponseDto> RegisterAsync(UserCreateDto dto);
    }
}