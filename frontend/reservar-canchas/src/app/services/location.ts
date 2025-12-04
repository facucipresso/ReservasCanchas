import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Location {
  
  private apiBaseUrl = 'https://apis.datos.gob.ar/georef/api'
  constructor(private http:HttpClient){}

  getProvinces():Observable<string[]>{
    return this.http.get<any>(`${this.apiBaseUrl}/provincias?campos=nombre`).pipe(
      map(resp => resp.provincias
        .map((provincia:any) => provincia.nombre)
        .sort((a:string,b:string) => a.localeCompare(b)))
    );
  }

  getLocalities(provinceName: string):Observable<string[]>{
    return this.http.get<any>(`${this.apiBaseUrl}/departamentos?provincia=${provinceName}&campos=nombre&max=170`).pipe(
      map(resp => resp.departamentos
        .map((d: any) => d.nombre)
        .sort((a:string, b:string) => a.localeCompare(b))
      )
    );
  }

  getCABALocalities():Observable<string[]>{
    return this.http.get<any>(`${this.apiBaseUrl}/localidades?provincia=CABA&campos=nombre&max=150`).pipe(
      map(resp => resp.localidades
        .map((l:any) => l.nombre)
        .sort((a:string, b:string) => a.localeCompare(b))
      )
    );
  }
}
