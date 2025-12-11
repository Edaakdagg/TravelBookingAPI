// TravelBooking.Application/DTOs/User/UserResponseDto.cs

using System;

namespace TravelBooking.Application.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        // Parola hash/salt bilgileri güvenlik nedeniyle burada yer almaz
    }
}