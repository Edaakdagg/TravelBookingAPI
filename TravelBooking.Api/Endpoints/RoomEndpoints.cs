
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Room;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Endpoints
{
    public static class RoomEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/minimal/rooms").WithTags("Rooms (Minimal API)");

            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id}", Update);
            group.MapDelete("/{id}", Delete);
        }

        private static async Task<IResult> GetAll(IRoomService service)
        {
            var result = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<object>(result, "Rooms retrieved successfully."));
        }

        private static async Task<IResult> GetById(int id, IRoomService service)
        {
            var result = await service.GetByIdAsync(id);
            return result != null 
                ? Results.Ok(new ApiResponse<RoomResponseDto>(result, "Room retrieved successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Room not found."));
        }

        private static async Task<IResult> Create([FromBody] RoomCreateDto dto, IRoomService service)
        {
            var result = await service.CreateAsync(dto);
            return Results.Created($"/api/minimal/rooms/{result.Id}", new ApiResponse<RoomResponseDto>(result, "Room created successfully."));
        }

        private static async Task<IResult> Update(int id, [FromBody] RoomUpdateDto dto, IRoomService service)
        {
            var result = await service.UpdateAsync(id, dto);
            return result != null 
                ? Results.Ok(new ApiResponse<RoomResponseDto>(result, "Room updated successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Room not found."));
        }

        private static async Task<IResult> Delete(int id, IRoomService service)
        {
            await service.DeleteAsync(id);
            return Results.Ok(new ApiResponse<object>(null, "Room deleted successfully."));
        }
    }
}
