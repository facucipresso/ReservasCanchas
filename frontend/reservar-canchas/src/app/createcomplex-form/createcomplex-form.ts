import { CommonModule } from '@angular/common';
import { Component} from '@angular/core';
import { FormArray, FormBuilder,FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AutoComplete, AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { Button } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputNumber, InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { Select, SelectModule } from 'primeng/select';
import { TextareaModule } from 'primeng/textarea';
import { Location } from '../services/location';
import { Complexservices } from '../services/complexservices';
import { ComplexServiceModel } from '../models/complexservice.model';
import { WeekDay } from '../models/weekday.enum';
import { Auth } from '../services/auth';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Complex } from '../services/complex';
import { Router } from '@angular/router';

@Component({
  selector: 'app-createcomplex-form',
  imports: [SelectModule,AutoComplete,CommonModule,AutoCompleteModule, TextareaModule,
    InputTextModule,Button,InputNumber,CheckboxModule,InputNumberModule,ReactiveFormsModule,Select,Toast],
  templateUrl: './createcomplex-form.html',
  styleUrl: './createcomplex-form.css',
  providers:[MessageService]
})
export class CreatecomplexForm {
  createComplexForm!: FormGroup;

  provinces!: string[];
  localities!: string[];
  filteredLocalities!: string[];

  services!:ComplexServiceModel[];

  selectedImage: File | null = null;
  previewUrl: string | null = null;
  imageError: string | null = null;

  weekDays : WeekDay[] = Object.values(WeekDay);

  availableHours = [
    '08:00','09:00','10:00','11:00','12:00','13:00',
    '14:00','15:00','16:00','17:00','18:00','19:00',
    '20:00','21:00','22:00','23:00','00:00','01:00','02:00'
  ]

  invalidSchedulesError: string | null = null;


  constructor(private fb: FormBuilder, private locationService: Location,
    private complexServices: Complexservices, private complexService:Complex, 
    private messageService:MessageService, private router:Router, private authService:Auth) {}

  ngOnInit(): void {
    this.createComplexForm = this.fb.group({
      name: ['',Validators.required],
      description: ['',Validators.required],
      province: ['',Validators.required],
      locality: [{ value: '', disabled: true }, Validators.required],
      street: ['',[Validators.required]],
      number: ['',[Validators.required]],
      phone: ['',[Validators.required,Validators.pattern(/^\d*$/)]],
      percentageSign: [null,[Validators.required,Validators.min(0), Validators.max(100)]],
      startIlumination: ['',Validators.required],
      aditionalIlumination: [null,Validators.required],
      cbu: ['', [Validators.required,Validators.pattern(/^\d*$/),Validators.minLength(22), Validators.maxLength(22)]],
      services: [[]],

      timeSlots: this.fb.array(
        this.weekDays.map((day) =>
          this.fb.group({
            weekDay: [day],
            initTime: ['',Validators.required],   
            endTime: ['',Validators.required],
          })
        ) 
      )
    });

    this.locationService.getProvinces().subscribe((provinces) => {
      this.provinces = provinces;
    });

    this.createComplexForm.get('province')?.valueChanges.subscribe((selectedProvince) => {
      if (!selectedProvince) {
        this.createComplexForm.get('locality')?.reset();
        this.createComplexForm.get('locality')?.disable();
        return;
      }
      this.createComplexForm.get('locality')?.enable();
      this.createComplexForm.get('locality')?.reset();
      if (selectedProvince.toLowerCase() == 'ciudad autónoma de buenos aires') {
        this.locationService.getCABALocalities().subscribe((localities) => {
          this.localities = localities;
          this.filteredLocalities = localities;
        });
      } else {
        this.locationService
          .getLocalities(selectedProvince)
          .subscribe((localities) => {
            this.localities = localities;
            this.filteredLocalities = localities;
          });
      }
    });

    this.complexServices.getAllServices().subscribe({
      next: (complexServices:ComplexServiceModel[]) => {
      console.log(complexServices);
      this.services = complexServices;
    },
      error: (err) => {
        console.log(err);
      }})

    
    this.createComplexForm.get('timeSlots')?.valueChanges.subscribe(() => {
      this.validateSchedules();
      }
    );
  }

  onFileSelected(event: any) {
    console.log(event);
    const file = event.target.files[0];
    this.selectedImage = file;
    this.imageError = null;

    console.log(this.selectedImage);

    const validTypes = ['image/jpeg', 'image/png', 'image/jpg'];
    if (!validTypes.includes(file.type)) {
      this.imageError = 'Formato de imagen inválido (solo JPG o PNG)';
      return;
    }
    if (file.size > 5*1024*1024) {
      this.imageError = 'La imagen no puede superar 5 MB de tamaño';
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.previewUrl = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  removeImage() {
    this.previewUrl = null;
    this.selectedImage = null;
    this.imageError = null;
  }

  get timeSlotsArray(): FormArray<FormGroup> {
    return this.createComplexForm.get('timeSlots') as FormArray<FormGroup>;
  }

  search(event: AutoCompleteCompleteEvent) {
    const query = event.query.toLowerCase();

    this.filteredLocalities = this.localities.filter((loc) =>
      loc.toLowerCase().includes(query)
    );
  }

  validateLocality() {
    const control = this.createComplexForm.get('locality');
    const value = control?.value;
    control?.markAsTouched();
    // Si no coincide exactamente con ninguna localidad -> borrar
    if (
      !this.localities
        .map((l) => l.toLowerCase())
        .includes(value?.toLowerCase())
    ) {
      this.createComplexForm.get('locality')?.reset('');
    }
  }

  validateSchedules(): void {
    const timeSlots = this.createComplexForm.get('timeSlots')?.value;

    if (!timeSlots) return;

    const hasInvalidSchedule = timeSlots.some((slot: any) => {
      const initIndex = this.availableHours.indexOf(slot.initTime);
      const endIndex = this.availableHours.indexOf(slot.endTime);
      return initIndex > endIndex && endIndex != -1;
    });

    this.invalidSchedulesError = hasInvalidSchedule? "El horario de apertura no puede ser mayor al de cierre" : null;
  }

  onSubmit() {
    const formData = new FormData();
    this.imageError = null;
    this.invalidSchedulesError = null;
    const value = this.createComplexForm.value;

    formData.append('Name', value.name);
    formData.append('Description', value.description);
    formData.append('Province', value.province);
    formData.append('Locality', value.locality);
    formData.append('Street', value.street);
    formData.append('Number', value.number);
    formData.append('Phone', value.phone);
    formData.append('PercentageSign', value.percentageSign);
    formData.append('StartIlumination', value.startIlumination);
    formData.append('AditionalIlumination', value.aditionalIlumination);
    formData.append('CBU', value.cbu);

    if (!this.selectedImage) {
      this.imageError = "La imagen del complejo es obligatoria"
      return;
    }

    this.validateSchedules();

    if (this.invalidSchedulesError) {
      return;
    }

    formData.append('Image', this.selectedImage, this.selectedImage.name);


    value.services.forEach((service:any, index: number) => {
      formData.append(`ServicesIds[${index}]`,service.id.toString())
    })

    value.timeSlots.forEach((slot: any, index: number) => {
      formData.append(`TimeSlots[${index}].WeekDay`, slot.weekDay); 
      formData.append(`TimeSlots[${index}].InitTime`, slot.initTime); 
      formData.append(`TimeSlots[${index}].EndTime`, slot.endTime); 
    })

    this.complexService.createComplex(formData).subscribe({
      next: (response) => {
        console.log("COMPLEJO CREADO EXITOSAMENTE: ", response.body);
        this.messageService.add({
          severity:'success',
          summary:'Complejo creado exitosamente.',
          detail:'Debes esperar a que sea aprobado por un administrador.',
          life: 2000
        })
        this.createComplexForm.reset();
        this.authService.setToken(response.body.token);
        this.removeImage();
        this.router.navigate(["complexes",response.body.complex.id])
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
}
