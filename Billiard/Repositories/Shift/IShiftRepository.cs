using Billiard.Models;
using Billiard.Repositories.IBaseRepository;

namespace Billiard.Repositories.Shift
{
    public interface IShiftRepository : IBaseRepository<ShiftAssignment>
    {
        Task<bool> UpdateStatusAsync(int id, string status);
    }
}
