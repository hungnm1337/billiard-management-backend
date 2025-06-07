using Billiard.Models;
using Billiard.Repositories.IBaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Services
{
    public class ServiceRepository : BaseRepository<Models.Service>, IServiceRepository
    {
        private readonly Prn232ProjectContext _context;
        public ServiceRepository(Prn232ProjectContext context) : base(context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services
                .OrderBy(s => s.ServiceName)
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == id);
        }

        public async Task<bool> UpdateQuantityAsync(int id, int quantity)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return false;

            if (quantity < 0)
                return false;

            service.Quantity = quantity;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IncreaseQuantityAsync(int id, int amount)
        {
            if (amount <= 0)
                return false;

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return false;

            service.Quantity += amount;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DecreaseQuantityAsync(int id, int amount)
        {
            if (amount <= 0)
                return false;

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return false;

            // Không cho phép số lượng âm
            if (service.Quantity < amount)
                return false;

            service.Quantity -= amount;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Services.AnyAsync(s => s.ServiceId == id);
        }
    }
}
