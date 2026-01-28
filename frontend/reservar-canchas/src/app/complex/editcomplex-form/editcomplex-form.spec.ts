import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditcomplexForm } from './editcomplex-form';

describe('EditcomplexForm', () => {
  let component: EditcomplexForm;
  let fixture: ComponentFixture<EditcomplexForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditcomplexForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditcomplexForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
