import { HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { ComplexCardModel } from '../models/complex/complexcard.model';
import { ComplexDetailModel } from '../models/complex/complexdetail.model';
import { basic } from '@primeuix/themes/aura/fileupload';
import { TimeSlotCreateModel } from '../models/complex/timeslotscreate.model';
import { ComplexStats } from '../models/complex/complexstats.model';
import { ComplexState } from '../models/complex/complexstate.enum';

@Injectable({
  providedIn: 'root',
})
export class Complex {

  private apiBaseUrl = 'https://localhost:7004/api/complexes'
  constructor(private http:HttpClient){}

  getComplexesWithFilters(filters:any):Observable<ComplexCardModel[]>{
    return this.http.get<ComplexCardModel[]>(`${this.apiBaseUrl}/filters`,{params:filters});
  }

  getComplexById(id:number):Observable<ComplexDetailModel>{
        return this.http.get<ComplexDetailModel>(`${this.apiBaseUrl}/${id}`);
  }

  getMyComplexes():Observable<ComplexCardModel[]>{
    return this.http.get<ComplexCardModel[]>(`${this.apiBaseUrl}/my`)
  }

  getComplexStats(complexId:number, date: string, fieldId:number | null):Observable<ComplexStats>{
    let params = new HttpParams().set('date', date);

    if (fieldId !== null && fieldId) {
      params = params.set('fieldId', fieldId.toString());
    }
    return this.http.get<ComplexStats>(`${this.apiBaseUrl}/stats/${complexId}`, {params});
  }

  createComplex(formData:FormData):Observable<HttpResponse<any>>{
    return this.http.post<any>(this.apiBaseUrl,formData,{observe: 'response'});
  }

  updateComplexBasicInfo(basicInfo:any, complexId:number):Observable<ComplexDetailModel>{
    return this.http.patch<ComplexDetailModel>(`${this.apiBaseUrl}/${complexId}/basic-info`,basicInfo);
  }

  updateComplexTimeSlots(timeSlots:TimeSlotCreateModel[], complexId:number):Observable<ComplexDetailModel>{
    return this.http.put<ComplexDetailModel>(`${this.apiBaseUrl}/${complexId}/time-slots`,timeSlots);
  }

  updateComplexServices(body:{servicesIds:number[]}, complexId:number):Observable<ComplexDetailModel>{
    return this.http.put<ComplexDetailModel>(`${this.apiBaseUrl}/${complexId}/services`,body);
  }

  updateComplexImage(formData:FormData, complexId:number):Observable<ComplexDetailModel>{
    return this.http.patch<ComplexDetailModel>(`${this.apiBaseUrl}/${complexId}/image`, formData);
  }

  updateComplexState(newStateObject:{complexState:ComplexState}, complexId:number):Observable<ComplexDetailModel>{
    return this.http.patch<ComplexDetailModel>(`${this.apiBaseUrl}/${complexId}/state`,newStateObject);
  }

  deleteComplex(complexId:number):Observable<any>{
    return this.http.delete<any>(`${this.apiBaseUrl}/${complexId}`);
  }
}
