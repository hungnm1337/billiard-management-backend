using Billiard.Models;
using Billiard.Repositories.BaseRepository;

namespace Billiard.Repositories.Table
{
    public class TableRepository : BaseRepository<Models.Table>, ITableRepository
    {
        public TableRepository(Prn232ProjectContext context) : base(context)
        {
        }
    }
}
