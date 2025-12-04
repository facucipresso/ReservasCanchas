import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormSearchComplexes } from './form-search-complexes';

describe('FormSearchComplexes', () => {
  let component: FormSearchComplexes;
  let fixture: ComponentFixture<FormSearchComplexes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormSearchComplexes]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormSearchComplexes);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
