import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComplexServiceModel } from '../models/complexservice.model';

@Injectable({
  providedIn: 'root',
})
export class Complexservices {
  private apiBaseUrl = 'https://localhost:7004/api/services'
  constructor(private http:HttpClient){}

  getServiceById(id:number):Observable<ComplexServiceModel>{
    return this.http.get<ComplexServiceModel>(`${this.apiBaseUrl}/${id}`);
  }

  getAllServices():Observable<ComplexServiceModel[]>{
    return this.http.get<ComplexServiceModel[]>(`${this.apiBaseUrl}`);
  }
}
