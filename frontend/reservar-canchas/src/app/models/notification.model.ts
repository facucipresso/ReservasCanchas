export interface Notification {
    id: number;
    userId: number;
  
    title: string;
    message: string;
  
    createdAt: Date;
    isRead: boolean;
  
    reservationId?: number;
    complexId?: number;
  }