import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DailyReservationsComplex } from '../models/reservation/dailyreservationsforcomplex.model';
import { Observable } from 'rxjs';
import { ReservationProcessRequest } from '../models/reservation/reservationprocessrequest.model';
import { ReservationProcessResponse } from '../models/reservation/reservationprocessresponse.model';
import { CheckoutInfo } from '../models/reservation/checkoutinfo.model';
import { CreateReservationResponse } from '../models/reservation/createreservationresponse.model';
import { ChangeStateReservation } from '../models/ChangeStateReservationRequest.model';
import { ReservationDetailResponse } from '../models/reservation/ReservationDetailResponse.model';
import { ReservationForUserResponse } from '../models/reservation/reservationforuserresponse.model';

@Injectable({
  providedIn: 'root',
})
export class Reservation {
  private apiBaseUrl = 'https://localhost:7004/api/reservations';

  constructor(private http:HttpClient) { }

  getMyReservations():Observable<ReservationForUserResponse[]>{
    return this.http.get<ReservationForUserResponse[]>(`${this.apiBaseUrl}/my`);
  }

  getReservationsByDateForComplex(complexId: number, date: string):Observable<DailyReservationsComplex>{
    return this.http.get<DailyReservationsComplex>(`${this.apiBaseUrl}/complex/${complexId}/by-date/`,{params: { date }});
  }

  createProcessReservation(reservationData:ReservationProcessRequest):Observable<ReservationProcessResponse>{
    return this.http.post<ReservationProcessResponse>(`${this.apiBaseUrl}/process`, reservationData);
  }

  getCheckoutInfo(reservationProcessId:string):Observable<CheckoutInfo>{
    return this.http.get<CheckoutInfo>(`${this.apiBaseUrl}/process/${reservationProcessId}`);
  }

  createReservation(formData:FormData):Observable<CreateReservationResponse>{
    return this.http.post<CreateReservationResponse>(`${this.apiBaseUrl}`, formData);
  }

  deleteProcessReservation(reservationProcessId:string):Observable<void>{
    return this.http.delete<void>(`${this.apiBaseUrl}/process/${reservationProcessId}`);
  }

  changeStateReservation(reservationId: number, request: ChangeStateReservation): Observable<void>{
    return this.http.patch<void>(`${this.apiBaseUrl}/${reservationId}/state`, request);
  }

  getReservationDetail(reservationId: number): Observable<ReservationDetailResponse>{
    return this.http.get<ReservationDetailResponse>(`${this.apiBaseUrl}/${reservationId}/detail`);
  }

  getReservationsByComplexAndDate(complexId:number, date:string): Observable<ReservationForUserResponse[]>{
    return this.http.get<ReservationForUserResponse[]>(`${this.apiBaseUrl}/complex/${complexId}/search`,{params:{date}});
  }

  getReservationsByFieldAndDate(fieldId:number, date:string): Observable<ReservationForUserResponse[]>{
    return this.http.get<ReservationForUserResponse[]>(`${this.apiBaseUrl}/field/${fieldId}/search`,{params:{date}});
  }

}
