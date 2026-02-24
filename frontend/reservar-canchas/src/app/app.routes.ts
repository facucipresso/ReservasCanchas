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
import { adminGuard } from './guards/admin-guard';
import { authGuard } from './guards/auth-guard';
import { Notfound } from './not-found/notfound/notfound';

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
    canActivate: [adminGuard],
    component: ComplexList,
    data: {mode:'admin'}
  },
  {
    path: 'admin/complexes/:id/reservations',
    canActivate: [adminGuard],
    component: ComplexReservations
  },
  {
    path: 'search/complexes',
    component:ComplexList,
    data:{mode:'search'}
  },
  {
    path: 'register-complex',
    canActivate: [authGuard],
    component:CreatecomplexForm
  },
  {
    path: 'reservation/checkout/:id',
    canActivate: [authGuard],
    component: ReservationCheckout
  },
  {
    path: 'reservations',
    canActivate: [authGuard],
    component: MyReservations
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    component: Profile
  },
  {
    path: 'notifications',
    canActivate: [authGuard],
    component: Buzon
  },
  {
    path: '**',
    component: Notfound
  }
];
