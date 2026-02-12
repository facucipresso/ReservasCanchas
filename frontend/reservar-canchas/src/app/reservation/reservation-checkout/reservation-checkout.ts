import { Component, OnDestroy, OnInit } from '@angular/core';
import { ComplexModel } from '../../models/complex.model';
import { FieldModel } from '../../models/field.model';
import { CheckoutInfo } from '../../models/reservation/checkoutinfo.model';
import { UserInfoModel } from '../../models/user/userinfo.model';
import { Reservation } from '../../services/reservation';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FileUploadModule } from 'primeng/fileupload';
import { Complex } from '../../services/complex';
import { Field } from '../../services/field';
import { User } from '../../services/user';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { forkJoin, interval, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ReservationType } from '../../models/reservation/reservationtype.enum';
import { PayType } from '../../models/reservation/paytype.enum';

@Component({
  selector: 'app-reservation-checkout',
  imports: [FileUploadModule, ProgressSpinnerModule, CommonModule, ButtonModule, FormsModule, RadioButtonModule],
  templateUrl: './reservation-checkout.html',
  styleUrl: './reservation-checkout.css',
})
export class ReservationCheckout implements OnInit, OnDestroy {
  complex!:ComplexModel;
  field!:FieldModel;
  checkoutInfo!:CheckoutInfo;
  checkoutId:string | null = null;
  user!:UserInfoModel;
  isLoading: boolean = false;
  remainingTimeDisplay: string = "--:--";
  timerSubscription: Subscription | null = null;
  paymentOption: 'total' | 'partial' = 'total';
  selectedImage: File | null = null;
  imageErrorMessage: string | null = null;
  constructor(private reservationService: Reservation, private route: ActivatedRoute, 
    private messageService: MessageService, private router: Router, private complexService:Complex,
    private fieldService:Field, private userService:User) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.checkoutId = this.route.snapshot.paramMap.get('id');
    if(this.checkoutId){
      this.reservationService.getCheckoutInfo(this.checkoutId).subscribe({
        next: (data)=>{
          this.checkoutInfo = data;
          console.log(this.checkoutInfo);
          this.loadData();
        },
        error: (err)=>{
          const backendError = err?.error;
          const message = backendError?.detail || 'Error desconocido';

          this.messageService.add({
            severity:'error',
            summary:backendError?.title || 'Error',
            detail: message,
            life: 2000
          })
          this.router.navigate(['/']);
        }
      });
    }else{
      this.router.navigate(['/']);
    }
  }

  ngOnDestroy(): void {
    if(this.timerSubscription){
      this.timerSubscription.unsubscribe();
    }
  }

  loadData(){
    const complex$ = this.complexService.getComplexById(this.checkoutInfo.complexId);
    const field$ = this.fieldService.getFieldById(this.checkoutInfo.fieldId);
    const user$ = this.userService.getUserInfoById(this.checkoutInfo.userId);

    forkJoin({
      complexResult: complex$,
      fieldResult: field$,
      userResult: user$
    }).subscribe({
      next: (response) => {
        this.complex = response.complexResult;
        this.field = response.fieldResult;
        this.user = response.userResult;

        this.startCountdown();
        this.isLoading = false;
        console.log(response.userResult)
      },
      error: (err) => {
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';
        
        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 3000
        })

        this.isLoading = false;
        this.router.navigate(['/']);
      }
    });
  }

  startCountdown() {

    const endTime = new Date(this.checkoutInfo.expirationTime).getTime();
    console.log('End Time (ms):', endTime);
    this.timerSubscription = interval(1000).subscribe(() => {
      const now = new Date().getTime();
      const distance = endTime - now;

      if (distance < 0) {
        this.handleExpiration();
      }else{
        this.remainingTimeDisplay = this.formatTime(distance);
      }
    });
  }

  formatTime(ms: number): string {
    const totalSeconds = Math.floor(ms / 1000);
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = totalSeconds % 60;

    // Rellenar con ceros (ej: 5 -> 05)
    const minStr = minutes < 10 ? '0' + minutes : minutes;
    const secStr = seconds < 10 ? '0' + seconds : seconds;

    return `${minStr}:${secStr}`;
  }

  handleExpiration() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }

    this.remainingTimeDisplay = "00:00";

    this.messageService.add({
      severity: 'warn',
      summary: 'Tiempo agotado',
      detail: 'El tiempo de reserva ha expirado. Redirigiendo al complejo...',
      life: 3000
    });

    setTimeout(() => {
        this.router.navigate(['/complexes', this.complex.id]);
    }, 2000);
  }

  get basePrice(): number {
    return this.field.hourPrice;
  }

  
  get illuminationCost(): number {
    if (!this.checkoutInfo.ilumination){
      return 0;
    }
    const percentage = this.complex.aditionalIlumination;
    return this.basePrice * (percentage / 100);
  }

  get totalAmount(): number {
    return this.basePrice + this.illuminationCost;
  }

  get partialAmount(): number {
    const percentage = this.complex.percentageSign; 
    return this.totalAmount * (percentage / 100);
  }

  get finalPriceToPay(): number {
    return this.paymentOption === 'total' ? this.totalAmount : this.partialAmount;
  }

  onFileSelect(event:any){
    this.selectedImage = event.files[0];
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
  }

  clearFile(){
    this.selectedImage = null;
  }

  onConfirm(){
    if(!this.selectedImage){
      this.imageErrorMessage = 'Debe seleccionar una imagen antes de continuar.';
      return;
    }
    const formData = new FormData();
    formData.append('processId', this.checkoutId!);
    formData.append('fieldId', this.checkoutInfo.fieldId.toString());
    formData.append('date', this.checkoutInfo.date);
    formData.append('initTime', this.checkoutInfo.initTime);
    formData.append('pricePaid', this.finalPriceToPay.toString());
    formData.append('reservationType',  ReservationType.Partido.toString());
    formData.append('payType', this.paymentOption === 'total' ? PayType.PagoTotal.toString() : PayType.PagoParcial.toString());
    formData.append('image', this.selectedImage!);
    this.reservationService.createReservation(formData).subscribe({
      next: (data)=>{
        console.log(data);
        this.messageService.add({
          severity:'success',
          summary:'Reserva creada con exito.',
          life: 3000
        })//deberiamos redirigir a mis reservas
        this.router.navigate(['/reservations']);
      },
      error: (error)=>{
        const backendError = error?.error;
        const message = backendError?.detail || 'Error desconocido';
        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 3000
        })
        this.router.navigate(['/']);
      }
    });
  }

  onCancel(){
    this.reservationService.deleteProcessReservation(this.checkoutId!).subscribe( () =>{
      this.messageService.add({
        severity:'info',
        summary:'Proceso de reserva cancelado con éxito.',
        life: 2000
      });
        this.router.navigate(['/complexes', this.complex.id]);
    });
  }
}
