import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { User } from '../services/user';
import { Auth } from '../services/auth';
import { userRequestModel } from '../models/user/userRequest.model';
import { UserInfoModel } from '../models/user/userinfo.model';
import { MessageService } from 'primeng/api';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    CardModule,
    DividerModule
  ],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit{
  profileForm: FormGroup;
  canSave = false;

  //copia del usuario original de cuando lo llamo al back
  private originalUser!: UserInfoModel;

  constructor(private fb: FormBuilder, private userService : User, private authService : Auth, private messageService : MessageService) {
    this.profileForm = this.fb.group({
      username: [{ value: '', disabled: true }],
      email: [''],
      name: [''],
      lastName: [''],
      phone: ['']
    });
  }

  ngOnInit(): void {
      const idUser = this.authService.getUserId();
      if (!idUser) return;

      this.userService.getUserInfoById(idUser).subscribe({
        next: (user) => {

          this.originalUser = user;
          console.log('USER DESDE API 👉', user);

          this.profileForm.patchValue({
            username: user.userName,
            email : user.email,
            name : user.name,
            lastName : user.lastName,
            phone : user.phone
          });

          //para que no quede dirty desde el inicio
          this.profileForm.markAsPristine();
          this.canSave = false;
        },
        error : (err) => {
          console.error('ERROR 👉', err);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Usuario no encontrado',
            life: 2000
          });
        }
      });
      this.profileForm.valueChanges.subscribe(() => {
        this.canSave = this.profileForm.dirty;
      });
  }

  onSave() {
    if (this.profileForm.invalid || !this.canSave) return;
  
    const userId = this.authService.getUserId();
    if (!userId) return;
  
    const formValue = this.profileForm.value;

    const payload: userRequestModel = {
      name: formValue.name,
      lastName: formValue.lastName,
      email: formValue.email,
      phone: formValue.phone
    };
    
    this.userService.updateUserInfoById(userId, payload).subscribe({
      next: () => {
        this.originalUser = {
          ...this.originalUser,
          ...payload
        };
  
        this.profileForm.markAsPristine();
        this.canSave = false;
  
        this.profileForm.patchValue({
          email: payload.email,
          name: payload.name,
          lastName: payload.lastName,
          phone: payload.phone
        });
        

        this.messageService.add({
          severity: 'success',
          summary: 'Perfil actualizado',
          detail: 'Los datos se guardaron correctamente',
          life: 2000
        });


      },
      error: (err) => {
        const backendError = err?.error;
      const message =
        backendError?.detail ||
        'No se pudieron guardar los cambios';

      this.messageService.add({
        severity: 'error',
        summary: backendError?.title || 'Error',
        detail: message,
        life: 2500
      });
      }
    });
  }

  onCancel() {
    if (!this.originalUser) return;
  
    this.profileForm.patchValue({
      //username: this.originalUser.username, este dato no me lo trae
      email: this.originalUser.email,
      name: this.originalUser.name,
      lastName: this.originalUser.lastName,
      phone: this.originalUser.phone
    });
  
    this.profileForm.markAsPristine();
    this.canSave = false;
  }
  
  

}
