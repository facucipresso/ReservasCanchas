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
import { Menu } from 'primeng/menu';
import { MenuItem, MessageService } from 'primeng/api';
import { Auth } from '../services/auth';
import { Toast } from 'primeng/toast';

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
    Menu,
    Toast
  ],
  templateUrl: './header.html',
  styleUrl: './header.css',
  providers: [MessageService]
})
export class Header implements OnInit {
  isLoggedIn!:boolean;
  isAdmin!:boolean;
  visible: boolean = false;
  value!: string;
  loginForm!: FormGroup;
  options: MenuItem[] | undefined;


  constructor(public router: Router, private fb: FormBuilder, public authService: Auth, private messageService:MessageService) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.getToken() ? true : false;
    this.isAdmin = this.authService.getUserRole() == 'AdminComplejo' ? true : false;
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
    this.options = [
            {
                label: 'Opciones',
                items: [
                    {
                      label: 'Mis reservas',
                      icon: 'pi pi-calendar'
                    },
                    ...(this.isAdmin ? [{
                      label: 'Mis complejos',
                      icon: 'pi pi-building'
                    }] : []),
                    {
                      label: 'Mi buzón',
                      icon: 'pi pi-envelope'
                    },
                    {
                      label: 'Mi perfil',
                      icon: 'pi pi-user'
                    },
                    {
                      separator: true
                    },
                    {
                      label: 'Cerrar sesión',
                      icon: 'pi pi-sign-out',
                      command: () => this.logout()
                    }
                ]
            }
        ];
  }

  showDialog() {
    this.visible = true;
  }

  onDialogClose() {
    this.loginForm.reset();
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    const formLoginInfo = this.loginForm.value;

    this.authService.login(formLoginInfo).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        console.log("LOGEO EXITOSO", response);
        console.log(this.authService.getUserRole());
        this.messageService.add({
          severity:'success',
          summary:'Inicio de sesión exitoso',
          detail:'Has iniciado sesión correctamente',
          life: 1500
        })
        this.isLoggedIn = true;
        this.isAdmin = this.authService.getUserRole() == 'AdminComplejo' ? true : false;
        this.visible = false; 
        this.loginForm.reset();
      },
      error: (err) => {
        console.log('ERROR DEL BACKEND:', err);
        const backendError = err?.error;
        const message = backendError?.detail || 'Error desconocido';
        this.messageService.add({
          severity:'error',
          summary:backendError?.title || 'Error',
          detail: message,
          life: 1500
        })
      }
    })
  }

  goToRegistrationForm() {
    this.router.navigate(['/register']);
    this.visible = false;
  }

  goToCreateComplex(){
    this.router.navigate(['/register-complex']);
    this.visible = false;
  }

  logout(){
    this.authService.logout();
    this.messageService.add({
      severity:'success',
      summary:'Cierre de sesión exitoso',
      detail:'Has cerrado la sesión correctamente',
      life: 1500
    })
    this.router.navigate(['/']);
    this.isLoggedIn = false;
    this.isAdmin = false;
  }
}
