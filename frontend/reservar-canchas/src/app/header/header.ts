import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { AvatarModule } from 'primeng/avatar';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { Dialog } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { PasswordModule } from 'primeng/password';

@Component({
  selector: 'app-header',
  imports: [
    ButtonModule,
    AvatarModule,
    OverlayBadgeModule,
    Dialog,
    InputTextModule,
    PasswordModule,
    ReactiveFormsModule,
  ],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {
  isLoggedIn = false; // reemplazar con tu auth real
  visible: boolean = false;
  value!: string;
  loginForm!: FormGroup;

  constructor(public router: Router, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  showDialog() {
    this.visible = true;
  }

  onDialogClose() {
    this.loginForm.reset();
  }

  onSubmit() {}

  goToRegistrationForm() {
    this.router.navigate(['/register']);
    this.visible = false;
  }
}
