import { CommonModule } from '@angular/common';
import { Component, numberAttribute, OnInit } from '@angular/core';
import { ComplexCardModel } from '../models/complexcard.model';
import { ActivatedRoute } from '@angular/router';
import { Complex } from '../services/complex';
import { ComplexCard } from '../complex-card/complex-card';

@Component({
  selector: 'app-complex-list',
  imports: [CommonModule, ComplexCard],
  templateUrl: './complex-list.html',
  styleUrl: './complex-list.css',
})
export class ComplexList implements OnInit {
  complexes: ComplexCardModel[] = [];

  constructor(private route: ActivatedRoute, private complexService: Complex) {}

  ngOnInit() {
    /*
    this.route.queryParams.subscribe((params) => {
      this.getComplexesCards(params);
    });
    */

    const complexCardMock = [{
      id:1,
      name:"Siempre al 10",
      province: "Santa Fé",
      locality: "Rosario",
      street: "Vera Mújica",
      number: "510",
      minPriceHour: 25000,
      imagePath: "/img/complejo.jpg"
    },
  {
      id:1,
      name:"Siempre al 10",
      province: "Santa Fé",
      locality: "Rosario",
      street: "Vera Mújica",
      number: "510",
      minPriceHour: 25000,
      imagePath: "/img/complejo.jpg"
    },
  {
      id:1,
      name:"Siempre al 10",
      province: "Santa Fé",
      locality: "Rosario",
      street: "Vera Mújica",
      number: "510",
      minPriceHour: 25000,
      imagePath: "/img/complejo.jpg"
    },
  {
      id:1,
      name:"Siempre al 10",
      province: "Santa Fé",
      locality: "Rosario",
      street: "Vera Mújica",
      number: "510",
      minPriceHour: 25000,
      imagePath: "/img/complejo.jpg"
    },
  {
      id:1,
      name:"Siempre al 10",
      province: "Santa Fé",
      locality: "Rosario",
      street: "Vera Mújica",
      number: "510",
      minPriceHour: 25000,
      imagePath: "/img/complejo.jpg"
    }]

    this.complexes = complexCardMock;
  }

  getComplexesCards(params:any){
    this.complexService.getComplexesWithFilters(params).subscribe(result => {
      this.complexes = result;
    })
  }
}
