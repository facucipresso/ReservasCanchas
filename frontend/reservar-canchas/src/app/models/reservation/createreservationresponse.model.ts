import { InitEditableRow } from "primeng/table";

export interface CreateReservationResponse{
  reservationId: number;
  fieldId: number;
  createdAt: string;
  date: string;
  startTime: string;
  endTime: string;
  reservationState:string;
  reservationType:string;
  totalAmount:number;
  amountPaid:number;
}