import { ReservationState } from "./reservationstate.enum";
import { PayType } from "./paytype.enum";

export interface ReservationDetailResponse{

    reservationId: number;

    // contexto
    isAdmin: boolean;
    state: ReservationState;
  
    // fecha y hora
    date: string;
    initTime: string;
  
    // pago
    payType?: string;
    totalPrice: number;
    pricePaid: number;
  
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
  }