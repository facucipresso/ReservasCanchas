import { ReservationState } from "./reservation/reservationstate.enum"

export interface ChangeStateReservation{
    newState : ReservationState,
    cancelationReason? : string
}