using TravelBooking.Application.DTOs.User;
using TravelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelBooking.Api.Endpoints
{
    public static class AuthEndpoints
    {
       
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth")
                           .WithTags("Authentication"); 

            
            group.MapPost("/register", async (
                [FromBody] UserCreateDto userCreateDto, 
                IUserService userService) =>
            {
                
                var result = await userService.RegisterAsync(userCreateDto);

                return Results.Ok(result); 
            })
            .WithName("RegisterUser")
            .Produces<UserResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest); 

            
            group.MapPost("/login", async (
                [FromBody] UserLoginDto userLoginDto, 
                IUserService userService) =>
            {
                try
                {
                    var (token, userDto) = await userService.LoginAsync(userLoginDto);

                    return Results.Ok(new { Token = token, User = userDto });
                }
                catch (UnauthorizedAccessException)
                {
                    return Results.Unauthorized();
                }
                
            })
            .WithName("LoginUser")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}