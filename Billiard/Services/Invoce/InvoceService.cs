using Billiard.DTO;
using Billiard.Repositories.Invoce;

namespace Billiard.Services.Invoce
{
    public class InvoceService : IInvoceService
    {
        private readonly IInvoceRepository _repository;
        public InvoceService(IInvoceRepository repository) { 
        _repository = repository;
        }
        public async Task<int> addInvoce(CreateInvoice invoice)
        {
            return await _repository.addInvoce(invoice);
        }
    }
}
