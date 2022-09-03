using Bot.DTO;

namespace App.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public List<ApiError>? Errors { get; set; }
    }
}