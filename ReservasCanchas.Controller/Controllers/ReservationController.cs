using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationBusinessLogic _reservationBusinessLogic;

        public ReservationController(ReservationBusinessLogic reservationBusinessLogic)
        {
            _reservationBusinessLogic = reservationBusinessLogic;
        }

        // Usuario ver sus reservas 
        [HttpGet("my")]
        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetMyReservations()
        {
            var result = await _reservationBusinessLogic.GetReservationsByUserIdAsync();
            return Ok(result);
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<ReservationForUserResponseDTO>> GetReservationById([FromRoute] int reservationId)
        {
            var result = await _reservationBusinessLogic.GetReservationByIdAsync(reservationId);
            return Ok(result);
        }

        // SuperUser puede ver las reservas de cualquier complejo, o ComplexAdmin puede ver todas las reservas SOLO de sus complejos
        [HttpGet("complex/{complexId}")]
        public async Task<ActionResult<List<ReservationForComplexResponseDTO>>> GetAllReservationsByComplexId([FromRoute] int complexId)
        {
            var result = await _reservationBusinessLogic.GetReservationsByComplexIdAsync(complexId);
            return Ok(result);
        }

        // SuperUser puede ver las reservas de una cancha en particular de un complejo 'X', o ComplexAdmin puede ver las reservas de una cancha de un complejo suyo
        [HttpGet("field/{fieldId}")]
        public async Task<ActionResult<List<ReservationForFieldResponseDTO>>> GetAllReservationsByFieldId([FromRoute] int fieldId)
        {
            var result = await _reservationBusinessLogic.GetReservationsByFieldIdAsync(fieldId); 
            return Ok(result);
        }

        [HttpGet("complex/{complexId}/by-date")]
        public async Task<ActionResult<DailyReservationsForComplexResponseDTO>> GetReservationsByDateForComplex ([FromRoute] int complexId, [FromQuery] DateOnly date)
        {
            var reservationsAndRecurringBLocks = await _reservationBusinessLogic.GetReservationsByDateForComplexAsync(complexId, date);
            return Ok(reservationsAndRecurringBLocks);
        }


        [HttpPost]
        public async Task<ActionResult<CreateReservationResponseDTO>> CreateReservation([FromBody] CreateReservationRequestDTO request)
        {
            var reservationCreated = await _reservationBusinessLogic.CreateReservationAsync(request);
            return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservationCreated.ReservationId }, reservationCreated);
        }

        [HttpPatch("{reservationId}/state")]
        public async Task<ActionResult> ChangeStateReservation ([FromRoute] int reservationId, ChangeStateReservationRequestDTO request)
        {
            await _reservationBusinessLogic.ChangeStateReservationAsync(reservationId, request); 
            return NoContent();
        }
    }
}
