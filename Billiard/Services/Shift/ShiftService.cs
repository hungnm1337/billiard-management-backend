using Billiard.Models;
using Billiard.Repositories.IBaseRepository;
using Billiard.Repositories.Shift;
using Billiard.Services.BaseService;

namespace Billiard.Services.Shift
{
    public class ShiftService : BaseService<Models.ShiftAssignment>, IShiftService
    {
        private IShiftRepository shiftRepository;
        public ShiftService(IBaseRepository<ShiftAssignment> repository, IShiftRepository repo) : base(repository)
        {
            shiftRepository = repo;
        }

        public Task<bool> UpdateStatusAsync(int id, string status)
        {
            return shiftRepository.UpdateStatusAsync(id, status);
        }
    }
}
