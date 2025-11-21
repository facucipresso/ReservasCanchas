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
        public static ReservationForComplexResponseDTO ToReservationForComplexResponseDTO (Reservation r) 
        {
            return new ReservationForComplexResponseDTO
            {
                ReservationId = r.Id,
                Date = r.Date,
                InitTime = r.InitTime,
                State = r.ReservationState,
                FieldId = r.FieldId,
                FieldName = r.Field.Name,
                UserId = r.UserId,
                UserName = r.Usuario.Name,
                PayType = r.PayType,
                TotalPrice = r.TotalPrice,
                PricePaid = r.PricePaid
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

        public static ReservationForFieldResponseDTO ToReservationForFieldResponseDTO(Reservation r)
        {
            return new ReservationForFieldResponseDTO
            {
                ReservationId = r.Id,
                Date = r.Date,
                InitTime = r.InitTime,
                State = r.ReservationState,

                UserId = r.UserId,
                UserName = r.Usuario.Name,

                PayType = r.PayType
            };
        }

        /*public static DayReservationDTO ToDayReservationDTO(Reservation r)
        {
            return new DayReservationDTO
            {
                ReservationId = r.Id,
                FieldId = r.FieldId,
                InitTime = r.InitTime,
                EndTime = r.InitTime.AddHours(1)
            };
        }

        public static DayRecurringBlockDTO ToDayRecurringBlockDTO(RecurringFieldBlock b)
        {
            return new DayRecurringBlockDTO
            {
                RecurringBlockId = b.Id,
                FieldId = b.FieldId,
                InitTime = b.InitHour,
                EndTime = b.EndHour
            };
        }*/

        public static CreateReservationResponseDTO ToCreateReservationResponseDTO(Reservation r)
        {
            return new CreateReservationResponseDTO
            {
                ReservationId = r.Id,
                FieldId = r.FieldId,
                Date = r.Date,
                InitTime = r.InitTime,
                EndTime = r.InitTime.AddHours(1),
                State = r.ReservationState,
                ReservationType = r.ReservationType,
                TotalPrice = r.TotalPrice,
            };
        }

        public static ReservationBlockingResponseDTO ToCreateReservationBlockingResponseDTO(Reservation r)
        {
            return new ReservationBlockingResponseDTO
            {
                ReservationId = r.Id,
                FieldId = r.FieldId,
                Date = r.Date,
                InitTime = r.InitTime,
                EndTime = r.InitTime.AddHours(1),
                ReservationType = r.ReservationType,
                BlockReason = r.BlockReason
            };
        }
    }
}
