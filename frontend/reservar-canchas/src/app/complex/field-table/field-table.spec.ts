import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldTable } from './field-table';

describe('FieldTable', () => {
  let component: FieldTable;
  let fixture: ComponentFixture<FieldTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FieldTable]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldTable);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
