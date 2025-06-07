using Billiard.Models;
using Billiard.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesService _serviceService;

        public ServiceController(IServicesService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetById(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<Service>> Add([FromBody] Service service)
        {
            await _serviceService.AddAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = service.ServiceId }, service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Service service)
        {
            if (id != service.ServiceId)
                return BadRequest();

            await _serviceService.UpdateAsync(service);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.RemoveAsync(id);
            return NoContent();
        }
    }

}
