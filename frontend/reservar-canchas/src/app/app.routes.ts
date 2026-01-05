import { Routes } from '@angular/router';
import { Home } from './home/home';
import { ComplexList } from './complex-list/complex-list';
import { ComplexDetail } from './complex-detail/complex-detail';
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
    path: 'complexes',
    component:ComplexList
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
