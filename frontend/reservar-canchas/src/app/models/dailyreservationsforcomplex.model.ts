import { ReservationsForField } from "./reservationsforfield.model";

export interface DailyReservationsComplex{
  complexId:number;
  date:string;
  fieldsWithReservedHours:ReservationsForField[];
}