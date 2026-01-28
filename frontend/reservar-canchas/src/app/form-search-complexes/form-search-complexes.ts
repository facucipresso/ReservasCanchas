import { Component, OnInit } from '@angular/core';
import {FormBuilder,FormGroup,ReactiveFormsModule,Validators,} from '@angular/forms';
import { Location } from '../services/location';
import { Select } from 'primeng/select';
import {AutoCompleteCompleteEvent,AutoCompleteModule,} from 'primeng/autocomplete';
import { FieldType } from '../models/fieldtype.enum';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import {  Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-form-search-complexes',
  imports: [
    ReactiveFormsModule,
    Select,
    AutoCompleteModule,
    DatePickerModule,
    ButtonModule,
    CommonModule,
  ],
  templateUrl: './form-search-complexes.html',
  styleUrl: './form-search-complexes.css',
})
export class FormSearchComplexes implements OnInit {
  provinces!: string[];
  localities!: string[];
  filteredLocalities!: string[];
  dateNow = new Date();
  maxDateValid = new Date();
  form!: FormGroup;
  fieldTypes = [
    { label: 'Fútbol 5', value: FieldType.Futbol5 },
    { label: 'Fútbol 7', value: FieldType.Futbol7 },
    { label: 'Fútbol 11', value: FieldType.Futbol11 }
  ]
  
  allHours = [
    '08:00',
    '09:00',
    '10:00',
    '11:00',
    '12:00',
    '13:00',
    '14:00',
    '15:00',
    '16:00',
    '17:00',
    '18:00',
    '19:00',
    '20:00',
    '21:00',
    '22:00',
    '23:00',
    '00:00',
    '01:00',
  ];

  hours: { label: string; value: string; disabled: boolean }[] = [];

  constructor(
    private fb: FormBuilder,
    private locationService: Location,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.maxDateValid.setDate(this.maxDateValid.getDate() + 7);
    this.form = this.fb.group({
      province: ['', Validators.required],
      locality: [{ value: '', disabled: true }, Validators.required],
      fieldType: ['', Validators.required],
      date: [this.dateNow, Validators.required],
      hour: ['', Validators.required],
    });

    // Inicializar horas con la fecha actual
    this.updateSelectableHours(this.dateNow);

    // Subscribirse a cambios en la fecha
    this.form.get('date')?.valueChanges.subscribe((selectedDate) => {
      this.updateSelectableHours(selectedDate);
      // Resetear hora si no es válida
      const currentHour = this.form.get('hour')?.value;
      if (currentHour) {
        const isDisabled = this.hours.find(h => h.value === currentHour)?.disabled;
        if (isDisabled) {
          this.form.get('hour')?.reset('');
        }
      }
    });

    this.locationService.getProvinces().subscribe((provinces) => {
      this.provinces = provinces;
    });

    this.form.get('province')?.valueChanges.subscribe((selectedProvince) => {
      if (!selectedProvince) {
        this.form.get('locality')?.reset();
        this.form.get('locality')?.disable();
        return;
      }
      this.form.get('locality')?.enable();
      this.form.get('locality')?.reset();
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
  }

  private updateSelectableHours(selectedDate: Date): void {
    const now = new Date();
    const isToday = selectedDate.toDateString() === now.toDateString();
    const currentHour = now.getHours();

    this.hours = this.allHours.map(timeStr => {
      const slotHour = parseInt(timeStr.split(':')[0], 10);
      // Deshabilitar si es hoy y la hora ya pasó (excepto medianoche y 1am)
      const isPastHour = isToday && slotHour <= currentHour && (slotHour != 0 && slotHour != 1);
      
      return {
        label: timeStr,
        value: timeStr,
        disabled: isPastHour
      };
    });
  }

  search(event: AutoCompleteCompleteEvent) {
    const query = event.query.toLowerCase();

    this.filteredLocalities = this.localities.filter((loc) =>
      loc.toLowerCase().includes(query)
    );
  }

  validateLocality() {
    const value = this.form.get('locality')?.value;
    
    // Si no coincide exactamente con ninguna localidad -> borrar
    if (
      !this.localities
        .map((l) => l.toLowerCase())
        .includes(value?.toLowerCase())
    ) {
      this.form.get('locality')?.reset('');
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const { province, locality, fieldType, date, hour } = this.form.value;
    console.log(this.form.value);
    const dateOnly = date.toISOString().substring(0, 10);
    console.log(dateOnly);
    const queryParams = {
      province,
      locality,
      fieldType,
      date: dateOnly,
      hour,
    };

    console.log('QUERYPARAMS',queryParams);

    this.router.navigate(['search/complexes'], { queryParams });
  }
}
