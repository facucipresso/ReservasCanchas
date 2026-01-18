import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexInfo } from './complex-info';

describe('ComplexInfo', () => {
  let component: ComplexInfo;
  let fixture: ComponentFixture<ComplexInfo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComplexInfo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComplexInfo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
