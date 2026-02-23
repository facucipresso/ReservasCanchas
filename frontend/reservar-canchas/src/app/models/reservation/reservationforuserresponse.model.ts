import { ReservationState } from "./reservationstate.enum";
import { ReservationType } from "./reservationtype.enum";

export interface ReservationForUserResponse{
  reservationId: number;
  date: string;
  reservationState: ReservationState;
  reservationType: ReservationType;
  startTime: string;
  complexName: string;
  fieldName: string;
  totalAmount: number;
  amountPaid: number;
  canReview: boolean;
}