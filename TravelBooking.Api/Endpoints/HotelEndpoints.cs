
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Hotel;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Endpoints
{
    public static class HotelEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/minimal/hotels").WithTags("Hotels (Minimal API)");

            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id}", Update);
            group.MapDelete("/{id}", Delete);
        }

        private static async Task<IResult> GetAll(IHotelService service)
        {
            var result = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<object>(result, "Hotels retrieved successfully."));
        }

        private static async Task<IResult> GetById(int id, IHotelService service)
        {
            var result = await service.GetByIdAsync(id);
            return result != null 
                ? Results.Ok(new ApiResponse<HotelResponseDto>(result, "Hotel retrieved successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Hotel not found."));
        }

        private static async Task<IResult> Create([FromBody] HotelCreateDto dto, IHotelService service)
        {
            var result = await service.CreateAsync(dto);
            return Results.Created($"/api/minimal/hotels/{result.Id}", new ApiResponse<HotelResponseDto>(result, "Hotel created successfully."));
        }

        private static async Task<IResult> Update(int id, [FromBody] HotelUpdateDto dto, IHotelService service)
        {
            var result = await service.UpdateAsync(id, dto);
            return result != null 
                ? Results.Ok(new ApiResponse<HotelResponseDto>(result, "Hotel updated successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Hotel not found."));
        }

        private static async Task<IResult> Delete(int id, IHotelService service)
        {
            await service.DeleteAsync(id);
            return Results.Ok(new ApiResponse<object>(null, "Hotel deleted successfully."));
        }
    }
}
