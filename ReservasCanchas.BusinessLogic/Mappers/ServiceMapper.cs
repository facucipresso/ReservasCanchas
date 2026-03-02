using ReservasCanchas.BusinessLogic.Dtos.Service;
using ReservasCanchas.Domain.Entities;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class ServiceMapper
    {
        public static ServiceResponseDTO ToServiceResponseDTO(Service service)
        {
            return new ServiceResponseDTO
            {
                Id = service.Id,
                ServiceDescription = service.ServiceDescription
            };
        }

        public static Service ToService(ServiceRequestDTO serviceDto)
        {
            return new Service { ServiceDescription = serviceDto.ServiceDescription };
        }
    }
}
