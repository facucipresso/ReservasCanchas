import { HttpClient, HttpResponse} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { ComplexCardModel } from '../models/complexcard.model';
import { ComplexModel } from '../models/complex.model';
import { basic } from '@primeuix/themes/aura/fileupload';
import { TimeSlotCreateModel } from '../models/timeslotscreate.model';

@Injectable({
  providedIn: 'root',
})
export class Complex {

  private apiBaseUrl = 'https://localhost:7004/api/complexes'
  constructor(private http:HttpClient){}

  getComplexesWithFilters(filters:any):Observable<ComplexCardModel[]>{
    return this.http.get<ComplexCardModel[]>(`${this.apiBaseUrl}/filters`,{params:filters});
  }

  getComplexById(id:number):Observable<ComplexModel>{
        return this.http.get<ComplexModel>(`${this.apiBaseUrl}/${id}`);
  }

  getMyComplexes():Observable<ComplexCardModel[]>{
    return this.http.get<ComplexCardModel[]>(`${this.apiBaseUrl}/my`)
  }

  createComplex(formData:FormData):Observable<HttpResponse<any>>{
    return this.http.post<any>(this.apiBaseUrl,formData,{observe: 'response'});
  }

  updateComplexBasicInfo(basicInfo:any, complexId:number):Observable<any>{
    return this.http.patch<any>(`${this.apiBaseUrl}/${complexId}/basic-info`,basicInfo);
  }

  updateComplexTimeSlots(timeSlots:TimeSlotCreateModel[], complexId:number):Observable<any>{
    return this.http.put<any>(`${this.apiBaseUrl}/${complexId}/time-slots`,timeSlots);
  }

  updateComplexServices(body:{servicesIds:number[]}, complexId:number):Observable<any>{
    return this.http.put<any>(`${this.apiBaseUrl}/${complexId}/services`,body);
  }

  updateComplexState(newStateObject:{state:string}, complexId:number):Observable<any>{
    return this.http.patch<any>(`${this.apiBaseUrl}/${complexId}/state`,newStateObject);
  }

  deleteComplex(complexId:number):Observable<any>{
    return this.http.delete<any>(`${this.apiBaseUrl}/${complexId}`);
  }
}
