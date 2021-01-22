import { Component, OnInit } from '@angular/core';

class Kuroneko {
  name: string;
  left: boolean;

  constructor( name: string, left: boolean ) {
    this.name = name;
    this.left = left;
  }
}

@Component({
  selector: 'app-kuroneko',
  templateUrl: './kuroneko.component.html',
  styleUrls: ['./kuroneko.component.sass']
})
export class KuronekoComponent implements OnInit {

  public active: Kuroneko = null;

  private nekos = [
    new Kuroneko('a', true),
    new Kuroneko('b', false),
    new Kuroneko('c', false),
  ];

  private christmasNeko = new Kuroneko('d', false);

  constructor() {
  }

  ngOnInit(): void {
    const today = new Date();
    if (today.getMonth() === 11 || (today.getMonth() === 0 && today.getDate() < 10)) {
      this.active = this.christmasNeko;
    } else {
      this.active = this.nekos[Math.floor(Math.random() * this.nekos.length)];
    }
  }

}
