import { ReservationState } from "./reservationstate.enum"

export interface ChangeStateReservation{
    newState : ReservationState,
    cancelationReason? : string
}