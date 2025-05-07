import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Notification {
  type: 'success' | 'error' | 'info';
  message: string;
  id: number;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notifications = new BehaviorSubject<Notification[]>([]);
  private nextId = 0;

  constructor() { }

  getNotifications(): Observable<Notification[]> {
    return this.notifications.asObservable();
  }

  success(message: string): void {
    this.addNotification({
      type: 'success',
      message,
      id: this.nextId++
    });
  }

  error(message: string): void {
    this.addNotification({
      type: 'error',
      message,
      id: this.nextId++
    });
  }

  info(message: string): void {
    this.addNotification({
      type: 'info',
      message,
      id: this.nextId++
    });
  }

  private addNotification(notification: Notification): void {
    const current = this.notifications.value;
    this.notifications.next([...current, notification]);
    
    setTimeout(() => {
      this.removeNotification(notification.id);
    }, 5000);
  }

  removeNotification(id: number): void {
    const current = this.notifications.value;
    this.notifications.next(current.filter(n => n.id !== id));
  }
}