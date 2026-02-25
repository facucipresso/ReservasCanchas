import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatecomplexForm } from './createcomplex-form';

describe('CreatecomplexForm', () => {
  let component: CreatecomplexForm;
  let fixture: ComponentFixture<CreatecomplexForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatecomplexForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatecomplexForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
