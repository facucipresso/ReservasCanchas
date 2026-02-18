import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ComplexModel } from '../../models/complex.model';
import { CommonModule } from '@angular/common';
import { Message } from 'primeng/message';
import { Panel } from 'primeng/panel';
import { FieldModel } from '../../models/field.model';
import { FieldTable } from '../field-table/field-table';
import { ReviewResponse } from '../../models/reservation/reviewresponse.model';
import { ReviewCard } from '../../review-card/review-card';
import { CarouselModule } from 'primeng/carousel';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { PopoverModule } from 'primeng/popover';
import { ComplexState } from '../../models/complexstate.enum';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-complex-info',
  imports: [CommonModule,Message,Panel, FieldTable, ReviewCard, CarouselModule, RatingModule, FormsModule, PopoverModule, ButtonModule, DialogModule],
  templateUrl: './complex-info.html',
  styleUrl: './complex-info.css',
})
export class ComplexInfo {
  @Input() complex!: ComplexModel;
  @Input() isAdmin!: boolean;
  @Input() fields!: FieldModel[];
  @Input() selectedDate!: Date;
  @Input() reviews: ReviewResponse[] = [];

  @Output() editField = new EventEmitter<FieldModel>();
  @Output() deleteField = new EventEmitter<number>();
  @Output() reserveField = new EventEmitter<{field:FieldModel, hour:string}>();
  @Output() recurringBlockField = new EventEmitter<FieldModel>();
  @Output() stateChanged = new EventEmitter<any>();

  responsiveOptions = [
    {
        breakpoint: '1199px',
        numVisible: 3,
        numScroll: 1
    },
    {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
    },
    {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
    }
  ];

  @ViewChild(FieldTable) fieldTableComponent!: FieldTable;
  backendUrl = 'https://localhost:7004';
  complexState = ComplexState;
  visible = false;
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

  onStateChange(){
    this.stateChanged.emit();
    this.visible = false;
  }

  public refreshFieldReservations() {
    this.fieldTableComponent.refreshReservations();
  }

}
