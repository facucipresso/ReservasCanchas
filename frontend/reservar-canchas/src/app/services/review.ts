import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateReviewRequest } from '../models/reservation/createreviewrequest.model';
import { Observable } from 'rxjs';
import { ReviewResponse } from '../models/reservation/reviewresponse.model';

@Injectable({
  providedIn: 'root',
})
export class Review {

  private apiBaseUrl = 'https://localhost:7004/api/reviews';

  constructor(private http:HttpClient) { }

  createReview(reviewDto:CreateReviewRequest): Observable<ReviewResponse>{
    return this.http.post<ReviewResponse>(`${this.apiBaseUrl}`, reviewDto);
  }

  getReviewsByComplexId(complexId:number): Observable<ReviewResponse[]>{
    return this.http.get<ReviewResponse[]>(`${this.apiBaseUrl}`, {params:{complexId}})
  }

  getReviewByReservationId(reservationId:number):Observable<ReviewResponse>{
    return this.http.get<ReviewResponse>(`${this.apiBaseUrl}/by-reservation/${reservationId}`);
  }
}
