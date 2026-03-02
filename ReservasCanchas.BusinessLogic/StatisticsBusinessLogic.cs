using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Dashboard;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.BusinessLogic.Dtos.User;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic
{
    public class StatisticsBusinessLogic
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly ReservationBusinessLogic _reservationBusinessLogic;
        private readonly AuthService _authService;
        private readonly NotificationBusinessLogic _notificationBusinessLogic;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly ReviewBusinessLogic _reviewBusinessLogic;



        public StatisticsBusinessLogic(ComplexBusinessLogic complexBusinessLogic, ReservationBusinessLogic reservationBusinessLogic, AuthService authService, NotificationBusinessLogic notificationBusinessLogic, UserBusinessLogic userBusinessLogic, ReviewBusinessLogic reviewBusinessLogic    )
        {
            _complexBusinessLogic = complexBusinessLogic;
            _reservationBusinessLogic = reservationBusinessLogic;
            _authService = authService;
            _notificationBusinessLogic = notificationBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
            _reviewBusinessLogic = reviewBusinessLogic;
        }

        public async Task<ComplexStatsDTO> GetComplexStats(int complexId, DateOnly date, int? fieldId)
        {
            var complex = await _complexBusinessLogic.GetComplexWithFieldsOrThrow(complexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);

            int totalSlots = 0;
            int ocuppiedSlots = 0;
            int matches = 0;
            int specificBlocks = 0;
            decimal totalRevenue = 0;

            DailyReservationsForComplexResponseDTO reservationsForComplex = await _reservationBusinessLogic.GetReservationsByDateForComplexAsync(complexId, date, false);


            if (fieldId.HasValue)
            {
                var fieldStats = reservationsForComplex.FieldsWithReservedHours.FirstOrDefault(f => f.FieldId == fieldId);
                ocuppiedSlots = fieldStats.ReservedHours.Count();
                List<ReservationForUserResponseDTO> reservationsField = await _reservationBusinessLogic.GetReservationsByFieldAndDateAsync(fieldId ?? 0, date);
                matches = reservationsField.Where(r => (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada) && r.ReservationType == ReservationType.Partido).
                    Count();
                specificBlocks = reservationsField.Where(r => (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada) && r.ReservationType == ReservationType.Bloqueo).
                    Count();
                totalRevenue = reservationsField.Where(r => r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada).Sum(r => r.TotalAmount ?? 0);

            }
            else
            {
                ocuppiedSlots = reservationsForComplex.FieldsWithReservedHours.Sum(f => f.ReservedHours.Count());
                List<ReservationForUserResponseDTO> reservationsComplex = await _reservationBusinessLogic.GetReservationsByComplexAndDateAsync(complexId, date);
                matches = reservationsComplex.Where(r => (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada) && r.ReservationType == ReservationType.Partido).
                    Count();
                specificBlocks = reservationsComplex.Where(r => (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada) && r.ReservationType == ReservationType.Bloqueo).
                    Count();
                totalRevenue = reservationsComplex.Where(r => r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Completada).Sum(r => r.TotalAmount ?? 0);
            }

            var fieldsToProcess = fieldId.HasValue
                ? complex.Fields.Where(f => f.Id == fieldId.Value).ToList()
                : complex.Fields.ToList();
            var dayOfWeek = _complexBusinessLogic.ConvertToWeekDay(date);
            var yesterday = date.AddDays(-1);
            var yesterdayDayOfWeek = _complexBusinessLogic.ConvertToWeekDay(yesterday);
            foreach (var field in fieldsToProcess)
            {
                var timeSlotToday = field.TimeSlotsField.FirstOrDefault(ts => ts.WeekDay == dayOfWeek);
                var timeSlotYesterday = field.TimeSlotsField.FirstOrDefault(ts => ts.WeekDay == yesterdayDayOfWeek);

                int fieldHoursOpen = 0;

                // Horas que la cancha abre HOY
                if (timeSlotToday != null && timeSlotToday.StartTime != timeSlotToday.EndTime)
                {
                    var start = timeSlotToday.StartTime.Hour;
                    var end = timeSlotToday.EndTime.Hour;

                    if (end > start)
                    {
                        fieldHoursOpen += (end - start);
                    }
                    else
                    {
                        fieldHoursOpen += (24 - start);
                    }
                }

                // Horas que la cancha trae desde AYER (madrugada de hoy)
                if (timeSlotYesterday != null && timeSlotYesterday.StartTime != timeSlotYesterday.EndTime)
                {
                    var yStart = timeSlotYesterday.StartTime.Hour;
                    var yEnd = timeSlotYesterday.EndTime.Hour;

                    if (yEnd < yStart)
                    {
                        fieldHoursOpen += yEnd;
                    }
                }

                totalSlots += fieldHoursOpen;
            }

            ComplexStatsDTO stats = new ComplexStatsDTO
            {
                totalPossibleSlots = totalSlots,
                occupiedSlots = ocuppiedSlots,
                matches = matches,
                specificBlocks = specificBlocks,
                totalRevenue = totalRevenue
            };

            return stats;
        }

        public async Task<SumaryDashboardDTO> GetDashboardDataAsync()
        {
            var pendingNotifications = await _notificationBusinessLogic.GetNumberOfNotificationsNoReaded();
            var totalUsers = await _userBusinessLogic.GetTotalUsersAsync();
            var enabledComplexes = await _complexBusinessLogic.GetNumberOfComplexEnabled();
            var totalReviews = await _reviewBusinessLogic.GetNumberOfReviews();

            return new SumaryDashboardDTO
            {
                PendingNotifications = pendingNotifications,
                TotalUsers = totalUsers,
                EnabledComplexes = enabledComplexes,
                TotalReviews = totalReviews
            };
        }

        public async Task<List<UserResponseWithRoleDTO>> GetLastSixUsersWithRoleAsync()
        {
            var usuarios = await _userBusinessLogic.GetLastSixUsersWithRoleAsync();
            if (usuarios == null) throw new NotFoundException($"No hay usuarios");

            return usuarios;
        }
        public async Task<List<ComplexSuperAdminResponseDTO>> GetLastFiveComplexesBySuperAdminAsync()
        {
            var complejos = await _complexBusinessLogic.GetLastFiveComplexesBySuperAdminAsync();
            if (complejos == null) throw new NotFoundException($"No hay complejos");

            return complejos;
        }

        public async Task<List<ReviewResponseDTO>> GetLastFourReviewsAsync()
        {
            var reviews = await _reviewBusinessLogic.GetLastFourReviewsAsync();
            if (reviews == null) throw new NotFoundException($"No hay reviews");

            return reviews;
        }
    }
}
