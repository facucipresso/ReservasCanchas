using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
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
        private readonly IWebHostEnvironment _env; // para WebRootPath

        public ReservationController(ReservationBusinessLogic reservationBusinessLogic, IWebHostEnvironment env)
        {
            _reservationBusinessLogic = reservationBusinessLogic;
            _env = env;
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
        public async Task<ActionResult<List<ReservationResponseDTO>>> GetAllReservationsByComplexId([FromRoute] int complexId)
        {
            var result = await _reservationBusinessLogic.GetReservationsByComplexIdAsync(complexId);
            return Ok(result);
        }

        // SuperUser puede ver las reservas de una cancha en particular de un complejo 'X', o ComplexAdmin puede ver las reservas de una cancha de un complejo suyo
        [HttpGet("field/{fieldId}")]
        public async Task<ActionResult<List<ReservationResponseDTO>>> GetAllReservationsByFieldId([FromRoute] int fieldId)
        {
            var result = await _reservationBusinessLogic.GetReservationsByFieldIdAsync(fieldId);
            return Ok(result);
        }

        [HttpGet("complex/{complexId}/by-date")]
        public async Task<ActionResult<DailyReservationsForComplexResponseDTO>> GetReservationsByDateForComplex([FromRoute] int complexId, [FromQuery] DateOnly date)
        {
            var reservationsAndRecurringBLocks = await _reservationBusinessLogic.GetReservationsByDateForComplexAsync(complexId, date, true);
            return Ok(reservationsAndRecurringBLocks);
        }

        [HttpGet("complex/{complexId}/search")]
        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetReservationsByComplexAndDate([FromRoute] int complexId, [FromQuery] DateOnly date)
        {
            var reservations = await _reservationBusinessLogic.GetReservationsByComplexAndDateAsync(complexId, date);
            return Ok(reservations);
        }

        [HttpGet("field/{fieldId}/search")]
        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetReservationsByFieldAndDate([FromRoute] int fieldId, [FromQuery] DateOnly date)
        {
            var reservations = await _reservationBusinessLogic.GetReservationsByFieldAndDateAsync(fieldId, date);
            return Ok(reservations);
        }

        [HttpGet("process/{processId}")]
        public async Task<ActionResult<CheckoutInfoDTO>> GetCheckoutInfoByProcessId([FromRoute] string processId)
        {
            var checkoutInfo = await _reservationBusinessLogic.GetCheckoutInfoAsync(processId);
            return Ok(checkoutInfo);
        }

        [HttpPost("process")]
        public async Task<ActionResult<string>> CreateProcessReservation([FromBody] ReservationProcessRequestDTO request)
        {
            var reservationProcessId = await _reservationBusinessLogic.CreateReservationProcessAsync(request);
            return Ok(reservationProcessId);
        }

        [HttpDelete("process/{processId}")]
        public async Task<ActionResult> DeleteProcessReservation([FromRoute] string processId)
        {
            await _reservationBusinessLogic.DeleteReservationProcessAsync(processId);
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<CreateReservationResponseDTO>> CreateReservation([FromForm] CreateReservationRequestDTO request)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "reservations");
            var reservationCreated = await _reservationBusinessLogic.CreateReservationAsync(request, uploadPath);
            return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservationCreated.ReservationId }, reservationCreated);
        }

        [HttpPatch("{reservationId}/state")]
        public async Task<ActionResult> ChangeStateReservation ([FromRoute] int reservationId, ChangeStateReservationRequestDTO request)
        {
            await _reservationBusinessLogic.ChangeStateReservationAsyncccc(reservationId, request); 
            return NoContent();
        }

        //notifico la aprobacion de la reserva
        [HttpPatch("approveReservation")]
        public async Task<IActionResult> ApproveReservation([FromBody] ApproveReservationRequestDTO request)
        {
            await _reservationBusinessLogic.ApproveReservationAsync(request);
            return NoContent();
        }

        //notifico el rechazo de la reserva
        [HttpPatch("rejectReservation")]
        public async Task<IActionResult> RejectReservation([FromBody] RejectReservationRequestDTO request)
        {
            await _reservationBusinessLogic.RejectReservationAsync(request);
            return NoContent();
        }

        [HttpGet("{reservationId}/detail")]
        public async Task<ActionResult<ReservationDetailResponseDTO>> GetReservationDetail([FromRoute] int reservationId)
        {
            var result = await _reservationBusinessLogic.GetReservationDetailAsync(reservationId);

            return Ok(result);
        }




    }
}
