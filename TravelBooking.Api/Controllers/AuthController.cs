
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.User;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            var userDto = await _userService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), new { id = userDto.Id }, new ApiResponse<UserResponseDto>(userDto, "User registered successfully."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var (token, userDto) = await _userService.LoginAsync(dto);
             var loginResponse = new 
            { 
                Token = token, 
                User = userDto 
            };
            return Ok(new ApiResponse<object>(loginResponse, "Login successful."));
        }
    }
}
