
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Api.Models;
using TravelBooking.Application.DTOs.Reservation;
using TravelBooking.Application.Interfaces;

namespace TravelBooking.Api.Endpoints
{
    public static class ReservationEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/minimal/reservations").WithTags("Reservations (Minimal API)");

            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id}", Update);
            group.MapDelete("/{id}", Delete);
        }

        private static async Task<IResult> GetAll(IReservationService service)
        {
            var result = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<object>(result, "Reservations retrieved successfully."));
        }

        private static async Task<IResult> GetById(int id, IReservationService service)
        {
            var result = await service.GetByIdAsync(id);
            return result != null 
                ? Results.Ok(new ApiResponse<ReservationResponseDto>(result, "Reservation retrieved successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Reservation not found."));
        }

        private static async Task<IResult> Create([FromBody] ReservationCreateDto dto, IReservationService service)
        {
            var result = await service.CreateAsync(dto);
            return Results.Created($"/api/minimal/reservations/{result.Id}", new ApiResponse<ReservationResponseDto>(result, "Reservation created successfully."));
        }

        private static async Task<IResult> Update(int id, [FromBody] ReservationUpdateDto dto, IReservationService service)
        {
            var result = await service.UpdateAsync(id, dto);
            return result != null 
                ? Results.Ok(new ApiResponse<ReservationResponseDto>(result, "Reservation updated successfully.")) 
                : Results.NotFound(new ApiResponse<object>(null, "Reservation not found."));
        }

        private static async Task<IResult> Delete(int id, IReservationService service)
        {
            await service.DeleteAsync(id);
            return Results.Ok(new ApiResponse<object>(null, "Reservation deleted successfully."));
        }
    }
}
