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
    }
}
