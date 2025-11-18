using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ReservationBusinessLogic
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly UsuarioBusinessLogic _usurioBusinessLogic;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ComplexRepository _complexRepository;
        private readonly ComplexBusinessLogic _complexBusinessLogic;

        public ReservationBusinessLogic(ReservationRepository reservationRepository, UsuarioBusinessLogic usurioBusinessLogic, UsuarioRepository usuarioRepository, ComplexRepository complexRepository, ComplexBusinessLogic complexBusinessLogic)
        {
            _reservationRepository = reservationRepository;
            _usurioBusinessLogic = usurioBusinessLogic;
            _usuarioRepository = usuarioRepository;
            _complexRepository = complexRepository;
            _complexBusinessLogic = complexBusinessLogic;
        }

        public async Task<List<ReservationForComplexResponseDTO>> GetReservationsForComplexAsync(int userId, int complexId)
        {
            var user = await _usuarioRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + userId + " no encontrado");
            }

            var complex = await _complexBusinessLogic.ComplexValidityExistenceCheck2(complexId);

            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();

            if(userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);
            

            var reservations = complex.Fields
                .SelectMany(f => f.Reservations)
                .ToList();

            var dtoList = reservations.Select(ReservationMapper.ToReservationForComplexResponseDTO).ToList();

            return dtoList;

        }

        public async Task<DayAvailabilityResponseDTO> GetReservationsForDaysAsync(int complexId, ReservationForDayRequest reservationRequest)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();
            
            // Esto me da un usuario Dto, que si no existe me tira solo el error
            var user = await _usurioBusinessLogic.GetById(userId);

            //si no me lo traigo del repositorio no puedo validarlo con ComplexValidityAdmin() porque toma un Complex, no el DTO que devuelve GetComplexByIdAsync()
            var complex = await _complexRepository.GetComplexByIdWithReservationsAsync(complexId);

            if (complex == null)
                throw new BadRequestException($"No existe un complejo con el id {complexId}");

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            // vaido la fecha
            if (reservationRequest.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BadRequestException("La fecha no puede ser anterior al día actual.");

            // Filtrar por canchas activas, tipo de cancha y tipo de suelo
            var validFields = complex.Fields
                .Where(f =>
                    f.Active &&
                    f.FieldType == reservationRequest.FieldType &&
                    f.FloorType == reservationRequest.FloorType)
                .ToList();

            if (!validFields.Any())
                throw new NotFoundException(
                    $"No hay canchas activas en este complejo con tipo {reservationRequest.FieldType} y piso {reservationRequest.FloorType}");

            WeekDay requestWeekDay = _complexBusinessLogic.ConvertToWeekDay(reservationRequest.Date);

            // Filtro las reservas del día
            var reservations = validFields
                .SelectMany(f => f.Reservations)
                .Where(r => r.Date == reservationRequest.Date)
                .ToList();

            // Filtro ls bloqueos recurrentes del día
            var recurringBlocks = validFields
                .SelectMany(f => f.RecurringCourtBlocks)
                .Where(b => b.WeekDay == requestWeekDay)
                .ToList();

            DayAvailabilityResponseDTO response = new DayAvailabilityResponseDTO
            {
                ComplexId = complex.Id,
                Date = reservationRequest.Date
            };

            response.Reservations = reservations.Select(ReservationMapper.ToDayReservationDTO).ToList();

            response.RecurringBlocks = recurringBlocks.Select(ReservationMapper.ToDayRecurringBlockDTO).ToList();

            return response;
        }

        public async Task<List<ReservationForFieldResponseDTO>> GetReservationsForFieldAsync(int complexId, int fieldId)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();
            
            var user = await _usurioBusinessLogic.GetById(userId);

            //si no me lo traigo del repositorio no puedo validarlo con ComplexValidityAdmin() porque toma un Complex, no el DTO que devuelve GetComplexByIdAsync()
            var complex = await _complexRepository.GetComplexByIdWithRelationsAsync(complexId);

            if (complex == null) 
                throw new BadRequestException($"No existe un complejo con el id {complexId}");


            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            var field = complex.Fields.FirstOrDefault(f => f.Id == fieldId);

            if (field == null)
                throw new NotFoundException($"La cancha con id {fieldId} no pertenece a este complejo");

            var reservations = field.Reservations.ToList();

            return reservations
                .Select(ReservationMapper.ToReservationForFieldResponseDTO)
                .ToList();
        }

        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetReservationsForUserAsync(int userId)
        {
            // el usuario tiene que existir
            var exist = await _usurioBusinessLogic.ExistUser(userId);

            var reservations = await _reservationRepository.GetReservationsByUserAsync(userId);

            var result = new List<ReservationForUserResponseDTO>();

            foreach (var r in reservations)
            { 
                var dtto = ReservationMapper.ToReservationForUserDTO(r);
                result.Add(dtto);
            }
            return result;
        }
    }
}
