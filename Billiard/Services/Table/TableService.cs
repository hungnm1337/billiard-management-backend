﻿using Billiard.Services.BaseService;
using Billiard.Repositories.Table;
using Billiard.Repositories.IBaseRepository;
using Billiard.DTO;

namespace Billiard.Services.Table
{
    public class TableService : BaseService<Models.Table>, ITableService
    {
        private readonly ITableRepository _tableRepository;

        public async Task UpdateTable(UpdateTableDto model)
        {
             await _tableRepository.UpdateTable(model);
        }
        public TableService(IBaseRepository<Models.Table> repository, ITableRepository tableRepository) : base(repository)
        {
            _tableRepository = tableRepository; 
        }

        public async Task<int> BookingTableAsync(BookingTableModel model)
        {
            return await _tableRepository.BookingTableAsync(model);
        }

        public async Task<bool> ChangeStatusTableAsync(int tableId, string oldStatus, string newStatus)
        {
            return await _tableRepository.ChangeStatusTableAsync(tableId, oldStatus, newStatus);
        }

        public async Task<bool> ChangeStatusTableByIdAsync(int tableId, string newStatus)
        {
            return await _tableRepository.ChangeStatusTableByIdAsync(tableId, newStatus);
        }

        public async Task<bool> ChangeStatusTableByInvoiceIdAsync(int invoiceId, string newStatus)
        {
            return await _tableRepository.ChangeStatusTableByInvoiceIdAsync(invoiceId, newStatus);
        }

        public async Task<int> GettableIdFromOrderId(int orderid)
        {
            return await _tableRepository.GettableIdFromOrderId(orderid);
        }

        public async Task<IEnumerable<Models.Table>> GetTablesOpening()
        {
            return await _tableRepository.GetTablesOpening();
        }
    }
}
