import { Component, OnInit } from '@angular/core';
import { ReservationForUserResponse } from '../../models/reservation/reservationforuserresponse.model';
import { Reservation } from '../../services/reservation';
import { ReservationState } from '../../models/reservation/reservationstate.enum';
import { ReservationList } from '../reservation-list/reservation-list';
import { ReservationDetail } from '../reservation-detail/reservation-detail';
import { CommonModule, Location } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Complex } from '../../services/complex';
import { ComplexDetailModel } from '../../models/complex/complexdetail.model';
import { FieldDetailModel } from '../../models/field/field.model';
import { Field } from '../../services/field';
import { ComplexStats } from '../../models/complex/complexstats.model';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-complex-reservations',
  imports: [TableModule, ButtonModule, CommonModule, ReservationDetail, ReservationList],
  templateUrl: './complex-reservations.html',
  styleUrl: './complex-reservations.css',
})
export class ComplexReservations implements OnInit {

  allReservations: ReservationForUserResponse[] = [];
  selectedReservationId!: number | null;
  selectedStatus: string = 'all';
  complexId!: number;
  complex!: ComplexDetailModel;
  fields!: FieldDetailModel[];
  complexStats!: ComplexStats;
  currentFilters: { date: string, fieldId: number | null } = {
    date: new Date().toLocaleDateString('en-CA'),
    fieldId: null
  };

  constructor(private reservationService: Reservation, private route:ActivatedRoute, private complexService:Complex, 
    private fieldService: Field, private router:Router, private messageService:MessageService, private location: Location){}
  
  ngOnInit(){
    this.route.paramMap.subscribe(params => {

      const idParam = params.get('id');
      if (idParam) {
        this.complexId = +idParam; 
        this.complexService.getComplexById(this.complexId).subscribe((res) =>{
          this.complex = res;
        })
        this.fieldService.getFieldsByComplexId(this.complexId).subscribe((res) => {
          this.fields = res;
          console.log(this.fields);
        })

        const reservationId = this.route.snapshot.queryParamMap.get('reservationId');

        if(reservationId){
          this.reservationService.getAdminReservationDetail(this.complexId,+reservationId).subscribe((res) => {
            this.currentFilters.date = res.date;
            this.selectedReservationId = +reservationId;
            this.loadReservationsByComplexAndDate(this.complexId, res.date);
            this.refreshStats();
          })
        }else{
          const dateNow = new Date().toLocaleDateString('en-CA');
          console.log(dateNow);
          this.loadReservationsByComplexAndDate(this.complexId, dateNow)
          this.refreshStats();
        }
      }
    })
  }

  private refreshStats() {
    console.log(this.complexId, this.currentFilters.date, this. currentFilters.fieldId);
    this.complexService.getComplexStats(this.complexId, this.currentFilters.date, this.currentFilters.fieldId).subscribe((stats) => {
      this.complexStats = stats;
    });
  }

  private loadReservationsByComplexAndDate(complexId:number, date:string){
    this.reservationService.getReservationsByComplexAndDate(complexId,date).subscribe({
      next: (reservations) => {
        this.allReservations = this.sortReservations(reservations);
      },
      error: (err) => {
        this.showBackendError(err);
        this.allReservations = [];
      }
    }) 
  }

  private loadReservationsByFieldAndDate(fieldId:number, date:string){
    this.reservationService.getReservationsByFieldAndDate(fieldId,date).subscribe({
      next: (reservations) => {
        this.allReservations = this.sortReservations(reservations);
      },
      error: (err) => {
        this.showBackendError(err);
        this.allReservations = [];
      }
    })
  }
  
  
  onReservationStateUpdated(newState: ReservationState) {
    if (!this.selectedReservationId) return;
  
    const index = this.allReservations.findIndex(r => r.reservationId === this.selectedReservationId);
  
    if (index !== -1) {
      const updatedReservations = [...this.allReservations];
      updatedReservations[index].reservationState = newState;
      this.allReservations = updatedReservations;
    }

    this.refreshStats();
  }

  searchReservations(filters: { date: Date, fieldId: number | null }) {
    const dateOnly = filters.date.toISOString().substring(0, 10);
    this.currentFilters = { date: dateOnly, fieldId: filters.fieldId };
    if(filters.fieldId == null){
      this.loadReservationsByComplexAndDate(this.complexId, dateOnly);
    }else{
      this.loadReservationsByFieldAndDate(filters.fieldId, dateOnly);
    }
    this.refreshStats();
  }

  private sortReservations(data: ReservationForUserResponse[]): ReservationForUserResponse[] {
    return data.sort((a, b) => {
      if (a.date < b.date) return 1;
      if (a.date > b.date) return -1;
      if (a.startTime < b.startTime) return 1;
      if (a.startTime > b.startTime) return -1;
      return 0;
    });
  }

  onAccessDenied(){
    this.selectedReservationId = null;
    this.router.navigate([`admin/complexes/${this.complexId}/reservations`], {
      replaceUrl: true
    });
  }

  onReservationSelected(reservationId: number) {
    if (this.selectedReservationId === reservationId) return;

    this.selectedReservationId = reservationId;
    const newUrl = this.router.createUrlTree([], {
      relativeTo: this.route,
      queryParams: { reservationId },
      queryParamsHandling: 'merge'
    }).toString();
    this.location.replaceState(newUrl);
  }

  private showBackendError(err: any, life = 2000): void {
    const backendError = err?.error;
    this.messageService.add({
      severity: 'error',
      summary: backendError?.title || 'Error',
      detail: backendError?.detail || 'Error desconocido',
      life
    });
  }
}
