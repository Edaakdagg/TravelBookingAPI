using TravelBooking.Application.DTOs.User;
using System.Threading.Tasks;

namespace TravelBooking.Application.Interfaces
{
    public interface IUserService
    {
        // CRUD metotlarını isterseniz Task<UserResponseDto> GetByIdAsync(int id); olarak tutabilirsiniz.
        Task<UserResponseDto> GetByIdAsync(int id); // UserService'de var ama NotImplemented

        // KRİTİK DÜZELTME 1: RegisterAsync olmalı (UserService ile uyumlu)
        Task<UserResponseDto> RegisterAsync(UserCreateDto dto); 

        // KRİTİK DÜZELTME 2: LoginAsync olmalı (UserService ile uyumlu)
        Task<(string Token, UserResponseDto User)> LoginAsync(UserLoginDto dto); 
    }
}