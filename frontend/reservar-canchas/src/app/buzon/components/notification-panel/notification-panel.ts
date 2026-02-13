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
import { NotificationContext } from '../../../models/NotificationContext.enum';

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

    console.log(
      'Notification context:',
      this.notification.context,
      'type:',
      typeof this.notification.context
    );

    switch (this.notification.context) {
      case NotificationContext.ADMIN_COMPLEX_RESERVATION:
        return ['VER RESERVAS'];

      case NotificationContext.USER_RESERVATION:
        return ['VER MIS RESERVAS'];
      
      case NotificationContext.ADMIN_COMPLEX_ACCTION:
        return ['VER COMPLEJO'];
      
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
      case 'VER COMPLEJO':
        this.goToComplex();
        break;
  
      case 'VER RESERVAS':
        this.goToComplexReservations();
        break;

        case 'VER MIS RESERVAS':
          this.goToMyReservations();
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

  private goToComplexReservations() {
    if (!this.notification.complexId) return;
  
    this.router.navigate(
      ['/admin/complexes', this.notification.complexId, 'reservations'],
      {
        queryParams: this.notification.reservationId
          ? { reservationId: this.notification.reservationId }
          : {}
      }
    );
  }

  private goToMyReservations() {
    this.router.navigate(
      ['/reservations'],
      {
        queryParams: this.notification.reservationId
          ? { reservationId: this.notification.reservationId }
          : {}
      }
    );
  }

  
  
  onReasonCancel() {
    this.selectedAction = null;
  }

}
