import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AvatarModule } from 'primeng/avatar';
import { RatingModule } from 'primeng/rating';
import { ReviewResponse } from '../models/reservation/reviewresponse.model';

@Component({
  selector: 'app-review-card',
  imports: [CommonModule, AvatarModule, FormsModule,RatingModule],
  templateUrl: './review-card.html',
  styleUrl: './review-card.css',
})
export class ReviewCard {

  @Input() review !: ReviewResponse;
}
