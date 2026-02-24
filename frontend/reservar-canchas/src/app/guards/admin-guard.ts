import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';
import { MessageService } from 'primeng/api';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router)
  const messageService = inject(MessageService)
  if(authService.isLoggedIn() && authService.getUserRole() === 'AdminComplejo'){
    console.log(authService.getUserRole());
    return true;
  }
  console.log(authService.getUserRole());
  messageService.add({
    severity: 'error',
    summary: 'Acceso denegado',
    detail: 'No tenés permisos para acceder a esta funcionalidad',
    life: 3000
  });

  return router.navigate(["/"]);
};
