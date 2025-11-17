using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
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
        public async Task<ActionResult<ReservationsForUserResponseDTO>> GetAllReservationsByUserId()
        {
            int userId = 1; //GetUserIdFromToken()
            var result = await _reservationBusinessLogic.GetReservationsForUserAsync(userId);
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
        [HttpGet]
        public async Task<ActionResult<List<ReservationForFieldResponseDTO>>> GetAllReservationsByIdField()
        {

        }
    }
}
