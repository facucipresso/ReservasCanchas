import { Component, OnInit } from '@angular/core';
import { Reservation } from '../../services/reservation';
import { ReservationForUserResponse } from '../../models/reservation/reservationforuserresponse.model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule, Location } from '@angular/common';
import { ReservationState } from '../../models/reservation/reservationstate.enum';
import { ReservationDetail } from '../reservation-detail/reservation-detail';
import { ReservationList } from '../reservation-list/reservation-list';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-my-reservations',
  imports: [TableModule, ButtonModule, CommonModule, ReservationDetail, ReservationList],
  templateUrl: './my-reservations.html',
  styleUrl: './my-reservations.css',
})
export class MyReservations implements OnInit {

  allReservations: ReservationForUserResponse[] = [];
  selectedReservationId!: number | null;
  selectedStatus: string = 'all';
  
  constructor(private reservationService:Reservation, private route:ActivatedRoute, private router:Router,
    private location:Location
  ){}

  ngOnInit(){
    this.reservationService.getMyReservations().subscribe( (data) =>{
      data.sort((a, b) => {

        if (a.date < b.date) return 1;  
        if (a.date > b.date) return -1; 
      
        if (a.startTime < b.startTime) return 1;
        if (a.startTime > b.startTime) return -1;
      
        return 0; 
      });

    this.allReservations = data;
    });

    this.route.queryParamMap.subscribe(params => {
      const reservationId = params.get('reservationId');
  
      if (reservationId) {
        this.selectedReservationId = +reservationId;
      }
    });
  }


  onReservationStateUpdated(newState: ReservationState) {
    if (!this.selectedReservationId) return;

    const index = this.allReservations.findIndex(r => r.reservationId === this.selectedReservationId);

    if (index !== -1) {
      const updatedReservations = [...this.allReservations];
      updatedReservations[index].reservationState = newState;
      this.allReservations = updatedReservations;
    }
  }

  onAccessDenied(){
    this.selectedReservationId = null;
    this.router.navigate(['reservations'], {
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
}
