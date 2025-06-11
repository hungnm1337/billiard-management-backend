using Billiard.Models;
using Billiard.Services.BaseService;
namespace Billiard.Services.Table
{
    public interface ITableService : IBaseService<Models.Table>
    {
        Task<int> BookingTableAsync(DTO.BookingTableModel model);
        Task<bool> ChangeStatusTableAsync(int orderId, string oldStatus, string newStatus);
        Task<int> GettableIdFromOrderId(int orderid);
        Task<bool> ChangeStatusTableByIdAsync(int tableId, string newStatus);

        Task<IEnumerable<Models.Table>> GetTablesOpening();

    }
}
