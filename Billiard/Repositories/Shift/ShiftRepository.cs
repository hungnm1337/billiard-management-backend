using Billiard.Models;
using Billiard.Repositories.IBaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Shift
{
    public class ShiftRepository : BaseRepository<ShiftAssignment>, IShiftRepository
    {
        private readonly Prn232ProjectContext _db;
        public ShiftRepository(Prn232ProjectContext context) : base(context)
        {
            _db = context;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var shift = await _db.ShiftAssignments.FirstOrDefaultAsync(x => x.Id == id);
            if (shift == null) { return false; }
            shift.Status = status;
            _db.ShiftAssignments.Update(shift);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
