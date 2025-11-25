using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
