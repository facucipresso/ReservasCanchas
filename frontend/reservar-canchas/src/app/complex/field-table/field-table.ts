import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FieldModel } from '../../models/field.model';
import { CommonModule } from '@angular/common';
import { ReservationsForField } from '../../models/reservationsforfield.model';
import { TableModule } from 'primeng/table';
import { Select } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FieldType } from '../../models/fieldtype.enum';
import { FloorType } from '../../models/floortype.enum';


@Component({
  selector: 'app-field-table',
  imports: [CommonModule,TableModule,Select, ButtonModule],
  templateUrl: './field-table.html',
  styleUrl: './field-table.css',
})
export class FieldTable implements OnInit{
  @Input() fields!: FieldModel[];
  @Input() isAdmin !: boolean;
  @Input() selectedDate !: Date;
  @Output() editField = new EventEmitter<FieldModel>();
  @Output() deleteField = new EventEmitter<number>();
  dayIndex!: number;
  reservationsForField: ReservationsForField[] = [{fieldId:6,reservedHours:['12:00','13:00']}];


  ngOnInit(){
    this.dayIndex = this.getWeekDayIdx(this.selectedDate);
  }  

  getSelectableHours(fieldId: number, init: string, end: string) {
    const hours = this.generateHourRange(init, end);

    const fieldReservation = this.reservationsForField
      .find((r:any) => r.fieldId === fieldId);

    const reserved = fieldReservation?.reservedHours ?? [];

    return hours.map(h => ({
      hour: h,
      disabled: reserved.includes(h)
    }));
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
}
