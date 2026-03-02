import { CommonModule } from '@angular/common';
import { Component,OnInit } from '@angular/core';
import { ComplexCardModel } from '../../models/complex/complexcard.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Complex } from '../../services/complex';
import { ComplexCard } from '../complex-card/complex-card';
import { ToastModule } from 'primeng/toast';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MessageService } from 'primeng/api';
import { FormSearchComplexes } from '../../form-search-complexes/form-search-complexes';

@Component({
  selector: 'app-complex-list',
  imports: [CommonModule, ComplexCard, ToastModule, ProgressSpinnerModule, FormSearchComplexes],
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
  constructor(public route: ActivatedRoute, private complexService: Complex, private router: Router, private messageService:MessageService) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
    this.isAdmin = data['mode'] === 'admin';

    if (this.isAdmin) {
      this.isLoading = true;
      this.title = 'Mis complejos';
      this.empty = 'No tienes complejos registrados aún. ¡Agrega uno nuevo!';
      this.getComplexCardsAdmin();
    } else {
      this.title = 'Complejos que coinciden con la búsqueda';
      this.empty = 'No se encontraron complejos que coincidan con la búsqueda.';

      this.route.queryParams.subscribe(params => {
        this.params = params;
        this.isLoading = true;
        this.getComplexCardsFilters(params);
      });
    }
  });
  }

  getComplexCardsFilters(params:any){
    this.complexService.getComplexesWithFilters(params).subscribe({
      next: (result) => {
        this.isLoading = false;
        this.complexes = result;
      },
      error: (err) => {
        this.isLoading = false;
        this.showBackendError(err);
      }
    })
  }

  getComplexCardsAdmin(){
    this.complexService.getMyComplexes().subscribe({
      next: (result) => {
        this.isLoading = false;
        this.complexes = result;
      },
      error: (err) => {
        this.isLoading = false;
        this.showBackendError(err);
      }
    })
  }

  onAddComplex(){
    this.router.navigate(['/register-complex']);
  }

  private showBackendError(err: any, life = 2000): void {
    const backendError = err?.error;
    this.messageService.add({
      severity: 'error',
      summary: backendError?.title || 'Error',
      detail: backendError?.detail || 'Error desconocido',
      life
    });
  }
}
