using Billiard.Models;
using Billiard.Repositories.IBaseRepository;

namespace Billiard.Repositories.Services
{
    public class ServiceRepository : BaseRepository<Models.Service>, IServiceRepository
    {
        public ServiceRepository(Prn232ProjectContext context) : base(context)
        {
        }
    }
}
