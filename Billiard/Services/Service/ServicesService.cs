
using Billiard.Repositories.BaseRepository;
using Billiard.Repositories.Services;
using Billiard.Services.BaseService;

namespace Billiard.Services.Service
{
    public class ServicesService : BaseService<Models.Service>, IServicesService
    {
        public ServicesService(IBaseRepository<Models.Service> repository) : base(repository)
        {
        }
    }
}
