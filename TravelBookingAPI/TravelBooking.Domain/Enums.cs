namespace TravelBooking.Domain.Enums
{
    /// <summary>
    /// Otel odası tiplerini tanımlar.
    /// </summary>
    public enum RoomType
    {
        Single = 1,
        Double = 2,
        Suite = 3,
        Deluxe = 4
    }

    
    public enum ReservationStatus
    {
        Pending = 1, 
        Confirmed = 2, 
        Cancelled = 3, 
        Completed = 4 
    }
}