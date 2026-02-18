import { HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { ComplexCardModel } from '../models/complexcard.model';
import { ComplexModel } from '../models/complex.model';
import { basic } from '@primeuix/themes/aura/fileupload';
import { TimeSlotCreateModel } from '../models/timeslotscreate.model';
import { ComplexStats } from '../models/complexstats.model';
import { ComplexState } from '../models/complexstate.enum';

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

  updateComplexBasicInfo(basicInfo:any, complexId:number):Observable<ComplexModel>{
    return this.http.patch<ComplexModel>(`${this.apiBaseUrl}/${complexId}/basic-info`,basicInfo);
  }

  updateComplexTimeSlots(timeSlots:TimeSlotCreateModel[], complexId:number):Observable<ComplexModel>{
    return this.http.put<ComplexModel>(`${this.apiBaseUrl}/${complexId}/time-slots`,timeSlots);
  }

  updateComplexServices(body:{servicesIds:number[]}, complexId:number):Observable<ComplexModel>{
    return this.http.put<ComplexModel>(`${this.apiBaseUrl}/${complexId}/services`,body);
  }

  updateComplexImage(formData:FormData, complexId:number):Observable<ComplexModel>{
    return this.http.patch<ComplexModel>(`${this.apiBaseUrl}/${complexId}/image`, formData);
  }

  updateComplexState(newStateObject:{state:ComplexState}, complexId:number):Observable<ComplexModel>{
    return this.http.patch<ComplexModel>(`${this.apiBaseUrl}/${complexId}/state`,newStateObject);
  }

  deleteComplex(complexId:number):Observable<any>{
    return this.http.delete<any>(`${this.apiBaseUrl}/${complexId}`);
  }
}
