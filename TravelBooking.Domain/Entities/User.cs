using System.Collections.Generic;
using TravelBooking.Domain;

namespace TravelBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = "User";
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); 
    }
}