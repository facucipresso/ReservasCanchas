import { Component, OnInit } from '@angular/core';
import { Reservation } from '../../services/reservation';
import { ReservationForUserResponse } from '../../models/reservation/reservationforuserresponse.model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { ReservationState } from '../../models/reservation/reservationstate.enum';
import { ReservationDetail } from '../reservation-detail/reservation-detail';
import { ReservationList } from '../reservation-list/reservation-list';
import { ActivatedRoute } from '@angular/router';

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
  
  constructor(private reservationService: Reservation, private route : ActivatedRoute){}

  ngOnInit(){
    this.reservationService.getMyReservations().subscribe( (data) =>{
      data.sort((a, b) => {

        if (a.date < b.date) return 1;  
        if (a.date > b.date) return -1; 
      
        if (a.initTime < b.initTime) return 1;
        if (a.initTime > b.initTime) return -1;
      
        return 0; 
      });

    // Asignamos ya ordenado
    this.allReservations = data;
    });

    // bloque de queryparams por si navega desde el buzon
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
      updatedReservations[index].state = newState;
      this.allReservations = updatedReservations;
    }
  }
}
