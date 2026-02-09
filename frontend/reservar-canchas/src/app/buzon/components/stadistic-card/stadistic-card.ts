import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stadistic-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stadistic-card.html',
  styleUrl: './stadistic-card.css',
})
export class StadisticCard {

  @Input() title!: string;
  @Input() value!: number;
  @Input() icon?: string;

}
