using Billiard.DTO;
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
        public InvoiceController(IInvoceService invoceService, IBaseService<Table> table,ITableService tb) {
        _invoceService = invoceService;
        _table = table;
        _tableService = tb;
        }

        private readonly IInvoceService _invoceService;
        private readonly IBaseService<Table> _table;
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
    }
}
