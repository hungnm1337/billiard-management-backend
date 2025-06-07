// UpdateQuantityDto.cs
using System.ComponentModel.DataAnnotations;

namespace Billiard.DTOs
{
    public class UpdateQuantityDto
    {
        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int Quantity { get; set; }
    }

    public class ChangeQuantityDto
    {
        [Required(ErrorMessage = "Số lượng thay đổi là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng thay đổi phải lớn hơn 0")]
        public int Amount { get; set; }
    }
}

// ServiceResult.cs
namespace Billiard.DTOs
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ServiceResult<T> Success(T data, string message = "")
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult<T> Failure(string message, List<string>? errors = null)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }

    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public static ServiceResult Success(string message = "")
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ServiceResult Failure(string message, List<string>? errors = null)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
