using Billiard.DTO;
using Billiard.Models;
using Billiard.Repositories.IBaseRepository;

namespace Billiard.Repositories.Services
{
    public interface IServiceRepository : IBaseRepository<Models.Service>
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<bool> UpdateQuantityAsync(int id, int quantity);
        Task<bool> IncreaseQuantityAsync(int id, int amount);
        Task<bool> DecreaseQuantityAsync(int id, int amount);
        Task<bool> ExistsAsync(int id);

        Task<bool> CreateService(ServiceModel service);

        Task<bool> UpdateService(ServiceModel service);
    }
}
