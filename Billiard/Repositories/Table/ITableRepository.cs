using Billiard.Repositories.IBaseRepository;

namespace Billiard.Repositories.Table
{
    public interface ITableRepository : IBaseRepository<Models.Table>
    {
         Task<int> BookingTableAsync(DTO.BookingTableModel model);
         Task<bool> ChangeStatusTableAsync(int OrderId, string oldStatus, string newStatus);
         Task<bool> ChangeStatusTableByIdAsync(int tableId, string newStatus);
        Task<int> GettableIdFromOrderId(int orderid);

        Task<IEnumerable<Models.Table>> GetTablesOpening();

        Task<bool> ChangeStatusTableByInvoiceIdAsync(int invoiceId, string newStatus);

    }
}
