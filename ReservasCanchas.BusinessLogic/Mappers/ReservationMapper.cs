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
                CreationDate = r.CreationDate,
                Date = r.Date,
                InitTime = r.InitTime,
                ReservationState = r.ReservationState,
                FieldId = r.FieldId,
                FieldName = r.Field.Name,
                UserId = r.UserId,
                ReservationType = r.ReservationType,

                UserName = isBlock ? null : r.User.Name,
                UserLastName = isBlock ? null : r.User.LastName,
                UserEmail = isBlock ? null : r.User.Email,
                UserPhone = isBlock ? null : r.User.PhoneNumber,

                PayType = isBlock ? null : r.PayType,
                PricePaid = isBlock ? null : r.PricePaid,
                TotalPrice = isBlock ? null : r.TotalPrice,
                VoucherPath = isBlock ? null : r.VoucherPath,
            };
        }
        public static ReservationForUserResponseDTO ToReservationForUserDTO (Reservation r)
        {
            return new ReservationForUserResponseDTO
            {
                    ReservationId = r.Id,
                    Date = r.Date,
                    InitTime = r.InitTime,
                    State = r.ReservationState,
                    ComplexName = r.Field.Complex.Name,
                    FieldName = r.Field.Name,
                    TotalPrice = r.TotalPrice,
                    PricePaid = r.PricePaid,
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
                CreationDate = r.CreationDate,
                Date = r.Date,
                InitTime = r.InitTime,
                EndTime = r.InitTime.AddHours(1),
                State = r.ReservationState,
                ReservationType = r.ReservationType,
                TotalPrice = r.TotalPrice,
                PricePaid = r.PricePaid,
            };
        }
    }
}
