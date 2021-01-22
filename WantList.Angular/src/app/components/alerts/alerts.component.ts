import { Component, OnInit } from '@angular/core';
import { AlertMessage } from '../../data/alert-message';
import { AlertService } from '../../services/alert.service';
import { AlertType } from '../../data/alert-type';

@Component({
  selector: 'app-alerts',
  templateUrl: './alerts.component.html',
  styleUrls: ['./alerts.component.sass']
})
export class AlertsComponent implements OnInit {

  public AlertType = AlertType;

  public alerts: AlertMessage[] = [];

  constructor(private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.alertService.subscribe(alert => this.alerts.push(alert));
  }

  public remove(index: number): void {
    this.alerts.splice(index, 1);
  }

}
