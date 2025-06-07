using Billiard.DTOs;
using Billiard.Services.BaseService;

namespace Billiard.Services.Service
{
    public interface IServicesService : IBaseService<Models.Service>
    {
        Task<ServiceResult<IEnumerable<Models.Service>>> GetAllServicesAsync();
        Task<ServiceResult<Models.Service>> GetServiceByIdAsync(int id);
        Task<ServiceResult> UpdateQuantityAsync(int id, UpdateQuantityDto updateQuantityDto);
        Task<ServiceResult> IncreaseQuantityAsync(int id, ChangeQuantityDto changeQuantityDto);
        Task<ServiceResult> DecreaseQuantityAsync(int id, ChangeQuantityDto changeQuantityDto);
    }
}
