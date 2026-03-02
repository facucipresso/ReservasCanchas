using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.Domain.Entities;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class NotificationMapper
    {
        public static NotificacionResponseDTO ToResponseDTO(Notification notification)
        {
            return new NotificacionResponseDTO
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }
    }
}
