
using Billiard.DTOs;
using Billiard.Repositories.IBaseRepository;
using Billiard.Repositories.Services;
using Billiard.Services.BaseService;
using Billiard.Models;
namespace Billiard.Services.Service
{
    public class ServicesService : BaseService<Models.Service>, IServicesService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServicesService(IBaseRepository<Models.Service> repository, IServiceRepository serviceRepository) : base(repository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceResult<IEnumerable<Models.Service>>> GetAllServicesAsync()
        {
            try
            {
                var services = await _serviceRepository.GetAllAsync();
                return ServiceResult<IEnumerable<Models.Service>>.Success(services, "Lấy danh sách dịch vụ thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Models.Service>>.Failure("Đã xảy ra lỗi khi lấy danh sách dịch vụ");
            }
        }

        public async Task<ServiceResult<Models.Service>> GetServiceByIdAsync(int id)
        {
            try
            {
                var service = await _serviceRepository.GetByIdAsync(id);
                if (service == null)
                {
                    return ServiceResult<Models.Service>.Failure("Không tìm thấy dịch vụ");
                }

                return ServiceResult<Models.Service>.Success(service, "Lấy thông tin dịch vụ thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<Models.Service>.Failure("Đã xảy ra lỗi khi lấy thông tin dịch vụ");
            }
        }

        public async Task<ServiceResult> UpdateQuantityAsync(int id, UpdateQuantityDto updateQuantityDto)
        {
            try
            {
                var exists = await _serviceRepository.ExistsAsync(id);
                if (!exists)
                {
                    return ServiceResult.Failure("Không tìm thấy dịch vụ");
                }

                var success = await _serviceRepository.UpdateQuantityAsync(id, updateQuantityDto.Quantity);
                if (success)
                {
                    return ServiceResult.Success("Cập nhật số lượng thành công");
                }

                return ServiceResult.Failure("Cập nhật số lượng thất bại");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Đã xảy ra lỗi khi cập nhật số lượng");
            }
        }

        public async Task<ServiceResult> IncreaseQuantityAsync(int id, ChangeQuantityDto changeQuantityDto)
        {
            try
            {
                var exists = await _serviceRepository.ExistsAsync(id);
                if (!exists)
                {
                    return ServiceResult.Failure("Không tìm thấy dịch vụ");
                }

                var success = await _serviceRepository.IncreaseQuantityAsync(id, changeQuantityDto.Amount);
                if (success)
                {
                    return ServiceResult.Success($"Tăng số lượng thành công (+{changeQuantityDto.Amount})");
                }

                return ServiceResult.Failure("Tăng số lượng thất bại");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Đã xảy ra lỗi khi tăng số lượng");
            }
        }

        public async Task<ServiceResult> DecreaseQuantityAsync(int id, ChangeQuantityDto changeQuantityDto)
        {
            try
            {
                var service = await _serviceRepository.GetByIdAsync(id);
                if (service == null)
                {
                    return ServiceResult.Failure("Không tìm thấy dịch vụ");
                }

                if (service.Quantity < changeQuantityDto.Amount)
                {
                    return ServiceResult.Failure($"Số lượng không đủ. Hiện tại: {service.Quantity}, yêu cầu giảm: {changeQuantityDto.Amount}");
                }

                var success = await _serviceRepository.DecreaseQuantityAsync(id, changeQuantityDto.Amount);
                if (success)
                {
                    return ServiceResult.Success($"Giảm số lượng thành công (-{changeQuantityDto.Amount})");
                }

                return ServiceResult.Failure("Giảm số lượng thất bại");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Đã xảy ra lỗi khi giảm số lượng");
            }
        }
    }
}

