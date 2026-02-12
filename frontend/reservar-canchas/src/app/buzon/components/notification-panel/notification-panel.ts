import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';

import { Notification } from '../../../models/notification.model';
import { NOTIFICATION_TITLES } from '../../../constants/notification-titles';
import { ButtonSeverity } from 'primeng/button';
import { NotificationService } from '../../../services/notification';

@Component({
  selector: 'app-notification-panel',
  standalone: true,
  imports: [
    CommonModule,
    PanelModule,
    ButtonModule
  ],
  templateUrl: './notification-panel.html'
})
export class NotificationPanelComponent {

  @Input() notification!: Notification;

  // estado del panel
  collapsed = true;

  constructor(private notificationService: NotificationService){}

  // acciones según tipo de notificación
  get actions(): string[] {
    switch (this.notification.title) {
      case NOTIFICATION_TITLES.COMPLEJO_PENDIENTE:
        return ['VER COMPLEJO', 'RECHAZAR'];

      case NOTIFICATION_TITLES.RESERVA_PENDIENTE:
        return ['VER RESERVA', 'RECHAZAR'];

      case NOTIFICATION_TITLES.COMPLEJO_APROBADO:
        return ['VER COMPLEJO'];

      default:
        return [];
    }
  }

  /* 
  onToggle() {
    if (!this.collapsed && !this.notification.isRead) {
      this.notification.isRead = true;
      // más adelante: llamar servicio para marcar como leída
      this.notificationService.markAsRead(this.notification.id).subscribe({
        error: (err) => {
          console.error("Error al marcar como leida", err);
          //hago un rollback visual
          this.notification.isRead = false;
        }
      })
    }
  }
 */

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
  }

  getButtonSeverity(action: string): ButtonSeverity {
    if (action === 'RECHAZAR') return 'danger';
    return 'primary';
  }
}
