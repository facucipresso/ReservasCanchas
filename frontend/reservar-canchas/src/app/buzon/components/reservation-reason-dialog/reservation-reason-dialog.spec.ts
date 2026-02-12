import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationReasonDialog } from './reservation-reason-dialog';

describe('ReservationReasonDialog', () => {
  let component: ReservationReasonDialog;
  let fixture: ComponentFixture<ReservationReasonDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationReasonDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationReasonDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
