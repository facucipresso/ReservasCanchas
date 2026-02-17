import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfoModel } from '../models/user/userinfo.model';
import { userRequestModel } from '../models/user/userRequest.model';

@Injectable({
  providedIn: 'root',
})
export class User {
  private apiBaseUrl = 'https://localhost:7004/api/users'

  constructor(private http:HttpClient){}

  getUserInfoById(userId:string){
    return this.http.get<any>(`${this.apiBaseUrl}/${userId}`);
  }

  updateUserInfoById(userId: string, userUpdated : userRequestModel) : Observable<UserInfoModel>{
    return this.http.put<UserInfoModel>(`${this.apiBaseUrl}/${userId}`, userUpdated);
  }
}
