import { Component, Input, OnInit } from '@angular/core';
import { ComplexCardModel } from '../models/complexcard.model';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import {Router } from '@angular/router';
import { Auth } from '../services/auth';

@Component({
  selector: 'app-complex-card',
  imports: [CardModule,ButtonModule, CommonModule],
  templateUrl: './complex-card.html',
  styleUrl: './complex-card.css',
})
export class ComplexCard implements OnInit{

  @Input() complexCard!: ComplexCardModel;
  @Input() selectedDate!: string|null;
  isAdmin!: boolean;
  isOwnerComplex!: boolean;
  backendUrl = 'https://localhost:7004';

  constructor(private router:Router, private authService:Auth){}

  ngOnInit(): void {
    this.isAdmin = this.authService.getUserRole() === 'AdminComplejo'
    this.isOwnerComplex = parseInt(this.authService.getUserId()) === this.complexCard.userId;
  }

  viewComplexDetail(){
    this.router.navigate(["/complexes", this.complexCard.id],
      {
        queryParams: {
          date: this.selectedDate
        }
      }
    );
  }  
}

