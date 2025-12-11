namespace TravelBooking.Api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse(T? data = default, string message = "", bool success = true)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}