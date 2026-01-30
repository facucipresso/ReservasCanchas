import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ComplexModel } from '../../models/complex.model';
import { CommonModule } from '@angular/common';
import { Message } from 'primeng/message';
import { Panel } from 'primeng/panel';
import { FieldModel } from '../../models/field.model';
import { FieldTable } from '../field-table/field-table';

@Component({
  selector: 'app-complex-info',
  imports: [CommonModule,Message,Panel, FieldTable],
  templateUrl: './complex-info.html',
  styleUrl: './complex-info.css',
})
export class ComplexInfo {
  @Input() complex!: ComplexModel;
  @Input() isAdmin!: boolean;
  @Input() fields!: FieldModel[];
  @Input() selectedDate!: Date;

  @Output() editField = new EventEmitter<FieldModel>();
  @Output() deleteField = new EventEmitter<number>();
  @Output() reserveField = new EventEmitter<{field:FieldModel, hour:string}>();
  @Output() recurringBlockField = new EventEmitter<FieldModel>();

  @ViewChild(FieldTable) fieldTableComponent!: FieldTable;
  backendUrl = 'https://localhost:7004';
  
  onEditField(field: FieldModel) {
    this.editField.emit(field);
  }

  onDeleteField(fieldId:number){
    this.deleteField.emit(fieldId);
  }

  onReserveField( event:{field:FieldModel, hour:string}){
    this.reserveField.emit(event);
  }

  onRecurringBlockField(field: FieldModel){
    this.recurringBlockField.emit(field);
  }

  public refreshFieldReservations() {
    this.fieldTableComponent.refreshReservations();
  }
}
