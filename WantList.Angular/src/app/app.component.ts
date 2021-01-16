import { Component } from '@angular/core';
import { faTrash, faSortAmountUp, faSortAmountDownAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'WantList';
  icon = faTrash;
  faSortAmountDown = faSortAmountDownAlt;
  faSortAlphaUp = faSortAmountUp;
}
