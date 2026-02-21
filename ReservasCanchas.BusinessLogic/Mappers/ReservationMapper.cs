using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class ReservationMapper
    {

        public static ReservationResponseDTO ToReservationResponseDTO(Reservation r)
        {
            bool isBlock = r.ReservationType == ReservationType.Bloqueo;
            return new ReservationResponseDTO
            {
                ReservationId = r.Id,
                CreatedAt = r.CreatedAt,
                Date = r.Date,
                StartTime = r.StartTime,
                ReservationState = r.ReservationState,
                FieldId = r.FieldId,
                FieldName = r.Field.Name,
                UserId = r.UserId,
                ReservationType = r.ReservationType,

                UserName = isBlock ? null : r.User.Name,
                UserLastName = isBlock ? null : r.User.LastName,
                UserEmail = isBlock ? null : r.User.Email,
                UserPhone = isBlock ? null : r.User.PhoneNumber,

                PaymentType = isBlock ? null : r.PaymentType,
                AmountPaid = isBlock ? null : r.AmountPaid,
                TotalAmount = isBlock ? null : r.TotalAmount,
                VoucherPath = isBlock ? null : r.VoucherPath,
            };
        }
        public static ReservationForUserResponseDTO ToReservationForUserDTO (Reservation r)
        {
            return new ReservationForUserResponseDTO
            {
                    ReservationId = r.Id,
                    Date = r.Date,
                    StartTime = r.StartTime,
                    ReservationState = r.ReservationState,
                    ComplexName = r.Field.Complex.Name,
                    FieldName = r.Field.Name,
                    TotalAmount = r.TotalAmount,
                    AmountPaid = r.AmountPaid,
                    CanReview = r.ReservationState == ReservationState.Completada &&
                        r.Review == null
            };
                    
        }
        public static CreateReservationResponseDTO ToCreateReservationResponseDTO(Reservation r)
        {
            return new CreateReservationResponseDTO
            {
                ReservationId = r.Id,
                FieldId = r.FieldId,
                CreatedAt = r.CreatedAt,
                Date = r.Date,
                StartTime = r.StartTime,
                EndTime = r.StartTime.AddHours(1),
                ReservationState = r.ReservationState,
                ReservationType = r.ReservationType,
                TotalAmount = r.TotalAmount,
                AmountPaid = r.AmountPaid,
            };
        }
    }
}
