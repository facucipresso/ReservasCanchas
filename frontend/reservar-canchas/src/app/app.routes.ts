import { Routes } from '@angular/router';
import { Home } from './home/home';
import { ComplexList } from './complex-list/complex-list';
import { ComplexDetail } from './complex/complex-detail/complex-detail';
import { RegistrationForm } from './registration-form/registration-form';
import { CreatecomplexForm } from './createcomplex-form/createcomplex-form';

export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path: 'register',
    component:RegistrationForm
  },
  {
    path: 'complexes/:id',
    component: ComplexDetail
  },
  {
    path: 'admin/complexes',
    component: ComplexList,
    data: {mode:'admin'}
  },
  {
    path: 'search/complexes',
    component:ComplexList,
    data:{mode:'search'}
  },
  {
    path: 'register-complex',
    component:CreatecomplexForm
  },
  {
    path: '**',
    redirectTo: ''
  }
];
