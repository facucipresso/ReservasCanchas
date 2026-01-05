import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ComplexCardModel } from '../models/complexcard.model';
import { ComplexModel } from '../models/complex.model';
import { ComplexCreateModel } from '../models/createcomplex.model';

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

  createComplex(complex:ComplexCreateModel):Observable<ComplexModel>{
    return this.http.post<ComplexModel>(this.apiBaseUrl,complex);
  }
}
