using Billiard.Models;
using Billiard.Services.Shift;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftAssignment>>> GetAll()
        {
            var services = await _shiftService.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftAssignment>> GetById(int id)
        {
            var service = await _shiftService.GetByIdAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<ShiftAssignment>> Add([FromBody] DTO.ShiftModel service)
        {
            ShiftAssignment shiftAssignment = new ShiftAssignment()
            {
                EmployeeId = service.EmployeeId,
                ShiftId = service.ShiftId,
                Day = service.Day,
                Status = "Pending",
            };
            await _shiftService.AddAsync(shiftAssignment);
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DTO.ShiftModel service)
        {
            if (service.Id != service.Id)
                return BadRequest();
            ShiftAssignment shiftAssignment = await _shiftService.GetByIdAsync(service.Id);

            shiftAssignment.EmployeeId = service.EmployeeId;
            shiftAssignment.ShiftId = service.ShiftId;
            shiftAssignment.Day = service.Day;
            shiftAssignment.Status = service.Status;
            
            await _shiftService.UpdateAsync(shiftAssignment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shiftService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateShiftAssignmentStatus(int id, [FromBody] string updateStatusDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _shiftService.UpdateStatusAsync(id, updateStatusDto);

                if (result)
                {
                    return Ok(new { message = "Thay đổi thành công" });
                }

                return BadRequest(new { message = "chịu chết " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật trạng thái ca làm việc");
            }
        }
    }
}
