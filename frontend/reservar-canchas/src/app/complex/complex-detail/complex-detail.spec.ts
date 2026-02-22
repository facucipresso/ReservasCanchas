import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexDetailModel } from './complex-detail';

describe('ComplexDetail', () => {
  let component: ComplexDetailModel;
  let fixture: ComponentFixture<ComplexDetailModel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComplexDetailModel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComplexDetailModel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
