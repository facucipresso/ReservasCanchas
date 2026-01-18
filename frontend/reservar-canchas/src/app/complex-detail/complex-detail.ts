import { Component, Input, OnInit } from '@angular/core';
import { ComplexModel } from '../models/complex.model';
import { ActivatedRoute } from '@angular/router';
import { FieldModel } from '../models/field.model';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FieldType } from '../models/fieldtype.enum';
import { FloorType } from '../models/floortype.enum';
import { ButtonModule } from 'primeng/button';
import { PanelModule } from 'primeng/panel';
import { Message } from 'primeng/message';
import { Select, SelectModule } from 'primeng/select';
import { Complex } from '../services/complex';
import { Dialog } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { Form, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputNumber } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { FloatLabel } from 'primeng/floatlabel';
import { WeekDay } from '../models/weekday.enum';
import { Complexservices } from '../services/complexservices';
import { ComplexServiceModel } from '../models/complexservice.model';
import { Checkbox } from 'primeng/checkbox';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Auth } from '../services/auth';
import { Field } from '../services/field';

type EditSection = 'basic' | 'timeslots' | 'services';

@Component({
  selector: 'app-complex-detail',
  imports: [TableModule, CommonModule, ButtonModule, PanelModule, Message, Select, Dialog, InputTextModule, ReactiveFormsModule,
    InputNumber,TextareaModule,SelectModule, FloatLabel, Checkbox, Toast
  ],
  templateUrl: './complex-detail.html',
  styleUrl: './complex-detail.css',
  providers: [MessageService]
})
export class ComplexDetail implements OnInit {
  complex!: ComplexModel;
  userId!: number | null;
  isAdminComplex = false;
  backendUrl = 'https://localhost:7004';
  allServices !: ComplexServiceModel[];
  fields!: FieldModel[];
  reservations!:any;
  visible: boolean = false;
  visibleFieldModal:boolean = false;
  editBasicInfoForm!:FormGroup;
  editTimeSlotsForm!:FormGroup;
  editServicesForm!:FormGroup;
  activeSection:EditSection = 'basic';
  weekDays : WeekDay[] = Object.values(WeekDay);
  availableHours = [
    '08:00','09:00','10:00','11:00','12:00','13:00',
    '14:00','15:00','16:00','17:00','18:00','19:00',
    '20:00','21:00','22:00','23:00','00:00','01:00','02:00'
  ]
  invalidSchedulesError: string | null = null;

  fieldForm!: FormGroup;
  fieldTypes = [
    { label: 'Fútbol 5', value: FieldType.Futbol5 },
    { label: 'Fútbol 7', value: FieldType.Futbol7 },
    { label: 'Fútbol 11', value: FieldType.Futbol11 }
  ]
  floorTypes = [
    {label:'Cesped Natural', value: FloorType.CespedNatural},
    {label:'Cesped Sintético', value: FloorType.CespedSintetico},
    {label:'Parquet', value:FloorType.Parquet},
    {label:'Cemento', value:FloorType.Cemento}
  ]

  constructor(private route: ActivatedRoute, private complexService: Complex, private fb: FormBuilder,
     private servicesComplex: Complexservices, private messageService: MessageService, private authService: Auth,
     private fieldService: Field) {}


  ngOnInit(): void {
    this.userId = parseInt(this.authService.getUserId(), 10);
    console.log('userId parseado:', this.userId);
    this.editBasicInfoForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      province: ['', Validators.required],
      locality: ['', Validators.required],
      street: ['', Validators.required],
      number: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d*$/)]],
      cbu: ['', [
        Validators.required,
        Validators.pattern(/^\d*$/),
        Validators.minLength(22),
        Validators.maxLength(22)
      ]],
      percentageSign: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      startIlumination: ['', Validators.required],
      aditionalIlumination: [0, Validators.required]
    });

    this.editTimeSlotsForm = this.fb.group({
      timeSlots: this.fb.array(
        this.weekDays.map((day) => {
          return this.fb.group({
            weekDay:[day],
            initTime:['',Validators.required],
            endTime:['',Validators.required]
          })
        })
      )
    })
    const complexId = this.route.snapshot.paramMap.get('id');
    this.fieldForm = this.fb.group({
      complexId:['', Validators.required],
      fieldType:['',Validators.required],
      floorType:['',Validators.required],
      hourPrice:[null,Validators.required],
      ilumination:[false],
      covered:[false],
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
    this.loadComplex();

    this.servicesComplex.getAllServices().subscribe({
      next: (services) => {
        this.allServices = services;
        this.tryBuildServicesForm();
        console.log(services);
    }});

    this.fieldForm.get('timeSlots')?.valueChanges.subscribe(() => {
      this.validateSchedules();
      }
    );
  }

  setSection(section:EditSection){
    this.activeSection = section;
  }

  loadComplex() {
    const complexId = 1

    this.complexService.getComplexById(1).subscribe({
      next: (complex) => {
        this.complex = complex;
        this.loadFields();
        if(this.userId && this.complex.userId === this.userId ){
          this.isAdminComplex = true;
        }else{
          console.log(this.userId)
          console.log(this.complex.userId)
        }
        this.loadFormBasicInfo();
        this.loadFormTimeSlots();
        this.tryBuildServicesForm();
      }
    });
  }

  loadFields(){
    this.fieldService.getFieldsByComplexId(this.complex.id).subscribe({
      next: fields => {
        this.fields = fields;
      }
    })
  }

  loadFormBasicInfo() {
    this.editBasicInfoForm.patchValue({
      name: this.complex.name,
      description: this.complex.description,
      province: this.complex.province,
      locality: this.complex.locality,
      street: this.complex.street,
      number: this.complex.number,
      phone: this.complex.phone,
      cbu: this.complex.cbu,
      percentageSign: this.complex.percentageSign,
      startIlumination: this.complex.startIlumination.slice(0,5),
      aditionalIlumination: this.complex.aditionalIlumination
    });
  }

  loadFormTimeSlots() {
    const timeSlotsComplex = this.complex.timeSlots;
    this.invalidSchedulesError = null;
    const timeSlotsForm = this.editTimeSlotsForm.get('timeSlots') as FormArray;

    timeSlotsForm.controls.forEach(control => {
      const weekDay = control.get('weekDay')?.value;
      const slot = timeSlotsComplex.find(ts => ts.weekDay === weekDay);
      
      if(slot){
        control.patchValue({
          initTime:slot.initTime.slice(0,5),
          endTime: slot.endTime.slice(0,5)
        })
      }
    })
  }

  validateSchedules(): void {
    let timeSlots;
    if(this.visible){
       timeSlots = this.editTimeSlotsForm.get('timeSlots')?.value;
    }else{
       timeSlots = this.fieldForm.get('timeSlots')?.value;
    }

    if (!timeSlots) return;

    const hasInvalidSchedule = timeSlots.some((slot: any) => {
      const initIndex = this.availableHours.indexOf(slot.initTime);
      const endIndex = this.availableHours.indexOf(slot.endTime);
      return initIndex > endIndex && endIndex != -1;
    });

    this.invalidSchedulesError = hasInvalidSchedule? "El horario de apertura no puede ser mayor al de cierre" : null;
  }

  buildServicesForm() {
    const complexServiceIds = this.complex.services.map(s => s.id);
    console.log(complexServiceIds);
    this.editServicesForm = this.fb.group({
      services: this.fb.array(
        this.allServices.map(service =>
          this.fb.control(complexServiceIds.includes(service.id))
        )
      )
    });
  }

  tryBuildServicesForm() {
    if (!this.complex || !this.allServices) return;
    this.buildServicesForm();
  }

  get timeSlotsArray(): FormArray {
    return this.editTimeSlotsForm.get('timeSlots') as FormArray;
  }

  get servicesArray(): FormArray {
    return this.editServicesForm.get('services') as FormArray;
  }

  showDialogComplex() {
    this.activeSection = 'basic';
    this.visible = true;
  }

  onDialogComplexClose() {
    this.loadFormBasicInfo();
    this.loadFormTimeSlots();
    this.buildServicesForm()
  }

  showDialogField(){
    this.visibleFieldModal = true;
    this.fieldForm.patchValue({
      complexId: this.complex.id
    });
  }

  onDialogFieldClose(){
    this.fieldForm.reset(); 
    this.fieldForm.setControl(
      'timeSlotsField',
      this.fb.array(
        this.weekDays.map(day =>
          this.fb.group({
            weekDay: [day],
            initTime: ['',Validators.required],
            endTime: ['',Validators.required],
          })
        )
      )
    )
  }

  get timeSlotsFieldArray(): FormArray<FormGroup> {
    return this.fieldForm.get('timeSlotsField') as FormArray<FormGroup>;
  }


  getSelectableHours(fieldId: number, init: string, end: string) {
    const hours = this.generateHourRange(init, end);

    const fieldReservation = this.reservations.fieldsWithReservedHours
      .find((r:any) => r.fieldId === fieldId);

    const reserved = fieldReservation?.reservedHours ?? [];

    return hours.map(h => ({
      hour: h,
      disabled: reserved.includes(h)
    }));
  }

  private generateHourRange(start: string, end: string): string[] {
    const hours: string[] = [];

    let [startH] = start.split(':').map(Number);
    let [endH] = end.split(':').map(Number);

  // si el horario pasa la medianoche (ej: 08:00 → 02:00)
    const passesMidnight = endH < startH;

    while (true) {
      const formatHour = startH.toString().padStart(2, '0') + ':00';
      hours.push(formatHour);

      if (!passesMidnight && startH === endH) break;

      startH = (startH + 1) % 24;

      if (passesMidnight && startH === endH) {
        hours.push(end.padStart(5, '0'));
        break;
      }
    }

    return hours;
  }

  onSubmitBasicInfo(){
    const infoBasic = this.editBasicInfoForm.value;
    const complexId = this.complex.id;

    this.complexService.updateComplexBasicInfo(infoBasic,complexId).subscribe({
      next: response => {
        console.log("INFORMACION BASICA DEL COMPLEJO ACTUALIZADA EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Complejo actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex();
        this.editBasicInfoForm.markAsPristine();
        this.loadFormBasicInfo();
      },
      error: (err) => {
        console.log('ERROR DEL BACKEND:', err);
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';

        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 2000
        })
      }
    })
    console.log(infoBasic);
  }

  onSubmitTimeSlots(){
    const timeSlots = this.editTimeSlotsForm.value;
    const complexId = this.complex.id;
    this.invalidSchedulesError = null;
    this.validateSchedules();

    if (this.invalidSchedulesError) {
      return;
    }

    this.complexService.updateComplexTimeSlots(timeSlots,complexId).subscribe({
      next: response => {
        console.log("HORARIOS DEL COMPLEJO ACTUALIZADOS EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Horarios del complejo actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex();
        this.editTimeSlotsForm.markAsPristine();
        this.loadFormTimeSlots();
      },
      error: (err) => {
        console.log('ERROR DEL BACKEND:', err);
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';

        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 2000
        })
      }
    })
  }

  onSubmitServices(){
    const values: boolean[] = this.editServicesForm.value.services;

    const selectedServiceIds = values
      .map((checked, i) => checked ? this.allServices[i].id : null)
      .filter(id => id !== null);
    const complexId = this.complex.id;

    const body = {
      servicesIds: selectedServiceIds
    }
    console.log(selectedServiceIds);

    this.complexService.updateComplexServices(body,complexId).subscribe({
      next: response => {
        console.log("SERVICIOS DEL COMPLEJO ACTUALIZADOS EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Servicios del complejo actualizados exitosamente.',
          life: 2000
        })        
        this.loadComplex();
        this.editServicesForm.markAsPristine();  
      },
      error: err => {
        console.log('ERROR DEL BACKEND:', err);
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';

        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 2000
        })        
      } 
    })
  }

  onSubmitCreateField(){
    this.invalidSchedulesError = null;
    this.validateSchedules();
    if (this.invalidSchedulesError) {
      return;
    }
    const field = this.fieldForm.value;
    this.fieldService.createField(field).subscribe({
      next: (response) => {
        console.log("COMPLEJO CREADO EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Cancha creada exitosamente.',
          life: 2000
        })
        this.fieldForm.markAsPristine();
        this.loadComplex();
      },
      error: (err) => {
        console.log('ERROR DEL BACKEND:', err);
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';

        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 2000
        })
      }
    })
  }

  onSubmitUpdateBasicInfoField(){
    
  }
}
