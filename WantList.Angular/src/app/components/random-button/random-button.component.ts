import { Component, Input, OnInit } from '@angular/core';
import { faRandom } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-random-button',
  templateUrl: './random-button.component.html',
  styleUrls: ['./random-button.component.sass']
})
export class RandomButtonComponent implements OnInit {

  @Input() items: any[];
  @Input() idTransform: (item: any) => string;

  public randomIcon = faRandom;
  public randomId: string;

  constructor() { }

  ngOnInit(): void {
    this.refreshRandom();
  }

  public refreshRandom(): void {
    if (this.items.length === 0) {
      return;
    }
    const randomItem = this.items[Math.floor(Math.random() * this.items.length)];
    this.randomId = this.idTransform(randomItem);
  }

}
