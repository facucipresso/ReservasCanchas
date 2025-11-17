using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.BusinessLogic.Exceptions;
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

        public ReservationBusinessLogic(ReservationRepository reservationRepository, UsuarioBusinessLogic usurioBusinessLogic, UsuarioRepository usuarioRepository)
        {
            _reservationRepository = reservationRepository;
            _usurioBusinessLogic = usurioBusinessLogic;
            _usuarioRepository = usuarioRepository;
        }

        public async Task GetReservationsForComplexAsync(int userId, int complexId)
        {
            var user = await _usuarioRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + userId + " no encontrado");
            }

            bool isSuperUser = user.Rol == Rol.SuperAdmin ? true : false;

            var complex = await _
        }

        public async Task<ActionResult<ReservationsForUserResponseDTO>> GetReservationsForUserAsync(int userId)
        {
            // el usuario tiene que existir
            var exist = await _usurioBusinessLogic.ExistUser(userId);

            var reservations = await _reservationRepository.GetReservationsByUserAsync(userId);

            var result = new ReservationsForUserResponseDTO();

            foreach (var r in reservations)
            {
                var dto = new ReservationForUserDTO
                {
                    ReservationId = r.Id,
                    Date = r.Date,
                    InitTime = r.InitTime,
                    State = r.ReservationState,
                    ComplexName = r.Field.Complex.Name,
                    FieldName = r.Field.Name,
                    TotalPrice = r.TotalPrice,
                    PricePaid = r.PricePaid,
                    CanReview = r.ReservationState == ReservationState.Completada &&
                        r.Review == null
                };
                // si la reserva es para hoy o futura
                if (r.Date >= DateOnly.FromDateTime(DateTime.Today))
                    result.Upcoming.Add(dto);
                else
                    // la reserva ya es del pasado
                    result.History.Add(dto);
            }
            return result;
        }
    }
}
