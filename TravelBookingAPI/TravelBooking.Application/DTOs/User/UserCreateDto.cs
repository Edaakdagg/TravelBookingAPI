// TravelBooking.Application/DTOs/User/UserCreateDto.cs

using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required] 
        // Parola hash'lenmeden önce DTO'da tutulur
        public string Password { get; set; } 
    }
}