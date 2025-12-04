import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit} from '@angular/core';
import { FormSearchComplexes } from '../form-search-complexes/form-search-complexes';



@Component({
  selector: 'app-home',
  imports: [CommonModule,FormSearchComplexes],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit, OnDestroy{
  images = [
    '/img/hero-image.jpg',
    '/img/hero-image6.jpg',
    '/img/hero-image7.jpg'
  ];

  currentIndex = 0;
  intervalId: any;

  get currentImage() {
    return `url('${this.images[this.currentIndex]}')`;
  }

  ngOnInit() {
    this.intervalId = setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }, 3000);
  }


  ngOnDestroy() {
    clearInterval(this.intervalId);
  }
}
