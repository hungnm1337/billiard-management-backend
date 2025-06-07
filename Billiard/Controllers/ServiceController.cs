using Billiard.DTOs;
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
        public async Task<IActionResult> GetAllServices()
        {
            var result = await _serviceService.GetAllServicesAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode(500, new { message = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Lấy dịch vụ theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var result = await _serviceService.GetServiceByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("Không tìm thấy"))
            {
                return NotFound(new { message = result.Message });
            }

            return StatusCode(500, new { message = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Cập nhật số lượng dịch vụ
        /// </summary>
        [HttpPut("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromBody] UpdateQuantityDto updateQuantityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceService.UpdateQuantityAsync(id, updateQuantityDto);

            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }

            if (result.Message.Contains("Không tìm thấy"))
            {
                return NotFound(new { message = result.Message });
            }

            return BadRequest(new { message = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Tăng số lượng dịch vụ
        /// </summary>
        [HttpPost("{id}/increase")]
        public async Task<IActionResult> IncreaseQuantity(int id, [FromBody] ChangeQuantityDto changeQuantityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceService.IncreaseQuantityAsync(id, changeQuantityDto);

            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }

            if (result.Message.Contains("Không tìm thấy"))
            {
                return NotFound(new { message = result.Message });
            }

            return BadRequest(new { message = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Giảm số lượng dịch vụ
        /// </summary>
        [HttpPost("{id}/decrease")]
        public async Task<IActionResult> DecreaseQuantity(int id, [FromBody] ChangeQuantityDto changeQuantityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceService.DecreaseQuantityAsync(id, changeQuantityDto);

            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }

            if (result.Message.Contains("Không tìm thấy"))
            {
                return NotFound(new { message = result.Message });
            }

            return BadRequest(new { message = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Cập nhật số lượng dịch vụ (phương thức PUT đơn giản)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateQuantityDto updateQuantityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceService.UpdateQuantityAsync(id, updateQuantityDto);

            if (result.IsSuccess)
            {
                // Trả về dịch vụ đã cập nhật
                var serviceResult = await _serviceService.GetServiceByIdAsync(id);
                return Ok(serviceResult.Data);
            }

            if (result.Message.Contains("Không tìm thấy"))
            {
                return NotFound(new { message = result.Message });
            }

            return BadRequest(new { message = result.Message, errors = result.Errors });
        }
    }

}

