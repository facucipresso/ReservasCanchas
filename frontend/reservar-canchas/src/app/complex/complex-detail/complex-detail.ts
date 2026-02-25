import { Component, OnInit } from '@angular/core';
import { Complex } from '../../services/complex';
import { ComplexDetailModel } from '../../models/complex/complexdetail.model';
import { ActivatedRoute, Router} from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Auth } from '../../services/auth';
import { ComplexInfo } from '../complex-info/complex-info';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FieldDetailModel } from '../../models/field/field.model';
import { Field } from '../../services/field';
import { DatePickerModule } from 'primeng/datepicker';
import { FormsModule } from '@angular/forms';
import { CommonModule} from '@angular/common';
import { Complexservices } from '../../services/complexservices';
import { ComplexServiceModel } from '../../models/complex/complexservice.model';
import { Dialog } from 'primeng/dialog';
import { EditcomplexForm } from '../editcomplex-form/editcomplex-form';
import { Fieldform } from '../../field/fieldform/fieldform';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { Reservation } from '../../services/reservation';
import { ReservationProcessRequest } from '../../models/reservation/reservationprocessrequest.model';
import { Recblock } from '../../recblock/recblock';
import { Review } from '../../services/review';
import { ReviewResponse } from '../../models/reservation/reviewresponse.model';
import { ComplexState } from '../../models/complex/complexstate.enum';
import { ReservationType } from '../../models/reservation/reservationtype.enum';
import { Location } from '@angular/common';


type Mode = 'create' | 'edit';
@Component({
  selector: 'app-complex-detail',
  imports: [ToastModule,ComplexInfo,ProgressSpinnerModule,DatePickerModule,FormsModule,
     CommonModule, Dialog, EditcomplexForm, Fieldform, ConfirmDialog, Recblock],
  templateUrl: './complex-detail.html',
  styleUrl: './complex-detail.css',
  providers:[ConfirmationService]
})
export class ComplexDetail implements OnInit{

  complex!: ComplexDetailModel;
  isAdmin!: boolean;
  complexId!: number;
  selectedDate!: Date;
  dateNow = new Date();
  maxDateValid = new Date();
  fields!: FieldDetailModel[];
  allServices !:ComplexServiceModel[];
  isLoading: boolean = false;

  visibleEditComplexModal = false;
  visibleFieldFormModal = false;
  visibleRecBlockModal = false;

  fieldFormMode:Mode = 'create';
  selectedField!: FieldDetailModel;

  reviews!: ReviewResponse[];

  constructor(private complexService:Complex, private route:ActivatedRoute, private messageService:MessageService,
     private authService: Auth, private router:Router, private fieldService:Field, 
     private servicesComplex:Complexservices, private confirmationService: ConfirmationService,
     private reservationService:Reservation, private reviewService:Review, private location: Location){}

  ngOnInit(){
    this.isLoading = true;
    this.complexId=Number(this.route.snapshot.paramMap.get('id'));
    this.maxDateValid.setDate(this.maxDateValid.getDate() + 7);
    this.dateNow.setHours(0, 0, 0, 0);

    this.loadComplex(this.complexId);

    this.route.queryParams.subscribe(params => {
      const dateParam = params['date'];
      if(dateParam){
        const parsedDate = new Date(dateParam + 'T00:00:00');
        console.log("Fecha parseada:", parsedDate);
        this.selectedDate = this.isDateValid(parsedDate) ? parsedDate : this.dateNow;
      }else{
        this.selectedDate = this.dateNow;
      }
    });
    this.reviewService.getReviewsByComplexId(this.complexId).subscribe({
      next: (reviews) => {
        this.reviews = reviews;
      },
      error: (err) => {
        this.reviews = [];
        this.showBackendError(err);
      }
    })
  }

  loadComplex(complexId:number){
    this.complexService.getComplexById(complexId).subscribe({
      next: (complex) => {
        this.complex = complex;
        this.isAdmin = this.complex.userId === parseInt(this.authService.getUserId());
        this.loadFields(this.complexId);
        this.loadServices();
        this.isLoading = false;
      },
      error: (err) => {
        this.showBackendError(err);
        this.router.navigate(['/']);
      }
    })
  }

  loadFields(complexId:number){
    this.fieldService.getFieldsByComplexId(complexId).subscribe({
      next: (fields) => {
        this.fields = fields;
        console.log('CANCHAS: ', fields);
      },
      error: (err) => {
        this.fields = [];
        this.showBackendError(err);
      }
    })
  }

  loadServices(){
    this.servicesComplex.getAllServices().subscribe({
      next: (services) => {
        this.allServices = services;
      },
      error: (err) => {
        this.allServices = [];
        this.showBackendError(err);
      }
    })
  }

  private getTodayDate(): string {
    return new Date().toISOString().substring(0, 10);
  }

  private isDateValid(date: Date): boolean {
    const today = new Date();
    today.setHours(0,0,0,0);

    const maxDate = new Date(today);
    maxDate.setDate(today.getDate() + 7);

    return date >= today && date <= maxDate;
  }

  showDialogComplex() {
    this.visibleEditComplexModal = true;
  }

  onDateChange(date: Date){
    this.selectedDate = date;
    const newUrl = this.router.createUrlTree([], {
      relativeTo: this.route,
      queryParams: { date: date.toISOString().substring(0, 10) },
      queryParamsHandling: 'merge'
    }).toString();
    this.location.replaceState(newUrl);
  }

  onEditField(field:FieldDetailModel){
    this.fieldFormMode = 'edit';
    this.selectedField = field;
    this.visibleFieldFormModal = true;
  }

  onAddField() {
    this.fieldFormMode = 'create';
    this.selectedField = undefined!;
    this.visibleFieldFormModal = true;
  }

  updateComplexBasicInfo(basicInfo: any) {
    console.log('Data: ' ,basicInfo);
    this.complexService.updateComplexBasicInfo(basicInfo, this.complex.id).subscribe({
      next: response => {
        this.messageService.add({
          severity:'success',
          summary:'Complejo actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
  }

  updateComplexTimeSlots(data: any) {
    this.complexService.updateComplexTimeSlots(data, this.complex.id).subscribe({
      next: (response) => {
        this.messageService.add({
          severity:'success',
          summary:'Horarios del complejo actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
  }

  updateComplexServices(serviceIds: number[]) {
    this.complexService.updateComplexServices({ servicesIds: serviceIds },this.complex.id).subscribe({
      next: response => {
        this.messageService.add({
          severity:'success',
          summary:'Servicios del complejo actualizados exitosamente.',
          life: 2000
        })        
        this.loadComplex(this.complexId);
      },
      error: err => {
        this.showBackendError(err);   
      } 
    });
  }

  updateComplexImage(formImage: FormData){
    this.complexService.updateComplexImage(formImage,this.complex.id).subscribe({
      next: response => {
        this.messageService.add({
          severity:'success',
          summary:'Imagen del complejo actualizada exitosamente.',
          life: 2000
        })        
        this.loadComplex(this.complexId);
      },
      error: err => {
        this.showBackendError(err);       
      } 
    });
  }

  changeComplexState(){
    let newState;
    if(this.complex.complexState === ComplexState.Habilitado){
      newState = ComplexState.Deshabilitado;
    }else if(this.complex.complexState === ComplexState.Deshabilitado){
      newState = ComplexState.Habilitado;
    }else{
      newState = ComplexState.Pendiente;
    }
    
    const payload = { complexState: newState };
    console.log('Nuevo estado: ',payload);
    this.complexService.updateComplexState(payload, this.complex.id).subscribe({
      next: response => {
        console.log("ESTADO DEL COMPLEJO ACTUALIZADO EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'El estado del complejo ha sido actualizado exitosamente.',
          life: 2000
        })        
        this.loadComplex(this.complexId);
      },
      error: err => {
        this.showBackendError(err);
      }
    }); 
  }

  confirmDeleteComplex(){
    this.confirmationService.confirm({
      message: '¿Estás seguro de que deseas eliminar el complejo? Esta acción no se puede deshacer.',
      header: 'Confirmar Eliminación',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      acceptLabel: 'Eliminar',
      rejectLabel: 'Cancelar',
      accept: () => {
        this.deleteComplex();
      },
      reject: () => {
        console.log('Eliminación cancelada');
      }
    });
  }

  deleteComplex(){
    this.complexService.deleteComplex(this.complex.id).subscribe({
      next: response => {
        this.router.navigate(['/admin/complexes']).then(() => {
          setTimeout(() => {
            this.messageService.add({
              severity: 'success',
              summary: 'Éxito',
              detail: 'El complejo ha sido eliminado correctamente.',
              life: 2000
            });
          }, 100);
        });
      },
      error: err => {
        this.showBackendError(err);
      }
    })
  }

  createField(field:any){
    this.fieldService.createField(field).subscribe({
      next: (response) => {
        this.messageService.add({
          severity:'success',
          summary:'Cancha creada exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    })
  }

  updateFieldBasicInfo(basicInfo:any){
    this.fieldService.updateBasicInfoField(basicInfo, this.selectedField!.id).subscribe({
      next: response => {
        this.messageService.add({
          severity:'success',
          summary:'Cancha actualizada exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
  }

  updateFieldTimeSlots(timeSlots:any){
    const body = {
      timeSlotsField: timeSlots
    }
    this.fieldService.updateTimeSlotsField(body, this.selectedField!.id).subscribe({
      next: (response) => {
        this.messageService.add({
          severity:'success',
          summary:'Horarios del complejo actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
  }

  confirmDeleteField(fieldId: number) {
    this.confirmationService.confirm({
      message: '¿Estás seguro de que deseas eliminar esta cancha? Esta acción no se puede deshacer.',
      header: 'Confirmar Eliminación',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      acceptLabel: 'Eliminar',
      rejectLabel: 'Cancelar',
      accept: () => {
        this.deleteField(fieldId);
      },
      reject: () => {
        console.log('Eliminación cancelada');
      }
    });
  }

  deleteField(fieldId:number){
    console.log(fieldId);
    this.fieldService.deleteField(fieldId).subscribe({
      next: (response) => {
        console.log(`LA CANCHA CON ID ${fieldId} FUE ELIMINADA EXITOSAMENTE`, response);
        this.messageService.add({
          severity:'success',
          summary:'Cancha eliminada exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
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

  updateFieldState(state:string){
    const payload = {
      fieldState:state
    }
    this.fieldService.updateStateField(payload,this.selectedField!.id).subscribe({
      next: (response) => {
        this.messageService.add({
          severity:'success',
          summary:'El estado de la cancha ha sido actualizado exitosamente.',
          life: 2000
        })
        this.loadComplex(this.complexId);
      },
      error: (err) => {
        this.showBackendError(err);
      }
    })
  }

  onReserveField(event:{field:FieldDetailModel, hour:string}){
    if(!this.authService.isLoggedIn()){
      this.messageService.add({
        severity:'warn',
        summary:'Debes iniciar sesión para reservar una cancha.',
        life: 2000
      })
      return;
    }
    const reservationData:ReservationProcessRequest = {
      complexId: this.complex.id,
      fieldId: event.field.id,
      date: this.selectedDate.toISOString().substring(0,10),
      startTime: event.hour
    }

    this.reservationService.createProcessReservation(reservationData).subscribe({
      next: (response) => {
        if(response.existReservationProcessForUser){
          this.confirmationService.confirm({
            message: 'Ya tienes una reserva en proceso. Debes completarla o cancelarla antes de realizar otra.',
            header: 'Reserva en curso',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Ir al Checkout',
            rejectLabel: 'Cerrar',
            accept: () => {
              this.router.navigate(['reservation/checkout', response.reservationProcessId]);
            },
        });
        }else{
          this.router.navigate(['reservation/checkout', response.reservationProcessId]);
        }
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
  }

  onRecurringBlockField(field: FieldDetailModel){
    this.selectedField = field;
    this.visibleRecBlockModal = true;
  }

  onSpecificBlockField(event:{field:FieldDetailModel,hour:string,reason:string}){

    const formData = new FormData();
    formData.append('fieldId', event.field.id.toString());
    formData.append('date', this.selectedDate.toISOString().substring(0,10));
    formData.append('startTime', event.hour);
    formData.append('reservationType',  ReservationType.Bloqueo.toString());
    formData.append('blockReason', event.reason);
    this.reservationService.createReservation(formData).subscribe({
      next: (data)=>{
        console.log(data);
        this.messageService.add({
          severity:'success',
          summary:'Bloqueo creado con éxito.',
          life: 3000
        })
        this.router.navigate([`/admin/complexes/${this.complexId}/reservations`]);
      },
      error: (error)=>{
        this.showBackendError(error);
      }
    })
  }

  private showBackendError(err: any, life = 2000): void {
    const backendError = err?.error;
    this.messageService.add({
      severity: 'error',
      summary: backendError?.title || 'Error',
      detail: backendError?.detail || 'Error desconocido',
      life
    });
  }
}
