import { ReservationState } from "./ReservationState.Enum"

export interface ChangeStateReservation{
    newState : ReservationState,
    cancelationReason? : string
}