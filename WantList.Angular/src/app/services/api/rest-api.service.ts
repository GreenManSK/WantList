import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable, of } from 'rxjs';
import { AlertService } from '../alert.service';
import { AlertMessage } from '../../data/alert-message';
import { AlertType } from '../../data/alert-type';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {

  public readonly url: string;

  constructor(private alertService: AlertService) {
    this.url = environment.restUrl;
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  public handleError<T>( operation = 'operation', result?: T ) {
    return ( error: any ): Observable<T> => {

      const errorMessage = error.error.title ?? error.message;
      const msg = `${operation} failed: ${errorMessage}`;
      console.error(msg, error); // log to console
      this.alertService.send(new AlertMessage(msg, AlertType.Error));

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
