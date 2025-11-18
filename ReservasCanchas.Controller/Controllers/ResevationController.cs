using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ResevationController : ControllerBase
    {
        private readonly ReservationBusinessLogic _reservationBusinessLogic;

        public ResevationController(ReservationBusinessLogic reservationBusinessLogic)
        {
            _reservationBusinessLogic = reservationBusinessLogic;
        }

        // Usuario ver sus reservas 
        [HttpGet]
        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetAllReservationsByUserId()
        {
            int userId = 1; //GetUserIdFromToken()
            var result = await _reservationBusinessLogic.GetReservationsForUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<ReservationForUserResponseDTO>> GetReservationById([FromRoute] int reservationId)
        {
            var result = await _reservationBusinessLogic.GetReservationsByIdAsync(reservationId);
            return Ok(result);
        }

        // SuperUser puede ver las reservas de cualquier complejo, o ComplexAdmin puede ver todas las reservas SOLO de sus complejos
        [HttpGet("/complex/{complexId}")]
        public async Task<ActionResult<List<ReservationForComplexResponseDTO>>> GetAllReservationsByIdComplex([FromRoute] int complexId)
        {
            int userId = 1; //GetUserIdFromToken()
            var result = await _reservationBusinessLogic.GetReservationsForComplexAsync(userId, complexId);
            return Ok(result);
        }

        // SuperUser puede ver las reservas de una cancha en particular de un complejo 'X', o ComplexAdmin puede ver las reservas de una cancha de un complejo suyo
        [HttpGet("complex/{complexId}/field/{fieldId}")]
        public async Task<ActionResult<List<ReservationForFieldResponseDTO>>> GetAllReservationsByIdField([FromRoute] int complexId, [FromRoute] int fieldId)
        {
            var result = await _reservationBusinessLogic.GetReservationsForFieldAsync(complexId, fieldId); 
            return Ok(result);
        }

        [HttpGet("complex/{complexId}")]
        public async Task<ActionResult<DayAvailabilityResponseDTO>> GetReservationsForDays([FromRoute] int complexId, [FromBody] ReservationForDayRequest reservationRequest)
        {
            var reservationsAndRecurridBLocks = await _reservationBusinessLogic.GetReservationsForDaysAsync(complexId, reservationRequest);
            return Ok(reservationsAndRecurridBLocks);
        }


        [HttpPost("complex/{complexId}/field/{fieldId}")]
        public async Task<ActionResult<CreateReservationResponseDTO>> CreateReservation([FromRoute] int complexId, [FromRoute] int fieldId, [FromBody] CreateReservationRequestDTO request)
        {
            var reservationCreated = await _reservationBusinessLogic.CreateReservationAsync(complexId, fieldId, request);
            //return Ok(reservationCreated);
            return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservationCreated.ReservationId }, reservationCreated); 
        }

        [HttpDelete("{reservationId}/cancelReservation")]
        public async Task<ActionResult> CancelReservationById([FromRoute] int reservationId)
        {
            await _reservationBusinessLogic.CancelReservationByIdAsync(reservationId);
            return NoContent();
        }
    }
}
