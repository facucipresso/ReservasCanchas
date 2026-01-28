import { HttpInterceptorFn } from '@angular/common/http';
import { Auth } from '../services/auth';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService :Auth = inject(Auth);
  const token :String|null = authService.getToken();

  if(token && !authService.isTokenExpired()){
    const cloneRequest = req.clone({
      setHeaders:{
        Authorization: `Bearer ${token}`
      }
    });
    return next(cloneRequest);
  }
  return next(req);
};
