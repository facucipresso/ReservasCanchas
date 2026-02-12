import { Pipe, PipeTransform } from '@angular/core';
import { ReservationState } from '../models/reservation/reservationstate.enum';

@Pipe({
  name: 'reservationStateText'
})
export class ReservationStatePipe implements PipeTransform {

  transform(value: ReservationState | string): string {
    switch (value) {
      case ReservationState.Pendiente:
        return 'Pendiente';
      case ReservationState.Aprobada:
        return 'Aprobada';
      case ReservationState.Rechazada:
        return 'Rechazada';
      case ReservationState.CanceladoConDevolucion:
        return 'Cancelada con devolución';
      case ReservationState.CanceladoSinDevolucion:
        return 'Cancelada sin devolución';
      case ReservationState.Completada:
        return 'Completada';
      case ReservationState.CanceladoPorAdmin:
        return 'Cancelada por el administrador';
      default:
        return value; 
    }
  }

}
