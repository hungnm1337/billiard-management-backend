using Billiard.Models;
using Billiard.Repositories.BaseRepository;

namespace Billiard.Repositories.Services
{
    public class ServiceRepository : BaseRepository<Models.Service>, IServiceRepository
    {
        public ServiceRepository(Prn232ProjectContext context) : base(context)
        {
        }
    }
}
