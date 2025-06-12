using Billiard.DTO;

namespace Billiard.Repositories.Invoce
{
    public interface IInvoceRepository
    {
        Task<int> addInvoce(CreateInvoice invoice);

        Task<bool> updateInvoice(InvoiceUpdateModel invoice);

        Task<bool> SaveServiceOfTable(ServiceOfTableModel servicesOfTable);


    }
}
