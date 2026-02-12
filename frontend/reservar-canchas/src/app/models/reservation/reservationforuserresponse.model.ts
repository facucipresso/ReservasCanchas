import { ReservationState } from "./reservationstate.enum";

export interface ReservationForUserResponse{
  reservationId: number;
  date: string;
  state: ReservationState;
  initTime: string;
  complexName: string;
  fieldName: string;
  totalPrice: number;
  pricePaid: number;
  canReview: boolean;
}