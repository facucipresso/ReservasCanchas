import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StadisticCard } from './stadistic-card';

describe('StadisticCard', () => {
  let component: StadisticCard;
  let fixture: ComponentFixture<StadisticCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StadisticCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StadisticCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
