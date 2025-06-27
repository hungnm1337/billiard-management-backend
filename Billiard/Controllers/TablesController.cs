using Billiard.DTO;
using Billiard.Models;
using Billiard.Services.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
        {
            var tables = await _tableService.GetAllAsync();
            return Ok(tables);
        }

        [HttpGet("open")]
        public async Task<ActionResult<IEnumerable<Table>>> GetTablesOpening()
        {
            var tables = await _tableService.GetTablesOpening();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<Table>> GetTableById(int id)
        {
            var table = await _tableService.GetByIdAsync(id);
            if (table == null)
                return NotFound();
            return Ok(table);
        }

        [HttpPost]

        public async Task<ActionResult<Table>> AddTable([FromBody] Table table)
        {
            await _tableService.AddAsync(table);
            return CreatedAtAction(nameof(GetTableById), new { id = table.TableId }, table);
        }

        [HttpPut("{id}")]       

        public async Task<IActionResult> UpdateTable(int id, [FromBody] UpdateTableDto table)
        {
            if (id != table.TableId)
                return BadRequest();

            await _tableService.UpdateTable(table);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("booking")]
        public async Task<IActionResult> BookingTable([FromBody] BookingTableModel model)
        {
            var result = await _tableService.BookingTableAsync(model);
            if (result != -1)
                return Ok(result);
            return BadRequest(new { message = "Đặt bàn thất bại!" });
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeStatusTable([FromBody] ChangeStatusTableRequest request)
        {
            var result = await _tableService.ChangeStatusTableAsync(request.TableId, request.OldStatus, request.NewStatus);
            if (result)
                return Ok(new { message = "Đổi trạng thái bàn thành công!" });
            return BadRequest(new { message = "Đổi trạng thái bàn thất bại!" });
        }
    }
    public class ChangeStatusTableRequest
    {
        public int TableId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
    }
}
