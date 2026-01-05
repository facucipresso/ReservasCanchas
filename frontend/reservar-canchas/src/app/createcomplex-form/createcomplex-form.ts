import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AutoComplete, AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { Button } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputMask } from 'primeng/inputmask';
import { InputNumber, InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { Select, SelectModule } from 'primeng/select';
import { TextareaModule } from 'primeng/textarea';
import { Location } from '../services/location';
import { Complexservices } from '../services/complexservices';
import { ComplexServiceModel } from '../models/complexservice.model';
import { FileUpload } from 'primeng/fileupload';

@Component({
  selector: 'app-createcomplex-form',
  imports: [SelectModule,AutoComplete,CommonModule,AutoCompleteModule, TextareaModule, FileUpload,
    InputTextModule,Button,InputMask,InputNumber,CheckboxModule,InputNumberModule,ReactiveFormsModule],
  templateUrl: './createcomplex-form.html',
  styleUrl: './createcomplex-form.css',
})
export class CreatecomplexForm {
  createComplexForm!: FormGroup;

  provinces!: string[];
  localities!: string[];
  filteredLocalities!: string[];

  services!:ComplexServiceModel[];

  selectedImage: File | null = null;
previewUrl: string | null = null;

onFileSelected(event: any) {
  const file = event.files[0];
  this.selectedImage = file;

  const reader = new FileReader();
  reader.onload = () => {
    this.previewUrl = reader.result as string;
  };
  reader.readAsDataURL(file);
}

onClear() {
  this.selectedImage = null;
  this.previewUrl = null;
}

  days = [
    'Lunes',
    'Martes',
    'Miercoles',
    'Jueves',
    'Viernes',
    'Sabado',
    'Domingo'
  ];


  constructor(private fb: FormBuilder, private locationService: Location, private complexService: Complexservices) {}

  ngOnInit(): void {
    this.createComplexForm = this.fb.group({
      name: [''],
      description: [''],
      province: ['',Validators.required],
      locality: [{ value: '', disabled: true }, Validators.required],
      street: [''],
      number: [''],
      phone: [''],
      percentageSign: [0],
      startIlumination: [''],
      aditionalIlumination: [0],
      cbu: [''],
      services: [[]],

      timeSlots: this.fb.array(
        this.days.map((day, index) =>
          this.fb.group({
            weekDay: [index],
            initTime: [''],   
            endTime: [''],
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

    this.complexService.getAllServices().subscribe({
      next: (complexServices:ComplexServiceModel[]) => {
      console.log(complexServices);
      this.services = complexServices;
    },
      error: (err) => {
        console.log(err);
      }})

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
    const value = this.createComplexForm.get('locality')?.value;

    // Si no coincide exactamente con ninguna localidad -> borrar
    if (
      !this.localities
        .map((l) => l.toLowerCase())
        .includes(value?.toLowerCase())
    ) {
      this.createComplexForm.get('locality')?.reset('');
    }
  }



  onSubmit() {
    const formData = new FormData();

    formData.append('basicInfo', JSON.stringify(this.createComplexForm.value));

    if (this.selectedImage) {
      formData.append('image', this.selectedImage);
    }

    console.log('Enviando:', this.createComplexForm.value);
  }
}
