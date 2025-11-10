using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class ServiceMapper
    {
        public static ServiceDto FromServiceToServiceDto(Service ser)
        {
            return new ServiceDto
            {
               ServiceDescription = ser.ServiceDescription
            };
        }

        public static Service FromServiceDtoToService(ServiceDto serviceDto)
        {
            return new Service { ServiceDescription = serviceDto.ServiceDescription };
        }
    }
}
