import { Routes } from '@angular/router';
import { Home } from './home/home';
import { ComplexList } from './complex-list/complex-list';
import { ComplexDetail } from './complex/complex-detail/complex-detail';
import { RegistrationForm } from './registration-form/registration-form';
import { CreatecomplexForm } from './createcomplex-form/createcomplex-form';
import { ReservationCheckout } from './reservation/reservation-checkout/reservation-checkout';
import { Profile } from './profile/profile';
import { Buzon } from './buzon/buzon';
import { ReservationDetail } from './reservation/reservation-detail/reservation-detail';
import { MyReservations } from './reservation/my-reservations/my-reservations';
import { ComplexReservations } from './reservation/complex-reservations/complex-reservations';

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
    path: 'admin/complexes/:id/reservations',
    component: ComplexReservations
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
    path: 'reservation/checkout/:id',
    component: ReservationCheckout
  },
  {
    path: 'reservations',
    component: MyReservations
  },
  {
    path: 'profile',
    component: Profile
  },
  {
    path: 'buzon',
    component: Buzon
  },
  {
    path: '**',
    redirectTo: ''
  }
];
