import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Recblock } from './recblock';

describe('Recblock', () => {
  let component: Recblock;
  let fixture: ComponentFixture<Recblock>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Recblock]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Recblock);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
