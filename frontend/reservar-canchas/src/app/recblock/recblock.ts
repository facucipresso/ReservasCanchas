import { Component, EventEmitter, Input,OnInit, Output} from '@angular/core';
import { FieldDetailModel } from '../models/field/field.model';
import { ButtonModule } from 'primeng/button';
import { RecBlockResponseModel } from '../models/recblock/recblockresponse.model';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { WeekDay } from '../models/complex/weekday.enum';
import { CommonModule } from '@angular/common';
import { TextareaModule } from 'primeng/textarea';
import { Field } from '../services/field';
import { RecBlockRequestModel } from '../models/recblock/reqblockrequest.model';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialog } from 'primeng/confirmdialog';

@Component({
  selector: 'app-recblock',
  imports: [ButtonModule, TableModule, DialogModule,
    SelectModule, ReactiveFormsModule, CommonModule, TextareaModule, ToastModule, ConfirmDialog],
  templateUrl: './recblock.html',
  styleUrl: './recblock.css',
  providers: [ConfirmationService]
})
export class Recblock implements OnInit{
  @Input() field !: FieldDetailModel;
  @Output() blocksUpdated = new EventEmitter<void>();
  blocks!: RecBlockResponseModel[];
  visibleAddBlockModal = false;
  blockForm!: FormGroup;
  
  daysOfWeek = Object.values(WeekDay);
  availableHours: string[] = [];
  invalidHoursError: string | null = null;
  
  constructor(private fb: FormBuilder, private fieldService: Field,
     private messageService: MessageService, private confirmationService:ConfirmationService) {}

  ngOnInit(): void {
    this.blockForm = this.fb.group({
      day: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
      reason: ['', Validators.required]
    });
    this.blocks = this.field.recurringCourtBlocks;
    
    this.blockForm.get('day')?.valueChanges.subscribe((day) => {
      this.updateAvailableHours(day);
      this.blockForm.patchValue({ startTime: '', endTime: '' });
    });

    this.blockForm.get('startTime')?.valueChanges.subscribe(() => {
      this.invalidHoursError = null;
    });

    this.blockForm.get('endTime')?.valueChanges.subscribe(() => {
      this.invalidHoursError = null;
    });
  }

  private updateAvailableHours(day: string) {
    const timeSlot = this.field.timeSlotsField.find(ts => ts.weekDay === day);
    
    if (timeSlot) {
      this.availableHours = this.generateHourRange(timeSlot.startTime, timeSlot.endTime);
    } else {
      this.availableHours = [];
    }
  }

  private generateHourRange(start: string, end: string): string[] {
    if (!start || !end) return [];

    const hours: string[] = [];
    let startH = Number(start.split(':')[0]);
    let endH = Number(end.split(':')[0]);

    if (startH === endH) return [];

    if (endH > startH) {
      for (let h = startH; h < endH; h++) {
        hours.push(h.toString().padStart(2, '0') + ':00');
      }
      return hours;
    }

    for (let h = startH; h < 24; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    for (let h = 0; h <= endH; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    return hours;
  }

  onAddBlock() {
    const startH = parseInt(this.blockForm.value.startTime.split(':')[0]);
    const endH = parseInt(this.blockForm.value.endTime.split(':')[0]);

    const duration = (endH - startH + 24) % 24;
    console.log(startH, endH, duration);
    if (duration === 0) {
      this.invalidHoursError = "La hora de inicio y fin no pueden ser iguales";
      return;
    }

    if (duration > 19) { 
      this.invalidHoursError = "El rango seleccionado es demasiado extenso";
      return;
    }
    this.invalidHoursError = null;
    if (this.blockForm.valid) {
      const newBlock: RecBlockRequestModel = {
        weekDay: this.blockForm.value.day,
        startTime: this.blockForm.value.startTime,
        endTime: this.blockForm.value.endTime,
        reason: this.blockForm.value.reason
      };
      this.fieldService.addRecurringBlock(this.field.id, newBlock).subscribe({
        next: (response) => {
          this.blocks = response.recurringCourtBlocks;
          this.field.recurringCourtBlocks = response.recurringCourtBlocks; 
          this.visibleAddBlockModal = false;
          this.blockForm.reset();
          this.messageService.add({
            severity:'success', 
            summary:'Éxito', 
            detail:'Bloqueo recurrente agregado correctamente',
            life:2000
          });
          this.blocksUpdated.emit();
        },
        error: (err) => {
          this.showBackendError(err);
        }
      });
    }
  }

  onCancelAddBlock() {
    this.visibleAddBlockModal = false;
    this.blockForm.reset();
  }

  confirmDeleteBlock(blockId: number) {
    this.confirmationService.confirm({
      message: '¿Estás seguro de que deseas eliminar este bloqueo recurrente? Esta acción no se puede deshacer.',
      header: 'Confirmar Eliminación',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      acceptLabel: 'Eliminar',
      rejectLabel: 'Cancelar',
      accept: () => {
        this.onDeleteBlock(blockId);
      },
      reject: () => {
        console.log('Eliminación cancelada');
      }
    });
  }

  onDeleteBlock(blockId: number) {
    this.fieldService.deleteRecurringBlock(this.field.id, blockId).subscribe({
      next: (response) => {
        this.blocks = response.recurringCourtBlocks;
        this.field.recurringCourtBlocks = response.recurringCourtBlocks; 
        this.messageService.add({
          severity:'success', 
          summary:'Éxito', 
          detail:'Bloqueo recurrente eliminado correctamente',
          life:2000
        });
        this.blocksUpdated.emit();
      },
      error: (err) => {
        this.showBackendError(err);
      }
    });
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