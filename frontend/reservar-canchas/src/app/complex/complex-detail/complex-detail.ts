import { Component, OnInit } from '@angular/core';
import { Complex } from '../../services/complex';
import { ComplexModel } from '../../models/complex.model';
import { ActivatedRoute, Router} from '@angular/router';
import { Toast, ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Auth } from '../../services/auth';
import { ComplexInfo } from '../complex-info/complex-info';
import { ProgressSpinner } from 'primeng/progressspinner';
import { FieldModel } from '../../models/field.model';
import { Field } from '../../services/field';
import { FieldTable } from '../field-table/field-table';
import { DatePickerModule } from 'primeng/datepicker';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Complexservices } from '../../services/complexservices';
import { ComplexServiceModel } from '../../models/complexservice.model';
import { Dialog } from 'primeng/dialog';
import { EditcomplexForm } from '../editcomplex-form/editcomplex-form';
import { Fieldform } from '../fieldform/fieldform';
import { ConfirmDialog } from 'primeng/confirmdialog';
type Mode = 'create' | 'edit';
@Component({
  selector: 'app-complex-detail',
  imports: [ToastModule,ComplexInfo,ProgressSpinner,DatePickerModule,FormsModule,
     CommonModule, Dialog, EditcomplexForm, Fieldform, ConfirmDialog],
  templateUrl: './complex-detail.html',
  styleUrl: './complex-detail.css',
  providers:[ConfirmationService]
})
export class ComplexDetail implements OnInit{

  complex!: ComplexModel;
  isAdmin!: boolean;
  complexId!: number;
  errorComplex =  false;
  selectedDate!: Date;
  dateNow = new Date();
  maxDateValid = new Date();
  fields!: FieldModel[];
  allServices !:ComplexServiceModel[];

  visibleEditComplexModal = false;
  visibleFieldFormModal = false;

  fieldFormMode:Mode = 'create';
  selectedField?: FieldModel;
  constructor(private complexService:Complex, private route:ActivatedRoute, private messageService:MessageService,
     private authService: Auth, private router:Router, private fieldService:Field, 
     private servicesComplex:Complexservices, private confirmationService: ConfirmationService){}

  ngOnInit(){

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

      console.log('Selected date: ', this.selectedDate)
    });
  }

  loadComplex(complexId:number){
    this.complexService.getComplexById(complexId).subscribe({
      next: (complex) => {
        this.complex = complex;
        console.log(this.complex);
        this.isAdmin = this.complex.userId === parseInt(this.authService.getUserId());
        this.loadFields(this.complexId);
        this.loadServices();
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
        this.errorComplex = true;
        setTimeout(()=> this.router.navigate(['/']),1500);
      }
    })
  }

  loadFields(complexId:number){
    this.fieldService.getFieldsByComplexId(complexId).subscribe({
      next: (fields) => {
        this.fields = fields;
        console.log(fields);
      }
    })
  }

  loadServices(){
    this.servicesComplex.getAllServices().subscribe({
      next: (services) => {
        this.allServices = services;
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

  onDateChange(date:Date){
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { date: date.toISOString().substring(0, 10) },
      queryParamsHandling: 'merge'
    });
  }

  onEditField(field:FieldModel){
    this.fieldFormMode = 'edit';
    this.selectedField = field;
    this.visibleFieldFormModal = true;
  }

  onAddField() {
    this.fieldFormMode = 'create';
    this.selectedField = undefined;
    this.visibleFieldFormModal = true;
  }


  updateBasicInfo(basicInfo: any) {
    console.log('Data: ' ,basicInfo);
    this.complexService.updateComplexBasicInfo(basicInfo, this.complex.id).subscribe({
      next: response => {
        console.log("INFORMACION BASICA DEL COMPLEJO ACTUALIZADA EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Complejo actualizado exitosamente.',
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
    });
  }

  updateTimeSlots(data: any) {
    this.complexService.updateComplexTimeSlots(data, this.complex.id).subscribe({
      next: (response) => {
        console.log("HORARIOS DEL COMPLEJO ACTUALIZADOS EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Horarios del complejo actualizado exitosamente.',
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
    });
  }

  updateServices(serviceIds: number[]) {
    this.complexService.updateComplexServices(
      { servicesIds: serviceIds },
      this.complex.id).subscribe({
        next: response => {
          console.log("SERVICIOS DEL COMPLEJO ACTUALIZADOS EXITOSAMENTE: ", response);
          this.messageService.add({
            severity:'success',
            summary:'Servicios del complejo actualizados exitosamente.',
            life: 2000
          })        
          this.loadComplex(this.complexId);
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
      });
  }

  changeComplexState(){
    const newState = this.complex.state === 'Habilitado' ? 'Deshabilitado' : 'Habilitado';
    const payload = { state: newState };
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
      // Si el usuario hace clic en "Aceptar", ejecutamos el borrado
        this.deleteComplex();
      },
      reject: () => {
      // Opcional: acción al cancelar
        console.log('Eliminación cancelada');
      }
    });
  }

  deleteComplex(){
    this.complexService.deleteComplex(this.complex.id).subscribe({
      next: response => {
        this.router.navigate(['/admin/complexes']).then(() => {
        // Un pequeño delay de 100ms ayuda a que el nuevo componente 
        // esté totalmente inicializado antes de disparar el mensaje
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

  createField(field:any){
    this.fieldService.createField(field).subscribe({
      next: (response) => {
        console.log("CANCHA CREADO EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Cancha creada exitosamente.',
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

  updateBasicInfoField(basicInfo:any){
    this.fieldService.updateBasicInfoField(basicInfo, this.selectedField!.id).subscribe({
      next: response => {
        console.log("INFORMACION BASICA DE LA CANCHA ACTUALIZADA EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Cancha actualizada exitosamente.',
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
    });
  }

  updateTimeSlotsField(timeSlots:any){
    const body = {
      timeSlotsField: timeSlots
    }
    this.fieldService.updateTimeSlotsField(body, this.selectedField!.id).subscribe({
      next: (response) => {
        console.log("HORARIOS DE LA CANCHA ACTUALIZADOS EXITOSAMENTE: ", response);
        this.messageService.add({
          severity:'success',
          summary:'Horarios del complejo actualizado exitosamente.',
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
      // Si el usuario hace clic en "Aceptar", ejecutamos el borrado
        this.deleteField(fieldId);
      },
      reject: () => {
      // Opcional: acción al cancelar
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

  updateStateField(state:string){
    const payload = {
      fieldState:state
    }
    this.fieldService.updateStateField(payload,this.selectedField!.id).subscribe({
      next: (response) => {
        console.log(`LA CANCHA CON ID ${this.selectedField?.id} FUE MODIFICADA EN SU ESTADO EXITOSAMENTE`, response);
        this.messageService.add({
          severity:'success',
          summary:'El estado de la cancha ha sido actualizado exitosamente.',
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
}
