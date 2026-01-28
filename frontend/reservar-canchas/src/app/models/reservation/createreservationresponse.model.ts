import { InitEditableRow } from "primeng/table";

export interface CreateReservationResponse{
  reservationId: number;
  fieldId: number;
  creationDate: string;
  date: string;
  initTime: string;
  endTime: string;
  reservationState:string;
  reservationType:string;
  totalPrice:number;
  pricePaid:number;
}