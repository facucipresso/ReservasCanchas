using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class StatisticsBusinessLogic
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly ReservationBusinessLogic _reservationBusinessLogic;
        private readonly AuthService _authService;

        public StatisticsBusinessLogic(ComplexBusinessLogic complexBusinessLogic, ReservationBusinessLogic reservationBusinessLogic, AuthService authService    )
        {
            _complexBusinessLogic = complexBusinessLogic;
            _reservationBusinessLogic = reservationBusinessLogic;
            _authService = authService;
        }

        public async Task<ComplexStatsDTO> GetComplexStats(int complexId, DateOnly date, int? fieldId)
        {
            var complex = await _complexBusinessLogic.GetComplexWithFieldsOrThrow(complexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);

            int totalSlots = 0;
            int ocuppiedSlots = 0;
            decimal totalRevenue = 0;

            DailyReservationsForComplexResponseDTO reservationsForComplex = await _reservationBusinessLogic.GetReservationsByDateForComplexAsync(complexId, date, false);


            if (fieldId.HasValue)
            {
                var fieldStats = reservationsForComplex.FieldsWithReservedHours.FirstOrDefault(f => f.FieldId == fieldId);
                ocuppiedSlots = fieldStats.ReservedHours.Count();
                List<ReservationForUserResponseDTO> reservationsField = await _reservationBusinessLogic.GetReservationsByFieldAndDateAsync(fieldId ?? 0, date);
                totalRevenue = reservationsField.Where(r => r.State == ReservationState.Aprobada || r.State == ReservationState.Completada).Sum(r => r.TotalPrice ?? 0);

            }
            else
            {
                ocuppiedSlots = reservationsForComplex.FieldsWithReservedHours.Sum(f => f.ReservedHours.Count());
                List<ReservationForUserResponseDTO> reservationsComplex = await _reservationBusinessLogic.GetReservationsByComplexAndDateAsync(complexId, date);
                totalRevenue = reservationsComplex.Where(r => r.State == ReservationState.Aprobada || r.State == ReservationState.Completada).Sum(r => r.TotalPrice ?? 0);
            }

            var dayOfWeek = _complexBusinessLogic.ConvertToWeekDay(date);
            var timeSlotComplex = complex.TimeSlots.FirstOrDefault(ts => ts.WeekDay == dayOfWeek);

            if (timeSlotComplex.InitTime != timeSlotComplex.EndTime)
            {
                var start = timeSlotComplex.InitTime.Hour;
                var end = timeSlotComplex.EndTime.Hour;

                int hoursOpen = 0;

                if (end > start)
                {
                    hoursOpen = end - start;
                }
                else
                {
                    hoursOpen = (24 - start) + end;
                }

                int fieldCount = fieldId.HasValue ? 1 : complex.Fields.Count();
                totalSlots = hoursOpen * fieldCount;
            }

            ComplexStatsDTO stats = new ComplexStatsDTO
            {
                totalPossibleSlots = totalSlots,
                occupiedSlots = ocuppiedSlots,
                totalRevenue = totalRevenue
            };

            return stats;
        }
    }
}
