using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ComplexBusinessLogic
    {
        private readonly ComplexRepository _complexRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public ComplexBusinessLogic(ComplexRepository complexRepository)
        {
            _complexRepository = complexRepository;
        }

        public async Task<List<ComplexCardResponseDTO>> GetComplexesForAdminComplexIdAsync(int adminComplexId)
        {
            List<Complejo> complexes = await _complexRepository.GetComplexesByUserIdAsync(adminComplexId);
            var complexescardsdto = complexes.Select(ComplexMapper.toComplexCardResponseDTO).ToList();
            return complexescardsdto;
        }

        public async Task<ComplexDetailResponseDTO> CreateComplexAsync(CreateComplexRequestDTO complexDTO)
        {
            var complex = ComplexMapper.toComplex(complexDTO);

            if (complexDTO.ServicesIds.Any())
            {
                var services = await _serviceRepository.GetServicesByIdsAsync((List<int>)complexDTO.ServicesIds);
                complex.Services = services;
            }

            if (complexDTO.TimeSlots.Select(ts => ts.WeekDay).Count() != complexDTO.TimeSlots.Count())
            {
                throw new BadRequestException("No se pueden repetir dias de la semana en los horarios del complejo");
            }

            if(await _usuarioRepository.GetUserByIdAsync(complexDTO.UserId) == null)
            {
                throw new NotFoundException($"No se encontró el usuario con id {complexDTO.UserId} asociado al complejo");
            }

            if(await _complexRepository.ExistsByNameAsync(complexDTO.Name))
            {
                throw new BadRequestException($"Ya existe un complejo con el nombre {complexDTO.Name}");
            }

            if(await _complexRepository.ExistsByAddressAsync(complexDTO.Street,complexDTO.Number,complexDTO.Locality, complexDTO.Province))
            {
                throw new BadRequestException($"Ya existe un complejo en la dirección {complexDTO.Street} {complexDTO.Number}, {complexDTO.Locality}, {complexDTO.Province}");
            }

            if(await _complexRepository.ExistsByPhoneAsync(complexDTO.Phone))
            {
                throw new BadRequestException($"Ya existe un complejo con el teléfono {complexDTO.Phone}");
            }//podria haber complejos con el mismo numero de telefono?

            complex.Active = true;
            complex.State = ComplexState.Pendiente;
            await _complexRepository.CreateComplexAsync(complex);

            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }
    }
}
