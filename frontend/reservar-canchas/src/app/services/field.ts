import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FieldModel } from '../models/field.model';
import { RecBlockRequestModel } from '../models/reqblockrequest.model';

@Injectable({
  providedIn: 'root',
})
export class Field {
  private apiBaseUrl = 'https://localhost:7004/api/fields'
  constructor(private http:HttpClient){}

  getFieldsByComplexId(complexId:number):Observable<FieldModel[]>{
    return this.http.get<FieldModel[]>(`${this.apiBaseUrl}/by-complex/${complexId}`);
  }

  getFieldById(fieldId:number):Observable<FieldModel>{
    return this.http.get<FieldModel>(`${this.apiBaseUrl}/${fieldId}`);
  }

  createField(field:any):Observable<FieldModel>{
    return this.http.post<FieldModel>(this.apiBaseUrl,field);
  }

  updateBasicInfoField(basicInfo:any, fieldId:number):Observable<FieldModel>{
    return this.http.patch<FieldModel>(`${this.apiBaseUrl}/${fieldId}`,basicInfo);
  }

  updateTimeSlotsField(timeSlots:any, fieldId:number):Observable<FieldModel>{
    return this.http.put<FieldModel>(`${this.apiBaseUrl}/${fieldId}/time-slots`,timeSlots);
  }

  updateStateField(fieldState:any, fieldId:number):Observable<FieldModel>{
    return this.http.patch<FieldModel>(`${this.apiBaseUrl}/${fieldId}/state`, fieldState);
  }

  deleteField(fieldId:number):Observable<void>{
    return this.http.delete<void>(`${this.apiBaseUrl}/${fieldId}`);
  }

  addRecurringBlock(fieldId:number, block:RecBlockRequestModel):Observable<FieldModel>{
    return this.http.post<FieldModel>(`${this.apiBaseUrl}/${fieldId}/recurring-block`, block);
  }

  deleteRecurringBlock(fieldId:number, blockId:number):Observable<FieldModel>{
    return this.http.delete<FieldModel>(`${this.apiBaseUrl}/${fieldId}/recurring-block/${blockId}`);
  }
}
