
using System;

namespace TravelBooking.Application.DTOs.Reservation
{
    public class ReservationCreateDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
