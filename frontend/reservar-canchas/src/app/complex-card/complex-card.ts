import { Component, Input } from '@angular/core';
import { ComplexCardModel } from '../models/complexcard.model';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import {Router } from '@angular/router';

@Component({
  selector: 'app-complex-card',
  imports: [CardModule,ButtonModule, CommonModule],
  templateUrl: './complex-card.html',
  styleUrl: './complex-card.css',
})
export class ComplexCard {

  @Input() complexCard!: ComplexCardModel;

  constructor(private router:Router){}

  viewComplexDetail(){
    this.router.navigate(["/complexes", this.complexCard.id]);
  }
}
