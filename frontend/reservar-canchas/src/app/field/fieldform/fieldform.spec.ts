import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Fieldform } from './fieldform';

describe('Fieldform', () => {
  let component: Fieldform;
  let fixture: ComponentFixture<Fieldform>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Fieldform]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Fieldform);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
