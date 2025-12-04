import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Fluid } from 'primeng/fluid';
import { InputMask } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { Password, PasswordModule } from 'primeng/password';

@Component({
  selector: 'app-registration-form',
  imports: [CommonModule, Password, PasswordModule, ReactiveFormsModule, InputTextModule, ButtonModule, InputMask],
  templateUrl: './registration-form.html',
  styleUrl: './registration-form.css',
})
export class RegistrationForm implements OnInit, OnDestroy {

  registrationForm!: FormGroup;

  images = [
    '/img/descarga4.jpg',
    '/img/descarga5.jpg',
    '/img/descarga11.jpg',
  ];
  currentIndex = 0;
  intervalId: any;
  get currentImage() {
    return `url('${this.images[this.currentIndex]}')`;
  }

  constructor(private router:Router, private fb:FormBuilder){}

  ngOnInit() {
    this.intervalId = setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }, 3000);

    this.registrationForm = this.fb.group({
      name:['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]+$/)]],
      lastname:['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]+$/)]],
      username:['', [Validators.required, Validators.minLength(4)]],
      email:['', [Validators.required, Validators.email]],
      phoneNumber:['', Validators.required],
      password:['', [Validators.required, Validators.minLength(8)]]
    })
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }
}
