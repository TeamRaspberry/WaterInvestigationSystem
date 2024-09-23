namespace AuthService.Core.DTOs
{
    public class OperationStatus
    {
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
