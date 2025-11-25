using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Notification;

namespace ReservasCanchas.Controller.Controllers
{
    
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationBusinessLogic _notificationBusinessLogic;
        private readonly ReservationBusinessLogic _reservationBusinessLogic;

        public NotificationController(NotificationBusinessLogic notificationBusinessLogic, ReservationBusinessLogic reservationBusinessLogic)
        {
            _notificationBusinessLogic = notificationBusinessLogic;
            _reservationBusinessLogic = reservationBusinessLogic;
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<NotificacionResponseDTO>>> GetMyNotifications()
        {
            var notifications = await _notificationBusinessLogic.GetNotificacionsByUserIdAsync();
            return Ok(notifications);
        }

        [HttpPatch("{id}/read")]
        public async Task<ActionResult> UpdateReadNotification([FromRoute] int id)
        {
            await _notificationBusinessLogic.MarkAsReadAsync(id);
            return NoContent();
        }

    }
}
