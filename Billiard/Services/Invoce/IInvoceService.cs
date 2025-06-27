using Billiard.DTO;
using Billiard.Models;

namespace Billiard.Services.Invoce
{
    public interface IInvoceService
    {
        Task<int> addInvoce(CreateInvoice invoice);

        Task<bool> updateInvoice(InvoiceUpdateModel invoice);


        Task<bool> SaveServiceOfTable(ServiceOfTableModel servicesOfTable);

        Task<IEnumerable<Invoice>> GetInvoices();
    }
}
