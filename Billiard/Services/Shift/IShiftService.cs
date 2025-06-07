
using Billiard.Services.BaseService;

namespace Billiard.Services.Shift
{
    public interface IShiftService : IBaseService<Models.ShiftAssignment>
    {
        Task<bool> UpdateStatusAsync(int id, string status);

    }
}
