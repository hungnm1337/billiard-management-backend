using Billiard.DTO;
using Billiard.Models;
using Billiard.Repositories.IBaseRepository;
using MailKit.Search;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Table
{
    public class TableRepository : BaseRepository<Models.Table>, ITableRepository
    {
        private readonly Prn232ProjectContext _conext;

        public TableRepository(Prn232ProjectContext context) : base(context)
        {
            _conext = context;
        }

        public async Task<bool> ChangeStatusTableAsync(int orderid, string oldStatus, string newStatus)
        {
            try
            {
                var order = await _conext.OrderTables.FirstOrDefaultAsync(x => x.Id == orderid);
                var table = await _conext.Tables.FirstOrDefaultAsync(x => x.TableId == order.TableId);
                if (table == null)
                {
               
                    return false; // Bàn không tồn tại
                }
                if (table == null)
                    return false;

                if (table.Status?.Trim() != oldStatus?.Trim())
                    return false;

                table.Status = newStatus;
                _conext.Tables.Update(table);
                await _conext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<int> BookingTableAsync(DTO.BookingTableModel model)
        {
            using var transaction = await _conext.Database.BeginTransactionAsync();
            try
            {
               

                var order = new OrderTable
                {
                    TableId = model.TableId,
                    UserId = model.UserId,
                    Time = model.Time
                };

                await _conext.OrderTables.AddAsync(order);
                await _conext.SaveChangesAsync();

                await transaction.CommitAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return -1;
            }
        }

        public async Task<int> GettableIdFromOrderId(int orderid)
        {
            var order = await _context.OrderTables.FirstOrDefaultAsync(x => x.Id == orderid);

            return order.TableId; 
        }

    }
}
