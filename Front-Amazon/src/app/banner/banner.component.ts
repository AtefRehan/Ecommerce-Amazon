import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.css'],
  animations: [
    trigger('fadeInOut', [
      transition(':increment', [
        style({ transform: 'translateX(100%)' }),
        animate('300ms ease-out', style({ transform: 'translateX(0)' })),
      ]),
      transition(':decrement', [
        style({ transform: 'translateX(-100%)' }),
        animate('300ms ease-out', style({ transform: 'translateX(0)' })),
      ]),
    ]),
  ],
})
export class BannerComponent implements OnInit {

  backgroundImages = ['assets/banner1.jpg', 'assets/banner2.jpg', 'assets/banner3.jpg']
  count: number = -1
  timerGate: boolean = false

  ngOnInit(): void {

    setInterval(() => {
      if (!this.timerGate) {
        if (this.count == 2) {
          this.count = -1
        }
        else if (this.count >= -1 && this.count < 3) {
          this.count++
        }
      }
    }, 3000)
  }



  right() {
    this.timerGate=true
    if (this.count == 2) {
      this.count = -1
    }
    else if (this.count >= -1 && this.count < 3) {
      this.count++
    }
    setTimeout(()=>{
      this.timerGate=false
    },2000)
  }
  left() {
    this.timerGate=true
    if (this.count == -1) {
      this.count = 2
    }
    else if (this.count >= -1 && this.count < 3) {
      this.count--
    }
    console.log(this.timerGate)

    setTimeout(()=>{
      this.timerGate=false
    },2000)

  }
}
