import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexCard } from './complex-card';

describe('ComplexCard', () => {
  let component: ComplexCard;
  let fixture: ComponentFixture<ComplexCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComplexCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComplexCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
