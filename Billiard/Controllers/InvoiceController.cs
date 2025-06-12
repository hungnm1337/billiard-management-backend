using Billiard.DTO;
using Billiard.Models;
using Billiard.Services.BaseService;
using Billiard.Services.Invoce;
using Billiard.Services.Table;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public InvoiceController(IInvoceService invoceService, IBaseService<Models.Table> table,ITableService tb) {
        _invoceService = invoceService;
        _table = table;
        _tableService = tb;
        }

        private readonly IInvoceService _invoceService;
        private readonly IBaseService<Models.Table> _table;
        private readonly ITableService _tableService;
        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody]CreateInvoice invoice)
        {
            try {

                int InvoiceId = await _invoceService.addInvoce(invoice);
                if (InvoiceId == 0)
                {
                    return NotFound();
                }
                else
                {               
                    bool r = await _tableService.ChangeStatusTableByIdAsync(invoice.TableId, "Đang sử dụng");
                    if (r)
                    {
                        return Ok(InvoiceId);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            } catch (Exception ex) {
            
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceUpdateModel invoiceData)
        {
            try
            {
                // Validate input
                if (invoiceData == null)
                {
                    return BadRequest("Invoice data is required");
                }

                invoiceData.InvoiceId = id;

                bool resultUpdateInvoice = await _invoceService.updateInvoice(invoiceData);
                if (resultUpdateInvoice)
                {
                    bool resultChangeStatusTable = await _tableService.ChangeStatusTableByInvoiceIdAsync(invoiceData.InvoiceId, "Đang trống");
                    if (resultChangeStatusTable)
                    {
                        return Ok(invoiceData.InvoiceId);
                    }
                    else
                    {
                        return BadRequest("Không thay đổi được trạng thái bàn");
                    }
                }
                return BadRequest("Không thay đổi được thông tin hóa đơn");
            }
            catch (Exception ex)
            {
                // Log error here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("service")]
        public async Task<IActionResult> SaveServiceOfTable([FromBody] ServiceOfTableModel serviceOfTable)
        {
            try
            {
                bool rerultOfSaveServiceOfTable = await _invoceService.SaveServiceOfTable(serviceOfTable);
                if (rerultOfSaveServiceOfTable)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
