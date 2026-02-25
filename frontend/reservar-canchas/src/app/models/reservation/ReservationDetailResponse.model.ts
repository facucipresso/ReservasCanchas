import { ReservationState } from "./reservationstate.enum";
import { PaymentType } from "./paymenttype.enum";
import { ReservationType } from "./reservationtype.enum";

export interface ReservationDetailResponse{

    reservationId: number;
    isAdmin: boolean;
    reservationState: ReservationState;
    reservationType: ReservationType;
    createdAt: string;
    date: string;
    startTime: string;
    paymentType?: string;
    totalAmount: number;
    amountPaid: number;
    hasFieldIllumination: boolean;
    paidIllumination: boolean;
    illuminationAmount: number;
    voucherUrl?: string;
    userId: number;
    userFullName: string;
    userEmail: string;
    userPhone: string;
    fieldId: number;
    fieldName: string;
    fieldType: string;
    floorType: string;
    hourPrice: number;
    complexId: number;
    complexName: string;
    street: string;
    number: string;
    locality: string;
    phone: string;
    blockReason: string;
    hasReview: boolean;
  }