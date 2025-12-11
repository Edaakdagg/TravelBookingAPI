using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Domain
{
    /// <summary>
    /// Base class for all entities in the domain, providing common fields like ID, timestamps, and soft delete flag.
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Yumuşak Silme (Soft Delete) özelliği.
        public bool IsDeleted { get; set; } = false;
    }
}