import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationCheckout } from './reservation-checkout';

describe('ReservationCheckout', () => {
  let component: ReservationCheckout;
  let fixture: ComponentFixture<ReservationCheckout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationCheckout]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationCheckout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
