import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ReservationForUserResponse } from '../../models/reservation/reservationforuserresponse.model';
import { ReservationState } from '../../models/reservation/reservationstate.enum';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { ReservationStatePipe } from '../../pipes/reservation-state-pipe';
import { ComplexModel } from '../../models/complex.model';
import { FieldModel } from '../../models/field.model';
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';
import { DatePicker } from 'primeng/datepicker';

@Component({
  selector: 'app-reservation-list',
  imports: [TableModule, ButtonModule, CommonModule, ReservationStatePipe, SelectModule, FormsModule, DatePicker],
  templateUrl: './reservation-list.html',
  styleUrl: './reservation-list.css',
})
export class ReservationList implements OnChanges, OnInit {

  @Input() allReservations: ReservationForUserResponse[] = [];
  @Input() isAdminView: boolean = false;
  @Input() selectedReservationId!: number | null;
  @Input() complex!: ComplexModel;
  @Input() fields!: FieldModel[];
  @Output() onSelect = new EventEmitter<number>();
  @Output() onSearch = new EventEmitter<{date:Date, fieldId: number | null}>();
  filteredReservations: ReservationForUserResponse[] = [];
  selectedStatus: string = 'all';

  filterDate: Date = new Date(); 
  selectedFieldId: number | null = null; 
  fieldOptions: any[] = [];

  maxDateValid = new Date();

  ngOnInit(): void {
    this.maxDateValid.setDate(this.maxDateValid.getDate() + 7);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['allReservations']) {
      this.applyFilter();
    }

    if (changes['fields'] && this.fields) {
      this.setupFieldOptions();
    }
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

  selectReservation(id:number){
    this.onSelect.emit(id);
  }

  setupFieldOptions() {
    // Creamos la opción "Todas" + las canchas reales
    this.fieldOptions = [
      { label: 'Todas las canchas', value: null },
      ...this.fields.map(f => ({ label: f.name, value: f.id }))
    ];
  }

  onFilterChange() {
    console.log(this.filterDate, this.selectedFieldId);
    this.onSearch.emit({
      date: this.filterDate,
      fieldId: this.selectedFieldId
    });
  }
}
