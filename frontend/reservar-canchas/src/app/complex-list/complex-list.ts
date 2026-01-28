import { CommonModule } from '@angular/common';
import { Component, numberAttribute, OnInit } from '@angular/core';
import { ComplexCardModel } from '../models/complexcard.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Complex } from '../services/complex';
import { ComplexCard } from '../complex-card/complex-card';
import { Auth } from '../services/auth';
import { Toast, ToastModule } from 'primeng/toast';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-complex-list',
  imports: [CommonModule, ComplexCard, ToastModule, ProgressSpinnerModule],
  templateUrl: './complex-list.html',
  styleUrl: './complex-list.css',
})
export class ComplexList implements OnInit {
  complexes: ComplexCardModel[] = [];
  params!: any;
  title!: string;
  empty!: string;
  isAdmin!: boolean;
  isLoading: boolean = false;
  constructor(public route: ActivatedRoute, private complexService: Complex, private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.params = params;
    });

    this.route.data.subscribe(data => {
      this.isLoading = true;
      if(data['mode'] === 'admin'){
        this.title = 'Mis complejos'
        this.empty = 'No tienes complejos registrados aún. ¡Agrega uno nuevo!';
        this.isAdmin = true;
        this.getComplexCardsAdmin();
      }else{
        this.empty = 'No se encontraron complejos que coincidan con la búsqueda.';
        this.title = 'Complejos que coinciden con la busqueda'
        this.getComplexCardsFilters(this.params);
      }
    })

  }

  getComplexCardsFilters(params:any){
    this.complexService.getComplexesWithFilters(params).subscribe(result => {
      console.log(params);
      console.log('Resultado filtro: ', result);
      this.isLoading = false;
      this.complexes = result;
    })
  }

  getComplexCardsAdmin(){
    this.complexService.getMyComplexes().subscribe(result => {
      console.log('Resultado mios: ', result);
      this.isLoading = false;
      this.complexes = result;
    })
  }

  onAddComplex(){
    this.router.navigate(['/register-complex']);
  }
}
