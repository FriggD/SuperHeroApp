import { Component, OnInit } from '@angular/core';
import { Notification, NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-notifications',
  template: `
    <div class="notifications-container">
      <div *ngFor="let notification of notifications" 
           class="notification" 
           [ngClass]="{'notification-success': notification.type === 'success', 
                      'notification-error': notification.type === 'error',
                      'notification-info': notification.type === 'info'}">
        <div class="notification-content">
          <span>{{ notification.message }}</span>
          <button class="close-btn" (click)="removeNotification(notification.id)">&times;</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .notifications-container {
      position: fixed;
      top: 20px;
      right: 20px;
      z-index: 1050;
      max-width: 350px;
    }
    
    .notification {
      margin-bottom: 10px;
      padding: 15px;
      border-radius: 4px;
      box-shadow: 0 4px 8px rgba(0,0,0,0.1);
      animation: fadeIn 0.3s ease-in;
    }
    
    .notification-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
    
    .notification-success {
      background-color: #d4edda;
      border-color: #c3e6cb;
      color: #155724;
    }
    
    .notification-error {
      background-color: #f8d7da;
      border-color: #f5c6cb;
      color: #721c24;
    }
    
    .notification-info {
      background-color: #d1ecf1;
      border-color: #bee5eb;
      color: #0c5460;
    }
    
    .close-btn {
      background: transparent;
      border: none;
      font-size: 1.5rem;
      line-height: 1;
      cursor: pointer;
      padding: 0 0 0 15px;
    }
    
    @keyframes fadeIn {
      from { opacity: 0; transform: translateY(-10px); }
      to { opacity: 1; transform: translateY(0); }
    }
  `]
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[] = [];

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.notificationService.getNotifications().subscribe(
      notifications => this.notifications = notifications
    );
  }

  removeNotification(id: number): void {
    this.notificationService.removeNotification(id);
  }
}