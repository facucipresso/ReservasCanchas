import { ReservationState } from "./reservationstate.enum";

export interface ReservationForUserResponse{
  reservationId: number;
  date: string;
  state: ReservationState;
  startTime: string;
  complexName: string;
  fieldName: string;
  totalAmount: number;
  amountPaid: number;
  canReview: boolean;
}