import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FieldDetailModel } from '../models/field/field.model';
import { RecBlockRequestModel } from '../models/recblock/reqblockrequest.model';

@Injectable({
  providedIn: 'root',
})
export class Field {
  private apiBaseUrl = 'https://localhost:7004/api/fields'
  constructor(private http:HttpClient){}

  getFieldsByComplexId(complexId:number):Observable<FieldDetailModel[]>{
    return this.http.get<FieldDetailModel[]>(`${this.apiBaseUrl}/by-complex/${complexId}`);
  }

  getFieldById(fieldId:number):Observable<FieldDetailModel>{
    return this.http.get<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}`);
  }

  createField(field:any):Observable<FieldDetailModel>{
    return this.http.post<FieldDetailModel>(this.apiBaseUrl,field);
  }

  updateBasicInfoField(basicInfo:any, fieldId:number):Observable<FieldDetailModel>{
    return this.http.patch<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}`,basicInfo);
  }

  updateTimeSlotsField(timeSlots:any, fieldId:number):Observable<FieldDetailModel>{
    return this.http.put<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}/time-slots`,timeSlots);
  }

  updateStateField(fieldState:any, fieldId:number):Observable<FieldDetailModel>{
    return this.http.patch<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}/state`, fieldState);
  }

  deleteField(fieldId:number):Observable<void>{
    return this.http.delete<void>(`${this.apiBaseUrl}/${fieldId}`);
  }

  addRecurringBlock(fieldId:number, block:RecBlockRequestModel):Observable<FieldDetailModel>{
    return this.http.post<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}/recurring-block`, block);
  }

  deleteRecurringBlock(fieldId:number, blockId:number):Observable<FieldDetailModel>{
    return this.http.delete<FieldDetailModel>(`${this.apiBaseUrl}/${fieldId}/recurring-block/${blockId}`);
  }
}
