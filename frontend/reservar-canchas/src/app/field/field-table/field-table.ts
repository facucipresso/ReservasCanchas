import { Component, EventEmitter, Input, OnInit, Output, OnChanges, SimpleChanges } from '@angular/core';
import { FieldDetailModel } from '../../models/field/field.model';
import { CommonModule } from '@angular/common';
import { ReservationsForField } from '../../models/reservation/reservationsforfield.model';
import { TableModule } from 'primeng/table';
import { Select, SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { Reservation } from '../../services/reservation';
import { ComplexDetailModel } from '../../models/complex/complexdetail.model';
import { Dialog } from 'primeng/dialog';
import { TextareaModule } from 'primeng/textarea';
import { FieldTypePipe } from '../../pipes/field-type-pipe';
import { FloorTypePipe } from '../../pipes/floor-type-pipe';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-field-table',
  imports: [CommonModule,TableModule,SelectModule,Select, ButtonModule, FormsModule, Dialog, TextareaModule, FieldTypePipe, FloorTypePipe],
  templateUrl: './field-table.html',
  styleUrl: './field-table.css',
})
export class FieldTable implements OnInit, OnChanges{
  @Input() complex!: ComplexDetailModel;
  @Input() fields!: FieldDetailModel[];
  @Input() isAdmin !: boolean;
  @Input() selectedDate !: Date;

  @Output() editField = new EventEmitter<FieldDetailModel>();
  @Output() deleteField = new EventEmitter<number>();
  @Output() reserveField = new EventEmitter<{field:FieldDetailModel, hour:string}>();
  @Output() recurringBlockField = new EventEmitter<FieldDetailModel>();
  @Output() specificBlockField = new EventEmitter<{field:FieldDetailModel, hour:string, reason:string}>

  dayIndex!: number;
  reservationsForField: ReservationsForField[] = [];
  selectedHours: {[fieldId:number]:string} = {};
  visible = false;
  reasonBlock !:string;
  selectedFieldForSpecificBlock: FieldDetailModel | null = null;

  constructor(private reservationService: Reservation, private messageService: MessageService) {}

  ngOnInit(){
    this.dayIndex = this.getWeekDayIdx(this.selectedDate);
    this.loadReservations(this.selectedDate);
  }  

  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectedDate'] && !changes['selectedDate'].firstChange) {
      this.dayIndex = this.getWeekDayIdx(this.selectedDate);
      this.loadReservations(this.selectedDate);
    }
  }

  private loadReservations(date?: Date) {
    if (!this.complex || !this.complex.id || !date) return;
    const dateStr = date.toISOString().split('T')[0];
    this.reservationService.getReservationsByDateForComplex(this.complex.id, dateStr)
      .subscribe({
        next: (data)=>{
          this.reservationsForField = data.fieldsWithReservedHours;
        },
        error: (err)=>{
          const backendError = err?.error;
          this.messageService.add({
            severity: 'error',
            summary: backendError?.title || 'Error',
            detail: backendError?.detail || 'Error desconocido',
            life: 2000
          });
        }
      });
  }

  public refreshReservations() {
    this.loadReservations(this.selectedDate);
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

    if (startH === endH) return [];

    // Caso normal (no cruza medianoche)
    if (endH > startH) {
      for (let h = startH; h < endH; h++) {
        hours.push(h.toString().padStart(2, '0') + ':00');
      }
      return hours;
    }

  // Caso cruza medianoche (18 a 02)
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

  getTimeSlotForField(field: FieldDetailModel) {
    if (!this.selectedDate || !field.timeSlotsField?.length) {
      return null;
    }

    const idx = this.getWeekDayIdx(this.selectedDate);

    return field.timeSlotsField[idx] ?? null;
  }

  onEdit(field:FieldDetailModel){
    console.log(field);
    this.editField.emit(field);
  }

  onDelete(fieldId:number){
    this.deleteField.emit(fieldId);
  }

  onReserve(field:FieldDetailModel, hour:string){
    this.reserveField.emit({field, hour});
  }

  onRecurringBlock(field: FieldDetailModel) {
    console.log('Bloqueo recurrente para la cancha con ID:', field);
    this.recurringBlockField.emit(field);
  }

  openSpecificBlockDialog(field:FieldDetailModel){
    this.visible = true;
    this.selectedFieldForSpecificBlock = field;
  }

  closeSpecificBlockDialog(){
    this.visible = false;
    this.reasonBlock = '';
    this.selectedFieldForSpecificBlock = null;
  }

  onConfirmSpecificBlock(){
    if (this.selectedFieldForSpecificBlock && this.selectedHours[this.selectedFieldForSpecificBlock.id]){
      const hour = this.selectedHours[this.selectedFieldForSpecificBlock?.id];
      this.specificBlockField.emit({
        field: this.selectedFieldForSpecificBlock,
        hour,
        reason: this.reasonBlock
      })
    }
    this.closeSpecificBlockDialog();
  }

}
