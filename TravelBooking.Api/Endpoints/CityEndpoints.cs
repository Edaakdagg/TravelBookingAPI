
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.City;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Endpoints
{
    public static class CityEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/minimal/cities").WithTags("Cities (Minimal API)");

            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id}", Update);
            group.MapDelete("/{id}", Delete);
        }

        private static async Task<IResult> GetAll(ICityService service)
        {
            var result = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<object>(result, "Cities retrieved successfully."));
        }

        private static async Task<IResult> GetById(int id, ICityService service)
        {
            var result = await service.GetByIdAsync(id);
            return result != null 
                ? Results.Ok(new ApiResponse<CityResponseDto>(result, "City retrieved successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "City not found."));
        }

        private static async Task<IResult> Create([FromBody] CityCreateDto dto, ICityService service)
        {
            var result = await service.CreateAsync(dto);
            return Results.Created($"/api/minimal/cities/{result.Id}", new ApiResponse<CityResponseDto>(result, "City created successfully."));
        }

        private static async Task<IResult> Update(int id, [FromBody] CityUpdateDto dto, ICityService service)
        {
            var result = await service.UpdateAsync(id, dto);
            return result != null 
                ? Results.Ok(new ApiResponse<CityResponseDto>(result, "City updated successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "City not found."));
        }

        private static async Task<IResult> Delete(int id, ICityService service)
        {
            await service.DeleteAsync(id);
            return Results.Ok(new ApiResponse<object>(null, "City deleted successfully."));
        }
    }
}
