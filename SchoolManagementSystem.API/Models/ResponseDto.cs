namespace SchoolManagementSystem.API.Models
{
    public class ResponseDto
    {
        public Object? data { get; set; }
        public string? message { get; set; } = String.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
