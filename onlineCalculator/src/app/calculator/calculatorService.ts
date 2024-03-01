import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable , throwError} from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})

export class calculatorService {
    // Define the server URL
    private serverUrl = 'http://localhost:8888'; 
    constructor(private http: HttpClient) {}
    // Send a mathematical expression to the server and receive the result of the calculation
    sendAndReceiveData(arithmeticExpression: string): Observable<string> {
      const url = `${this.serverUrl}`;
      return this.http.post<string>(url, { arithmeticExpression }).pipe(
        catchError((error) => {
          console.error('Error occurred:', error);
          return throwError(error);
        })
      );
    }
}