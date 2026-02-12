import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexReservations } from './complex-reservations';

describe('ComplexReservations', () => {
  let component: ComplexReservations;
  let fixture: ComponentFixture<ComplexReservations>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComplexReservations]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComplexReservations);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
