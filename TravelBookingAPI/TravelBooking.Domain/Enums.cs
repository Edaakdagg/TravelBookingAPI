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

    /// <summary>
    /// Rezervasyonun mevcut durumunu tanımlar.
    /// </summary>
    public enum ReservationStatus
    {
        Pending = 1, // Beklemede (Ödeme veya onay bekleniyor)
        Confirmed = 2, // Onaylandı (Rezervasyon kesinleşti)
        Cancelled = 3, // İptal Edildi
        Completed = 4 // Tamamlandı (Çıkış yapıldı)
    }
}