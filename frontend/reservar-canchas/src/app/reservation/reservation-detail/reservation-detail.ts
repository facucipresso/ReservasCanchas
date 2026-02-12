import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Reservation } from '../../services/reservation';
import { ReservationDetailResponse } from '../../models/reservation/ReservationDetailResponse.model';
import { ReservationState } from '../../models/reservation/reservationstate.enum';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ChangeStateReservation } from '../../models/ChangeStateReservationRequest.model';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ReservationStatePipe } from '../../pipes/reservation-state-pipe';
import { ConfirmDialog } from 'primeng/confirmdialog';

@Component({
  selector: 'app-reservation-detail',
  standalone: true,
  imports: [CommonModule, DialogModule, ProgressSpinnerModule, ReservationStatePipe, ConfirmDialog],
  templateUrl: './reservation-detail.html',
  styleUrl: './reservation-detail.css',
  providers: [ConfirmationService]
})
export class ReservationDetail implements OnInit, OnChanges {

  reservationDetail!: ReservationDetailResponse;
  visible = false;
  isLoading = true;
  @Input() selectedReservationId!: number;
  @Output() stateChanged = new EventEmitter<ReservationState>();
  showReasonDialog = false;
  isAdminRoute = false;
  pendingState: ReservationState | null = null;

  ReservationState = ReservationState; // por si después querés usar enum

  constructor(
    private reservationService: Reservation, 
    private messageService : MessageService,
    private confirmationService : ConfirmationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadReservation();
    this.isAdminRoute = this.router.url.includes("/admin");
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedReservationId'] && this.selectedReservationId) {
       this.loadReservation();
    }
  }

  private loadReservation(){
    if (!this.selectedReservationId) return; 

    this.reservationService.getReservationDetail(this.selectedReservationId).subscribe({
      next: (response) => {
        //this.reservationDetail = response;
        this.reservationDetail = response;
        console.log(response);
        this.isLoading = false;
        
      },
      error: (err) => {
        console.error('Error loading reservation detail', err);
        this.isLoading = false;
      }
    });
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


  changeState(newState: ReservationState) {
    const dto: ChangeStateReservation = {
      newState
    };

    console.log(newState);
  
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
          this.stateChanged.emit(newState);
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
  
    this.reservationService.changeStateReservation(this.reservationDetail.reservationId, dto).subscribe({
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

  onClientCancelAprobada() {
    if (!this.reservationDetail) return;

    const dateStr = this.reservationDetail.date.toString(); 
    const timeStr = this.reservationDetail.initTime.toString();

    const [year, month, day] = dateStr.split('-').map(Number);
    const [hours, minutes] = timeStr.split(':').map(Number);

    const matchDate = new Date(year, month - 1, day, hours, minutes);
    
    const now = new Date();

    const diffMs = matchDate.getTime() - now.getTime();

    const diffHours = diffMs / (1000 * 60 * 60);

    console.log(`Horas restantes para el partido: ${diffHours}`);

    let newState: ReservationState;

    if (diffHours > 4) {
      newState = ReservationState.CanceladoConDevolucion;
    } else {
      newState = ReservationState.CanceladoSinDevolucion;
    }
    this.confirmCancelReservation(newState);
  }

  confirmCancelReservation(state: ReservationState) {
    this.confirmationService.confirm({
      message: '¿Estás seguro que deseas cancelar la reserva? El reembolso sólo se realizará si la reserva '+
      'se encuentra en estado pendiente o aprobada(con más de 4 horas para el inicio de la misma).',
      header: 'Confirmar cancelación',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      acceptLabel: 'Confirmar',
      rejectLabel: 'Cancelar',
      accept: () => {
        this.changeState(state);
      },
      reject: () => {
        console.log('Eliminación cancelada');
      }
    });
  }
  
  
  
}

