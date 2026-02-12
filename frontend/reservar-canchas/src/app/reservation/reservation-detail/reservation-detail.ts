import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Reservation } from '../../services/reservation';
import { ReservationDetailResponse } from '../../models/reservation/ReservationDetailResponse.model';
import { ReservationState } from '../../models/ReservationState.Enum';
import { MessageService } from 'primeng/api';
import { ChangeStateReservation } from '../../models/ChangeStateReservationRequest.model';
import { ReservationReasonDialog } from '../../buzon/components/reservation-reason-dialog/reservation-reason-dialog';

@Component({
  selector: 'app-reservation-detail',
  standalone: true,
  imports: [CommonModule, ReservationReasonDialog],
  templateUrl: './reservation-detail.html',
  styleUrl: './reservation-detail.css',
})
export class ReservationDetail implements OnInit {

  reservationDetail!: ReservationDetailResponse;
  showVoucher = false;
  loading = true;

  showReasonDialog = false;
  pendingState: ReservationState | null = null;

  ReservationState = ReservationState; // por si después querés usar enum

  constructor(
    private route: ActivatedRoute,
    private reservationService: Reservation, 
    private messageService : MessageService
  ) {}

  ngOnInit(): void {
    const reservationId = Number(this.route.snapshot.paramMap.get('id'));

    if (reservationId) {
      this.reservationService.getReservationDetail(reservationId).subscribe({
        next: (response) => {
          //this.reservationDetail = response;
          this.reservationDetail = {
            ...response,
            state: this.mapState(response.state)
          };
          this.loading = false;
        },
        error: (err) => {
          console.error('Error loading reservation detail', err);
          this.loading = false;
        }
      });
    }
  }

  get isAdmin(): boolean {
    return this.reservationDetail?.isAdmin;
  }

  get totalAmount(): number {
    return this.reservationDetail?.totalPrice ?? 0;
  }

  get amountPaid(): number {
    return this.reservationDetail?.pricePaid ?? 0;
  }

  get basePrice(): number {
    if (!this.reservationDetail) return 0;
  
    return this.reservationDetail.totalPrice
         - this.reservationDetail.illuminationAmount;
  }
  
  get hasIllumination(): boolean {
    return this.reservationDetail?.paidIllumination === true;
  }

  private mapState(state: any): ReservationState {
    return ReservationState[state as keyof typeof ReservationState];
  }
  

  changeState(newState: ReservationState) {
    const dto: ChangeStateReservation = {
      newState
    };
  
    this.reservationService
      .changeStateReservation(this.reservationDetail.reservationId, dto)
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Éxito',
            detail: 'Estado de la reserva actualizado',
            life: 2500
          });
  
          // refrescamos detalle
          this.reservationDetail.state = newState;
        },
        error: (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: err?.error?.detail || 'No se pudo actualizar la reserva',
            life: 3000
          });
        }
      });
  }

  openReasonDialog(state: ReservationState) {
    this.pendingState = state;
    this.showReasonDialog = true;
  }

  onReasonConfirm(reason: string) {
    if (!this.pendingState) return;
  
    const dto: ChangeStateReservation = {
      newState: this.pendingState,
      cancelationReason: reason
    };
  
    this.reservationService
      .changeStateReservation(this.reservationDetail.reservationId, dto)
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Éxito',
            detail: 'La reserva fue actualizada correctamente',
            life: 2500
          });
  
          this.reservationDetail.state = this.pendingState!;
          this.pendingState = null;
          this.showReasonDialog = false;
        },
        error: (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: err?.error?.detail || 'No se pudo actualizar la reserva',
            life: 3000
          });
        }
      });
  }

  onReasonCancel() {
    this.pendingState = null;
    this.showReasonDialog = false;
  }
  
  
  
}

