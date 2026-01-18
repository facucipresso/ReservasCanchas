import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { WeekDay } from '../../models/weekday.enum';
import { FieldType } from '../../models/fieldtype.enum';
import { FloorType } from '../../models/floortype.enum';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { Checkbox } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { InputNumber } from 'primeng/inputnumber';
import { ComplexModel } from '../../models/complex.model';
import { FieldModel } from '../../models/field.model';
import { RadioButtonModule } from 'primeng/radiobutton';


@Component({
  selector: 'app-fieldform',
  imports: [ReactiveFormsModule, SelectModule,InputTextModule,Checkbox,ButtonModule, CommonModule, InputNumber, RadioButtonModule],
  templateUrl: './fieldform.html',
  styleUrl: './fieldform.css',
})
export class Fieldform implements OnInit {
  weekDays : WeekDay[] = Object.values(WeekDay);
  availableHours = [
    '08:00','09:00','10:00','11:00','12:00','13:00',
    '14:00','15:00','16:00','17:00','18:00','19:00',
    '20:00','21:00','22:00','23:00','00:00','01:00','02:00'
  ]
  fieldTypes = [
      { label: 'Fútbol 5', value: FieldType.Futbol5 },
      { label: 'Fútbol 7', value: FieldType.Futbol7 },
      { label: 'Fútbol 11', value: FieldType.Futbol11 }
    ]
  floorTypes = [
      {label:'Cesped Natural', value: FloorType.CespedNatural},
      {label:'Cesped Sintetico', value: FloorType.CespedSintetico},
      {label:'Parquet', value:FloorType.Parquet},
      {label:'Cemento', value:FloorType.Cemento}
    ]
  invalidSchedulesError: string | null = null;
  fieldForm!: FormGroup;
  @Input() formMode !:string;
  @Input() field :FieldModel | undefined;
  @Input() complex !: ComplexModel;
  @Output() create = new EventEmitter<any>();
  @Output() updateBasic = new EventEmitter<any>();
  @Output() updateTimeSlots = new EventEmitter<any>();
  @Output() updateState = new EventEmitter<any>();
  constructor(private fb:FormBuilder){}

  ngOnInit(){
    this.fieldForm = this.fb.group({
      fieldState:this.fb.group({
        state:['']
      }),
      basic:this.fb.group({
        complexId:[''],
        name:['',Validators.required],
        fieldType:['',Validators.required],
        floorType:['',Validators.required],
        hourPrice:[null,Validators.required],
        ilumination:[false],
        covered:[false]
      }),
      timeSlotsField: this.fb.array(
        this.weekDays.map((day) =>
          this.fb.group({
            weekDay: [day],
            initTime: ['',Validators.required],   
            endTime: ['',Validators.required],
          })
        ) 
      )
    })

    this.fieldForm.get('timeSlotsField')?.valueChanges.subscribe(() => {
      this.validateSchedules();
    });

    if(this.formMode === 'edit'){
      this.loadField();
    }
  }

  get basicForm(): FormGroup {
    return this.fieldForm.get('basic') as FormGroup;
  }

  get timeSlotsFieldArray(): FormArray<FormGroup> {
    return this.fieldForm.get('timeSlotsField') as FormArray<FormGroup>;
  }

  loadField(){
      this.basicForm.patchValue({
        name: this.field?.name,
        fieldType: FieldType[this.field!.fieldType],
        floorType: FloorType[this.field!.floorType],
        hourPrice: this.field?.hourPrice,
        ilumination: this.field?.ilumination,
        covered: this.field?.covered
      })

      this.fieldForm.get('fieldState')?.patchValue({
        state: this.field?.fieldState
      })

      const fieldTimeSlots = this.field?.timeSlotsField

      this.timeSlotsFieldArray.controls.forEach(slotGroup => {
        const weekDay = slotGroup.get('weekDay')?.value;

        const fieldSlot = fieldTimeSlots?.find(ts => ts.weekDay === weekDay);

        if (fieldSlot) {
          slotGroup.patchValue({
          initTime: fieldSlot.initTime.slice(0, 5),
          endTime: fieldSlot.endTime.slice(0, 5)
          });
        }
    });
  }

  
  validateSchedules(): void {
    const timeSlots = this.fieldForm.get('timeSlotsField')?.value;
    if (!timeSlots) return;

    const hasInvalidSchedule = timeSlots.some((slot: any) => {
      const initIndex = this.availableHours.indexOf(slot.initTime);
      const endIndex = this.availableHours.indexOf(slot.endTime);
      return initIndex > endIndex && endIndex != -1;
    });

    this.invalidSchedulesError = hasInvalidSchedule? "El horario de apertura no puede ser mayor al de cierre" : null;
  }

  copyComplexTimeSlots() {
    const complexTimeSlots = this.complex.timeSlots;
    const timeSlotsFieldArray = this.timeSlotsFieldArray;

    let hasChanges = false;

    timeSlotsFieldArray.controls.forEach((slotGroup) => {
      const weekDay = slotGroup.get('weekDay')?.value;
      const complexSlot = complexTimeSlots.find(ts => ts.weekDay === weekDay);

      if (!complexSlot) return;

      const currentInit = slotGroup.get('initTime')?.value;
      const currentEnd = slotGroup.get('endTime')?.value;

      const newInit = complexSlot.initTime.slice(0, 5);
      const newEnd = complexSlot.endTime.slice(0, 5);

      if (currentInit !== newInit || currentEnd !== newEnd) {
        hasChanges = true;

        slotGroup.patchValue({
          initTime: newInit,
          endTime: newEnd
        });
      }
    });

    if (hasChanges) {
      timeSlotsFieldArray.markAsDirty();
    }
  }

  onSubmitCreateField(){
    this.fieldForm.get('basic.complexId')?.setValue(this.complex.id);
    const basic = this.fieldForm.get('basic')!.value;
    const timeSlotsField = this.fieldForm.get('timeSlotsField')!.value;
    const payload = {
      ...basic,
      timeSlotsField
    }
    this.fieldForm.markAsPristine();
    this.create.emit(payload);
  }

  onSubmitBasicInfoEdit(){
    this.updateBasic.emit(this.basicForm.value);
    this.basicForm.markAsPristine();
  }

  onSubmitTimeSlotsEdit(){
    this.updateTimeSlots.emit(this.timeSlotsFieldArray.value);
    this.timeSlotsFieldArray.markAsPristine();
  }

  onSubmitStateEdit(){
    const stateValue = this.fieldForm.get('fieldState.state')?.value;
    this.updateState.emit(stateValue);
    this.fieldForm.get('fieldState')?.markAsPristine();
  }
  
}
