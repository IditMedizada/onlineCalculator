import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent { 
  input: string = '';
  history: string[] = ['empty'];
  // if it's the first time the history is empty
  firstTime: boolean = true;
  private serverUrl = 'http://localhost:8888'; 
  constructor(private http: HttpClient) {}


  appendToInput(value: string): void {
    this.input += value;
    this.updateInput();
  }

  clearInput(): void {
    this.input = '';
    this.updateInput();
  }

  calculateResult(): void {
    const url = `${this.serverUrl}`;
    this.http.post<string>(url, this.input, { withCredentials: true }).subscribe({
      next: (data) => {
        // Add the calculation to the history
        if (this.firstTime){
          this.history = []
          this.firstTime = false
        }
        this.history.push(`${this.input} = ${data}`);
        // update the result on the screen
        this.input = data;
        this.updateInput();
      },
      error: (error) => {
        console.error('Error occurred:', error);
      },
    });
  }
 
  updateInput(): void {
    const inputElement = document.getElementById('screen') as HTMLInputElement;
    if (inputElement) {
      inputElement.value = this.input;
    }
  }
}
