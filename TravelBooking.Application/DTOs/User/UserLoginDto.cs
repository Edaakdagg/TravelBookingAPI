// TravelBooking.Application/DTOs/User/UserLoginDto.cs

using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.DTOs.User
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}