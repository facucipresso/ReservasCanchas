import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { WeekDay } from '../../models/weekday.enum';
import { ComplexServiceModel } from '../../models/complexservice.model';
import { ComplexModel } from '../../models/complex.model';
import { FloatLabel } from 'primeng/floatlabel';
import { InputText } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { Checkbox } from 'primeng/checkbox';
import { InputNumber } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { FileUploadHandlerEvent, FileUploadModule } from 'primeng/fileupload';
import { TooltipModule } from 'primeng/tooltip';
type EditSection = 'basic' | 'timeslots' | 'services' | 'image';

@Component({
  selector: 'app-editcomplex-form',
  imports: [FloatLabel,InputText,SelectModule,Checkbox,ReactiveFormsModule,
    InputNumber,TextareaModule,ButtonModule,CommonModule, FileUploadModule, TooltipModule],
  templateUrl: './editcomplex-form.html',
  styleUrl: './editcomplex-form.css',
})
export class EditcomplexForm implements OnInit {

  @Input() complex!: ComplexModel;
  @Input() allServices!: ComplexServiceModel[];
  @Output() saveBasic = new EventEmitter<any>();
  @Output() saveTimeSlots = new EventEmitter<any>();
  @Output() saveServices = new EventEmitter<number[]>();
  @Output() saveImage = new EventEmitter<any>();
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
  selectedImage :File | null = null;
  imageErrorMessage:string | null = null;
  previewUrl: string | null = null;

  constructor(private fb: FormBuilder){}

  ngOnInit(){
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

    this.editTimeSlotsForm.get('timeSlots')?.valueChanges.subscribe(() => {
      this.validateSchedules();
      if(this.invalidSchedulesError){
        this.editTimeSlotsForm.markAsPristine();
      }
    });

    this.loadFormBasicInfo();
    this.loadFormTimeSlots();
    this.buildServicesForm();
  }

  get timeSlotsArray(): FormArray {
    return this.editTimeSlotsForm.get('timeSlots') as FormArray;
  }

  get servicesArray(): FormArray {
    return this.editServicesForm.get('services') as FormArray;
  }

  setSection(section:EditSection){
    this.activeSection = section;
  }

  buildServicesForm() {
    if (!this.allServices || !this.complex){
       return;
    }
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
    const timeSlots = this.editTimeSlotsForm.get('timeSlots')?.value;

    if (!timeSlots) return;

    const hasInvalidSchedule = timeSlots.some((slot: any) => {
      const initIndex = this.availableHours.indexOf(slot.initTime);
      const endIndex = this.availableHours.indexOf(slot.endTime);
      return initIndex > endIndex && endIndex != -1;
    });

    this.invalidSchedulesError = hasInvalidSchedule? "El horario de apertura no puede ser mayor al de cierre" : null;
  }

  onSubmitBasicInfo() {
    console.log('InfoBasic: ',this.editBasicInfoForm.value);
    if (this.editBasicInfoForm.valid) {
      this.saveBasic.emit(this.editBasicInfoForm.value);
      this.editBasicInfoForm.markAsPristine();
    }
  }

  onSubmitTimeSlots() {
    this.invalidSchedulesError = null;
    this.validateSchedules();

    if (this.invalidSchedulesError) {
      return;
    }
    console.log('TimeSlots: ',this.editTimeSlotsForm.value);

    this.saveTimeSlots.emit(this.editTimeSlotsForm.value);
    this.editTimeSlotsForm.markAsPristine();
    
  }

  onSubmitServices() {
    const values = this.editServicesForm.value.services;
    const ids = values
      .map((v: boolean, i: number) => v ? this.allServices[i].id : null)
      .filter(Boolean);
    console.log('ServicesIds:', ids);
    this.saveServices.emit(ids);
    this.editServicesForm.markAsPristine();
  }

  onFileSelect(event: FileUploadHandlerEvent){
    const file = event.files[0];
    this.selectedImage = file;
    console.log(this.selectedImage);

    this.imageErrorMessage = null;

    const validTypes = ['image/jpeg', 'image/png', 'image/jpg'];
    if (!validTypes.includes(this.selectedImage!.type)) {
      this.imageErrorMessage = 'Formato de imagen inválido (solo JPG o PNG)';
      this.selectedImage = null;
      return;
    }

    const maxSizeInBytes = 5 * 1024 * 1024; 
    if (this.selectedImage!.size > maxSizeInBytes) {
      this.imageErrorMessage = 'La imagen no puede superar 5 MB de tamaño';
      this.selectedImage = null;
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.previewUrl = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  clearFile(){
    this.selectedImage = null;
    this.imageErrorMessage = null;
    this.previewUrl = null;
  }

  onSubmitImage(){
    const form= new FormData();

    if (!this.selectedImage) {
      this.imageErrorMessage = "La imagen del complejo es obligatoria"
      return;
    }

    form.append('image', this.selectedImage, this.selectedImage?.name);

    this.saveImage.emit(form);

    this.clearFile();
  }
}
