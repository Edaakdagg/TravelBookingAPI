using System;
using System.Collections.Generic;
using TravelBooking.Domain;

namespace TravelBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Hash & Salt
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        // Role
        public string Role { get; set; } = "User";

        // Soft Delete (BaseEntity'den de geliyor, ama override etmiyoruz)
        public bool IsDeleted { get; set; } = false;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
