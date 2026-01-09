using Microsoft.AspNetCore.Mvc;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.User;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static void Map(WebApplication app)
        {
            var authGroup = app.MapGroup("/auth").WithTags("Auth");

            // POST /auth/register
            authGroup.MapPost("/register", RegisterUser)
                .Produces<ApiResponse<UserResponseDto>>(StatusCodes.Status201Created)
                .Produces<ApiResponse<object>>(StatusCodes.Status409Conflict);

            // POST /auth/login
            authGroup.MapPost("/login", LoginUser)
                .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
                .Produces<ApiResponse<object>>(StatusCodes.Status401Unauthorized);
        }

        private static async Task<IResult> RegisterUser(
            [FromBody] UserCreateDto dto, 
            [FromServices] IUserService userService)
        {
            var userDto = await userService.RegisterAsync(dto);
            
            var response = new ApiResponse<UserResponseDto>(userDto, "Kullanıcı başarıyla kaydedildi.");
            return Results.Created($"/users/{userDto.Id}", response);
        }

        private static async Task<IResult> LoginUser(
            [FromBody] UserLoginDto dto, 
            [FromServices] IUserService userService)
        {
            var (token, userDto) = await userService.LoginAsync(dto);

            var loginResponse = new 
            { 
                Token = token, 
                User = userDto 
            };
            
            var response = new ApiResponse<object>(loginResponse, "Giriş başarılı.");
            return Results.Ok(response);
        }
    }
}