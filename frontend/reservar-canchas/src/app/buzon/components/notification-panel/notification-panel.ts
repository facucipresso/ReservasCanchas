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
import { Reservation } from '../../../services/reservation';

@Component({
  selector: 'app-notification-panel',
  standalone: true,
  imports: [
    CommonModule,
    PanelModule,
    ButtonModule,
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
        return ['VER COMPLEJO'];

      case NOTIFICATION_TITLES.RESERVA_PENDIENTE:
        return ['VER RESERVA'];

      case NOTIFICATION_TITLES.COMPLEJO_APROBADO:
        return ['VER COMPLEJO'];

      case NOTIFICATION_TITLES.RESERVA_CREADA:
        return ['VER RESERVA'];

      case NOTIFICATION_TITLES.RESERVA_APROBADA:
        return ['VER RESERVA'];
      
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

    }

  }

  getButtonSeverity(action: string): ButtonSeverity {
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
  
  
  onReasonCancel() {
    this.selectedAction = null;
  }

}
