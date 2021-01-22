import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {

  public readonly url: string;

  constructor(/*, private alertService: AlertService*/) {
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

      const msg = `${operation} failed: ${error.message}`;
      console.error(msg, error); // log to console
      // TODO: this.alertService.send(new AlertMessage(msg, AlertType.Error));

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
