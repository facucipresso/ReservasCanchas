import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Notification } from '../models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private readonly apiUrl = 'https://localhost:7004/api/notifications';

  constructor(private http: HttpClient) {}

  getMyNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.apiUrl}/my`);
  }

  markAsRead(notificationId: number): Observable<void> {
    return this.http.patch<void>(
      `${this.apiUrl}/${notificationId}/read`,
      {}
    );
  }
}
