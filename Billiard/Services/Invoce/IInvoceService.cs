using Billiard.DTO;

namespace Billiard.Services.Invoce
{
    public interface IInvoceService
    {
        Task<int> addInvoce(CreateInvoice invoice);

    }
}
