import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfoModel } from '../models/user/userinfo.model';

@Injectable({
  providedIn: 'root',
})
export class User {
  private apiBaseUrl = 'https://localhost:7004/api/users'

  constructor(private http:HttpClient){}

  getUserInfoById(userId:string):Observable<UserInfoModel>{
    return this.http.get<UserInfoModel>(`${this.apiBaseUrl}/${userId}`);
  }
}
