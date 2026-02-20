using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Jobs
{
    public class ReservationAutoApprovalJob
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly NotificationBusinessLogic _notificationBusinessLogic;

        public ReservationAutoApprovalJob(ReservationRepository reservationRepository, NotificationBusinessLogic notificationBusinessLogic)
        {
            _reservationRepository = reservationRepository;
            _notificationBusinessLogic = notificationBusinessLogic;
        }

        public async Task ExecuteAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdWithRelationsAsync(reservationId);

            if (reservation == null)
                return;

            // Si todavía está pendiente, aprobar
            if (reservation.ReservationState == ReservationState.Pendiente)
            {
                reservation.ReservationState = ReservationState.Aprobada;
                await _reservationRepository.SaveAsync();

                Console.WriteLine($"[Hangfire] Reserva {reservationId} auto-aprobada.");

                var notification = new Notification
                {
                    UserId = reservation.UserId,
                    Title = "Tu reserva fue aprobada",
                    Message = $"Tu reserva en '{reservation.Field.Name}' para el {reservation.Date} a las {reservation.InitTime:HH\\:mm} fue aprobada.",
                    ReservationId = reservation.Id,
                    ComplexId = reservation.Field.ComplexId
                };

                await _notificationBusinessLogic.CreateNotificationAsync(notification);
            }
        }
    }
}
