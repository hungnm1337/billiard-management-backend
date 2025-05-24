using Billiard.Services.BaseService;
using Billiard.Repositories.Table;
using Billiard.Repositories.BaseRepository;

namespace Billiard.Services.Table
{
    public class TableService : BaseService<Models.Table>, ITableService
    {
        public TableService(IBaseRepository<Models.Table> repository) : base(repository)
        {
        }
    }
}
