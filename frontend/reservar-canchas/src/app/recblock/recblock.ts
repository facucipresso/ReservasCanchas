import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FieldModel } from '../models/field.model';
import { ButtonModule } from 'primeng/button';
import { RecBlockResponseModel } from '../models/recblockresponse.model';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { WeekDay } from '../models/weekday.enum';
import { CommonModule } from '@angular/common';
import { TextareaModule } from 'primeng/textarea';
import { Field } from '../services/field';
import { RecBlockRequestModel } from '../models/reqblockrequest.model';
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
  @Input() field !: FieldModel;
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
    console.log('Cancha recibida en recblock:', this.field);
    this.blocks = this.field.recurringCourtBlocks;
    console.log('Bloques recurrentes:', this.blocks);
    
    // Suscribirse a cambios en el día para actualizar las horas disponibles
    this.blockForm.get('day')?.valueChanges.subscribe((day) => {
      this.updateAvailableHours(day);
      // Limpiar selecciones de hora cuando cambia el día
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
      this.availableHours = this.generateHourRange(timeSlot.initTime, timeSlot.endTime);
    } else {
      this.availableHours = [];
    }

    console.log('Horas disponibles para', day, ':', this.availableHours);
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

    // Cruza medianoche
    for (let h = startH; h < 24; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    for (let h = 0; h < endH; h++) {
      hours.push(h.toString().padStart(2, '0') + ':00');
    }

    return hours;
  }

  onAddBlock() {
    const initTime = parseInt(this.blockForm.value.startTime.split(':')[0]);
    const endTime = parseInt(this.blockForm.value.endTime.split(':')[0]);

    if(endTime <= initTime || (initTime === 23 && endTime === 0) || (initTime === 1 && endTime === 0)) {
      this.invalidHoursError = "La hora de fin debe ser mayor que la hora de inicio";
      return;
    }
    this.invalidHoursError = null;
    if (this.blockForm.valid) {
      const newBlock: RecBlockRequestModel = {
        weekDay: this.blockForm.value.day,
        initHour: this.blockForm.value.startTime,
        endHour: this.blockForm.value.endTime,
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
          console.error('Error al crear bloqueo:', err);
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
        console.error('Error al eliminar bloqueo:', err);
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
}