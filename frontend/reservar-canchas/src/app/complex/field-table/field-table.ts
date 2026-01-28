import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FieldModel } from '../../models/field.model';
import { CommonModule } from '@angular/common';
import { ReservationsForField } from '../../models/reservation/reservationsforfield.model';
import { TableModule } from 'primeng/table';
import { Select, SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FieldType } from '../../models/fieldtype.enum';
import { FloorType } from '../../models/floortype.enum';
import { FormsModule } from '@angular/forms';
import { Reservation } from '../../services/reservation';
import { ComplexModel } from '../../models/complex.model';


@Component({
  selector: 'app-field-table',
  imports: [CommonModule,TableModule,SelectModule,Select, ButtonModule, FormsModule],
  templateUrl: './field-table.html',
  styleUrl: './field-table.css',
})
export class FieldTable implements OnInit{
  @Input() complex!: ComplexModel;
  @Input() fields!: FieldModel[];
  @Input() isAdmin !: boolean;
  @Input() selectedDate !: Date;
  @Output() editField = new EventEmitter<FieldModel>();
  @Output() deleteField = new EventEmitter<number>();
  @Output() reserveField = new EventEmitter<{field:FieldModel, hour:string}>();
  dayIndex!: number;
  reservationsForField: ReservationsForField[] = [];
  selectedHours: {[fieldId:number]:any} = {};

  constructor(private reservationService: Reservation) {}

  ngOnInit(){
    this.dayIndex = this.getWeekDayIdx(this.selectedDate);
    this.reservationService.getReservationsByDateForComplex(this.complex.id, this.selectedDate.toISOString().split('T')[0])
      .subscribe({
        next: (data)=>{
          this.reservationsForField = data.fieldsWithReservedHours;
        },
        error: (err)=>{
          console.error('Error al cargar las reservas para las canchas:', err);
        }
      }); 
  }  

  getSelectableHours(fieldId: number, init: string, end: string) {
    const hours = this.generateHourRange(init, end);

    const fieldReservation = this.reservationsForField
      .find((r:any) => r.fieldId === fieldId);

    const rawReserved = fieldReservation?.reservedHours ?? [];

    const reserved = rawReserved.map((h: string) => h.substring(0, 5));

    const now = new Date();
    const isToday = this.selectedDate.toDateString() === now.toDateString();
    const currentHour = now.getHours();
    return hours.map(h => {
      const slotHour = parseInt(h.split(':')[0], 10);
      const isPastHour = isToday && slotHour <= currentHour && (slotHour != 0 && slotHour != 1);
      return {
        hour: h,
        disabled: reserved.includes(h) || isPastHour
      };
    });
  }

  private generateHourRange(start: string, end: string): string[] {
    if (!start || !end) return [];

    const hours: string[] = [];

    let startH = Number(start.split(':')[0]);
    let endH = Number(end.split(':')[0]);

    // Si abre y cierra a la misma hora → cerrado
    if (startH === endH) return [];

    // Caso normal (no cruza medianoche)
    if (endH > startH) {
      for (let h = startH; h < endH; h++) {
        hours.push(h.toString().padStart(2, '0') + ':00');
      }
      return hours;
    }

  // Caso cruza medianoche (ej 18 → 02)
    for (let h = startH; h < 24; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    for (let h = 0; h < endH; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    return hours;
  }

  getWeekDayIdx(date: Date): number {
    const d = date.getDay();
    return d === 0 ? 6 : d-1;
  }

  getTimeSlotForField(field: FieldModel) {
    if (!this.selectedDate || !field.timeSlotsField?.length) {
      return null;
    }

    const idx = this.getWeekDayIdx(this.selectedDate);

    return field.timeSlotsField[idx] ?? null;
  }

  onEdit(field:FieldModel){
    console.log(field);
    this.editField.emit(field);
  }

  onDelete(fieldId:number){
    this.deleteField.emit(fieldId);
  }

  onReserve(field:FieldModel, hour:string){
    this.reserveField.emit({field, hour});
  }
}
