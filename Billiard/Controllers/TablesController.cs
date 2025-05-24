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
        [Authorize]

        public async Task<ActionResult<Table>> AddTable([FromBody] Table table)
        {
            await _tableService.AddAsync(table);
            return CreatedAtAction(nameof(GetTableById), new { id = table.TableId }, table);
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateTable(int id, [FromBody] Table table)
        {
            if (id != table.TableId)
                return BadRequest();

            await _tableService.UpdateAsync(table);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableService.RemoveAsync(id);
            return NoContent();
        }
    }
}
