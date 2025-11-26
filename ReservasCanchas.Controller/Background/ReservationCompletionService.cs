using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Background
{
    public class ReservationCompletionService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReservationCompletionService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessReservationsAsync();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task ProcessReservationsAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var reservationRepository = scope.ServiceProvider.GetRequiredService<ReservationRepository>();

            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);

            // Buscar reservas aprobadas cuyo horario ya terminó
            var reservationsToComplete = await reservationRepository.GetReservationsToCompleteAsync(today, now);

            foreach (var r in reservationsToComplete)
            {
                r.ReservationState = ReservationState.Completada;
            }

            await reservationRepository.SaveAsync();
        }
    }
}
