import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-reservation-reason-dialog',
  standalone: true,
  imports: [    CommonModule, DialogModule, ButtonModule, InputTextModule, FormsModule],
  templateUrl: './reservation-reason-dialog.html',
  styleUrl: './reservation-reason-dialog.css',
})
export class ReservationReasonDialog {

  
  @Input() visible = false;
  @Input() title = 'Indique la razón';
  @Input() confirmLabel = 'Confirmar';

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() confirm = new EventEmitter<string>();
  @Output() cancel = new EventEmitter<void>();

  reason = '';

  onCancel() {
    this.reason = '';
    this.visibleChange.emit(false);
    this.cancel.emit();
  }

  onConfirm() {
    if (!this.reason.trim()) return;

    this.confirm.emit(this.reason.trim());
    this.reason = '';
    this.visibleChange.emit(false);
  }

}
