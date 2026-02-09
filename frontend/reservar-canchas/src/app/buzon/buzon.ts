import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Notification } from '../models/notification.model';
import { NOTIFICATION_TITLES } from '../constants/notification-titles';
import { NotificationPanelComponent } from './components/notification-panel/notification-panel';
import { StadisticCard } from './components/stadistic-card/stadistic-card';
import { NotificationService } from '../services/notification';


@Component({
  selector: 'app-buzon',
  imports: [CommonModule, NotificationPanelComponent, StadisticCard],
  templateUrl: './buzon.html',
  //styleUrl: './buzon.css',
})
export class Buzon implements OnInit{

  notifications: Notification[] = [];
  loading = true;

  constructor(private notificationService : NotificationService){}

  ngOnInit(): void {
      this.loadNotifications();
  }


  loadNotifications(): void {
    this.notificationService.getMyNotifications().subscribe({
      next:(notis) => {
        console.log('Notifications: ', notis);
        //this.notifications = notis;
        this.notifications = notis.map(n => ({
          ...n,
          createdAt: new Date(n.createdAt)
        }));
        this.loading = false;
      },
      error:(err) =>{
        console.error('Error cargando las notificaciones', err);
        this.loading = false;
      }
    })
  }
    // --- estadísticas ---
    get totalNotifications(): number {
      return this.notifications.length;
    }
  
    get unreadNotifications(): number {
      return this.notifications.filter(n => !n.isRead).length;
    }
  
    get thisWeekNotifications(): number {
      const now = new Date();
      const weekAgo = new Date();
      weekAgo.setDate(now.getDate() - 7);
  
      return this.notifications.filter(
        n => n.createdAt >= weekAgo
      ).length;
    }

    get actionRequiredNotifications(): number {
      return this.notifications.filter(n =>
        !n.isRead &&
        (
          n.title === NOTIFICATION_TITLES.COMPLEJO_PENDIENTE ||
          n.title === NOTIFICATION_TITLES.RESERVA_PENDIENTE
        )
      ).length;
    }

    get todayNotifications(): number {
      const now = new Date();
      const last24h = new Date(now.getTime() - 24 * 60 * 60 * 1000);
    
      return this.notifications.filter(n =>
        n.createdAt >= last24h
      ).length;
    }

}
