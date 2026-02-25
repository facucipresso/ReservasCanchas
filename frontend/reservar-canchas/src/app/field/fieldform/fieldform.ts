import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { WeekDay } from '../../models/complex/weekday.enum';
import { FieldType } from '../../models/field/fieldtype.enum';
import { FloorType } from '../../models/field/floortype.enum';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { Checkbox } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { InputNumber } from 'primeng/inputnumber';
import { ComplexDetailModel } from '../../models/complex/complexdetail.model';
import { FieldDetailModel } from '../../models/field/field.model';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FieldTypePipe } from '../../pipes/field-type-pipe';
import { FloorTypePipe } from '../../pipes/floor-type-pipe';
import { AVAILABLE_HOURS } from '../../constants/available-hours';


@Component({
  selector: 'app-fieldform',
  imports: [ReactiveFormsModule, SelectModule,InputTextModule,Checkbox,ButtonModule, CommonModule, InputNumber, RadioButtonModule,FieldTypePipe, FloorTypePipe],
  templateUrl: './fieldform.html',
  styleUrl: './fieldform.css',
})
export class Fieldform implements OnInit {

  @Input() formMode !:string;
  @Input() field :FieldDetailModel | undefined;
  @Input() complex !: ComplexDetailModel;

  @Output() create = new EventEmitter<any>();
  @Output() updateBasic = new EventEmitter<any>();
  @Output() updateTimeSlots = new EventEmitter<any>();
  @Output() updateState = new EventEmitter<any>();

  weekDays : WeekDay[] = Object.values(WeekDay);
  availableHours = AVAILABLE_HOURS;
  fieldTypeOptions: any[] = [];
  floorTypeOptions: any[] = [];
  invalidSchedulesError: string | null = null;
  fieldForm!: FormGroup;

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
        illumination:[false],
        covered:[false]
      }),
      timeSlotsField: this.fb.array(
        this.weekDays.map((day) =>
          this.fb.group({
            weekDay: [day],
            startTime: ['',Validators.required],   
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

    this.fieldTypeOptions = Object.values(FieldType).map(val => ({
      label: val, 
      value: val  
    }));

    this.floorTypeOptions = Object.values(FloorType).map(val => ({
      label: val, 
      value: val  
    }));
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
        illumination: this.field?.illumination,
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
          startTime: fieldSlot.startTime.slice(0, 5),
          endTime: fieldSlot.endTime.slice(0, 5)
          });
        }
    });
  }

  
  validateSchedules(): void {
    const timeSlots = this.fieldForm.get('timeSlotsField')?.value;
    if (!timeSlots) return;

    const hasInvalidSchedule = timeSlots.some((slot: any) => {
      const initIndex = this.availableHours.indexOf(slot.startTime);
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

      const currentInit = slotGroup.get('startTime')?.value;
      const currentEnd = slotGroup.get('endTime')?.value;

      const newInit = complexSlot.startTime.slice(0, 5);
      const newEnd = complexSlot.endTime.slice(0, 5);

      if (currentInit !== newInit || currentEnd !== newEnd) {
        hasChanges = true;

        slotGroup.patchValue({
          startTime: newInit,
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
