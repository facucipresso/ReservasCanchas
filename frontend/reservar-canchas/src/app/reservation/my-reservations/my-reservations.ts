import { Component, OnInit } from '@angular/core';
import { Reservation } from '../../services/reservation';
import { ReservationForUserResponse } from '../../models/reservation/reservationforuserresponse.model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { ReservationState } from '../../models/reservation/reservationstate.enum';

@Component({
  selector: 'app-my-reservations',
  imports: [TableModule, ButtonModule, CommonModule],
  templateUrl: './my-reservations.html',
  styleUrl: './my-reservations.css',
})
export class MyReservations implements OnInit {

  allReservations: ReservationForUserResponse[] = [];
  filteredReservations: ReservationForUserResponse[] = [];
  selectedStatus: string = 'all';

  constructor(private reservationService: Reservation){}

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
    this.filteredReservations = data;
    });
  }

  applyFilter(){
    if(this.selectedStatus === 'all'){
      this.filteredReservations = this.allReservations;
    }else if(this.selectedStatus === 'Cancelada'){
      this.filteredReservations = this.allReservations.filter(reservation =>{ 
        return reservation.state === ReservationState.CanceladoConDevolucion || 
               reservation.state === ReservationState.CanceladoSinDevolucion ||
               reservation.state === ReservationState.CanceladoPorAdmin;});
    } else {
      this.filteredReservations = this.allReservations.filter(reservation => reservation.state === this.selectedStatus);
    }
  }
}
