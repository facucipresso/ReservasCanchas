import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserInfoLogin } from '../models/userInfoLogin.model';
import { UserRegistration } from '../models/userRegistration.model';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private apiBaseUrl = 'https://localhost:7004/api/account';
  private tokenKey = 'auth-token'

  constructor(private http:HttpClient, private route:Router){}

  register(userInfoRegistration: UserRegistration):Observable<UserInfoLogin>{
    return this.http.post<UserInfoLogin>(`${this.apiBaseUrl}/register`, userInfoRegistration);
  }

  login(credentials:{username:string, password:string}):Observable<UserInfoLogin>{
    return this.http.post<UserInfoLogin>(`${this.apiBaseUrl}/login`,credentials);
  }

  logout(){
    localStorage.removeItem(this.tokenKey);
    this.route.navigate(['/'])
  }

  setToken(token:string){
    localStorage.setItem(this.tokenKey,token);
  }

  getToken(): string | null{
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean{
    const token = this.getToken();
    return !!token && !this.isTokenExpired();
  }

  private decodeTokenPayload(): any | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
      return null;
    }
  }

  getUserId(): string | null {
    const payload = this.decodeTokenPayload();
    return payload?.nameid || payload?.sub || null;
  }

  getUserEmail(): string | null {
    const payload = this.decodeTokenPayload();
    return payload?.email || null;
  }

  getUsername(): string | null {
    const payload = this.decodeTokenPayload();
    return payload?.given_name || null;
  }

  getUserRole(): string | null{
    const payload = this.decodeTokenPayload();
    return payload?.role || null;
  }

  getTokenExpiration(): number | null {
    const payload = this.decodeTokenPayload();
    return payload?.exp || null;
  }

  isTokenExpired(): boolean{
    const exp = this.getTokenExpiration();
    if(!exp) return true;

    const now = Math.floor(Date.now() / 1000);
    return exp < now;
  }
}
