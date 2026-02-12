import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { Notification } from '../../../models/notification.model';
import { ChangeStateReservation } from '../../../models/ChangeStateReservationRequest.model';
import { ReservationState } from '../../../models/ReservationState.Enum';
import { NOTIFICATION_TITLES } from '../../../constants/notification-titles';
import { ButtonSeverity } from 'primeng/button';
import { NotificationService } from '../../../services/notification';
import { Router } from '@angular/router';
import { ReservationReasonDialog } from '../reservation-reason-dialog/reservation-reason-dialog';
import { Reservation } from '../../../services/reservation';

@Component({
  selector: 'app-notification-panel',
  standalone: true,
  imports: [
    CommonModule,
    PanelModule,
    ButtonModule,
    ReservationReasonDialog
  ],
  templateUrl: './notification-panel.html'
})
export class NotificationPanelComponent {

  @Input() notification!: Notification;

  // estado del panel
  collapsed = true;

  showReasonDialog = false;
  selectedAction: 'RECHAZAR' | 'CANCELAR' | null = null;

  constructor(private notificationService: NotificationService, private reservationService : Reservation, private messageService: MessageService, private router : Router){}

  // acciones según tipo de notificación
  get actions(): string[] {
    switch (this.notification.title) {
      case NOTIFICATION_TITLES.COMPLEJO_PENDIENTE:
        return ['VER_COMPLEJO', 'RECHAZAR'];

      case NOTIFICATION_TITLES.RESERVA_PENDIENTE:
        return ['VER_RESERVA', 'RECHAZAR'];

      case NOTIFICATION_TITLES.COMPLEJO_APROBADO:
        return ['VER_COMPLEJO'];

      case NOTIFICATION_TITLES.RESERVA_CREADA:
        return ['VER_RESERVA'];

      case NOTIFICATION_TITLES.RESERVA_APROBADA:
        return ['VER_RESERVA'];
      
      default:
        return [];
    }
  }

  togglePanel() {
    this.collapsed = !this.collapsed;
  
    if (!this.collapsed && !this.notification.isRead) {
      this.notification.isRead = true;
  
      this.notificationService.markAsRead(this.notification.id).subscribe({
        error: () => {
          this.notification.isRead = false;
        }
      });
    }
  }
  
  onAction(action: string) {
    console.log('Acción:', action, 'Notificación:', this.notification);

    switch (action) {
      case 'VER_COMPLEJO':
        this.goToComplex();
        break;
  
      case 'VER_RESERVA':
        this.goToReservation();
        break;
  
      case 'RECHAZAR':
        // más adelante
        this.selectedAction = 'RECHAZAR';
        this.showReasonDialog = true;
        break;
    }

  }

  getButtonSeverity(action: string): ButtonSeverity {
    if (action === 'RECHAZAR') return 'danger';
    return 'primary';
  }

  private goToComplex() {
    if (!this.notification.complexId) {
      console.warn('La notificación no tiene complexId');
      return;
    }
  
    // Marcar como leída si aún no lo está
    if (!this.notification.isRead) {
      this.notification.isRead = true;
      this.notificationService.markAsRead(this.notification.id).subscribe({
        error: () => {
          this.notification.isRead = false;
        }
      });
    }
  
    this.router.navigate(['/complexes', this.notification.complexId]);
  }

  private goToReservation() {
    if (!this.notification.reservationId) {
      console.warn('La notificación no tiene reservationId');
      return;
    }
  
    this.router.navigate(['/reservation', this.notification.reservationId]);
  }

  onReasonConfirm(reason: string) {
    if (!this.notification.reservationId) {
      console.error('La notificación no tiene reservationId');
      return;
    }
  
    const dto: ChangeStateReservation = {
      newState: ReservationState.Rechazada,
      cancelationReason: reason
    };
  
    this.reservationService
      .changeStateReservation(this.notification.reservationId, dto)
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Reserva rechazada',
            detail: 'La reserva fue rechazada correctamente.',
            life: 2500
          });
  
          this.showReasonDialog = false;
          this.selectedAction = null;
  
          // opcional: marcar notificación como leída
          if (!this.notification.isRead) {
            this.notification.isRead = true;
            this.notificationService.markAsRead(this.notification.id).subscribe();
          }
        },
        error: (err) => {
          console.error('Error al rechazar reserva', err);
  
          const message =
            err?.error?.detail || 'No se pudo rechazar la reserva';
  
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: message,
            life: 3000
          });
        }
      });
  }
  
  
  onReasonCancel() {
    this.selectedAction = null;
  }

}
