import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';
import { MessageService } from 'primeng/api';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);                
  const messageService = inject(MessageService);    
  if(authService.isLoggedIn()){
    return true;
  }
  messageService.add({
    severity: 'error',
    summary: 'Acceso denegado',
    detail: 'Debes iniciar sesión para acceder a esta funcionalidad',
    life: 3000
  });

  router.navigate(["/"]);
  return false;
  
};
