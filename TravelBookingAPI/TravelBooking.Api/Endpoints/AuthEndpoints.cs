using TravelBooking.Application.DTOs.User;
using TravelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelBooking.Api.Endpoints
{
    public static class AuthEndpoints
    {
        // Extension metot ile endpoint'leri haritalar
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth")
                           .WithTags("Authentication"); // Swagger'da gruplama için

            // 1. KAYIT (Register) Endpoint'i
            group.MapPost("/register", async (
                [FromBody] UserCreateDto userCreateDto, 
                IUserService userService) =>
            {
                // DÜZELTME: RegisterAsync metodu çağrılıyor
                var result = await userService.RegisterAsync(userCreateDto);

                return Results.Ok(result); 
            })
            .WithName("RegisterUser")
            .Produces<UserResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest); // Swagger dökümantasyonu için

            // 2. GİRİŞ (Login) Endpoint'i
            group.MapPost("/login", async (
                [FromBody] UserLoginDto userLoginDto, 
                IUserService userService) =>
            {
                try
                {
                    // DÜZELTME: LoginAsync metodu çağrılıyor ve tuple geri dönüşü yakalanıyor
                    var (token, userDto) = await userService.LoginAsync(userLoginDto);

                    // Başarılı giriş: Token ve kullanıcı bilgileri geri döndürülüyor
                    return Results.Ok(new { Token = token, User = userDto });
                }
                catch (UnauthorizedAccessException)
                {
                    // UserService'ten gelen yetkilendirme hatasını yakalar
                    return Results.Unauthorized();
                }
                
            })
            .WithName("LoginUser")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}