
using System;
using TravelBooking.Application.DTOs.User;
using TravelBooking.Application.DTOs.Room;

namespace TravelBooking.Application.DTOs.Reservation
{
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        // public UserResponseDto User { get; set; }
        public int RoomId { get; set; }
        // public RoomResponseDto Room { get; set; }
    }
}
