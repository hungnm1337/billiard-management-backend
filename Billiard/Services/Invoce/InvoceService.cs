using Billiard.DTO;
using Billiard.Models;
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

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await _repository.GetInvoices();
        }

        public async Task<bool> SaveServiceOfTable(ServiceOfTableModel servicesOfTable)
        {
            return await _repository.SaveServiceOfTable(servicesOfTable);
        }

        public async Task<bool> updateInvoice(InvoiceUpdateModel invoice)
        {
            return await _repository.updateInvoice(invoice);
        }
    }
}
