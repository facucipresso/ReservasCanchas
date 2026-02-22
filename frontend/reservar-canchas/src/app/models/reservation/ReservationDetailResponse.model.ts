import { ReservationState } from "./reservationstate.enum";
import { PaymentType } from "./paymenttype.enum";

export interface ReservationDetailResponse{

    reservationId: number;

    // contexto
    isAdmin: boolean;
    reservationState: ReservationState;
  
    // fecha y hora
    date: string;
    startTime: string;
  
    // pago
    paymentType?: string;
    totalAmount: number;
    amountPaid: number;
  
    // iluminación
    hasFieldIllumination: boolean;
    paidIllumination: boolean;
    illuminationAmount: number;
  
    // comprobante
    voucherUrl?: string;
  
    // usuario
    userId: number;
    userFullName: string;
    userEmail: string;
    userPhone: string;
  
    // cancha
    fieldId: number;
    fieldName: string;
    fieldType: string;
    floorType: string;
    hourPrice: number;
  
    // complejo
    complexId: number;
    complexName: string;
    street: string;
    number: string;
    locality: string;
    phone: string;

    // review
    hasReservation: boolean;
  }