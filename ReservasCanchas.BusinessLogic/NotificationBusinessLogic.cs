using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class NotificationBusinessLogic
    {
        private NotificationRepository _notificationRepository;
        private UserBusinessLogic _userBusinessLogic;

        public NotificationBusinessLogic(NotificationRepository notificationRepository, UserBusinessLogic userBusinessLogic)
        {
            _notificationRepository = notificationRepository;
            _userBusinessLogic = userBusinessLogic;
        }

        public async Task<List<NotificacionResponseDTO>> GetNotificacionsByUserIdAsync()
        {
            var userId = 2;//_authService.getUserId();
            await _userBusinessLogic.GetUserOrThrow(userId);

            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);

            var ordered = notifications
                .OrderBy(n => n.IsRead)                // primero las no leidas
                .ThenByDescending(n => n.CreatedAt)    // dentro de cada grupo, fecha desc
                .ToList();


            return ordered.Select(n => new NotificacionResponseDTO
            {
                Id = n.Id,
                UserId = n.UserId,
                CreatedAt = n.CreatedAt,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                ReservationId = n.ReservationId,
                ComplexId = n.ComplexId
            }).ToList();
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _notificationRepository.CreateNotificationAsync(notification);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var userId = 2; //_authService.getUserId();
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);

            if (notification == null)
                throw new DirectoryNotFoundException($"Notificacion con id {id} no encontrada");

            notification.IsRead = true;
            await _notificationRepository.SaveAsync();
        }

    }
}
