
using System;

namespace TravelBooking.Application.DTOs.Reservation
{
    public class ReservationUpdateDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}
