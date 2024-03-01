import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { CalculatorComponent } from './calculator.component';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('CalculatorComponent', () => {
  let component: CalculatorComponent;
  let fixture: ComponentFixture<CalculatorComponent>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CalculatorComponent],
      imports: [HttpClientTestingModule,FormsModule,RouterTestingModule,HttpClientModule], 

    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CalculatorComponent);
    component = fixture.componentInstance;
    httpTestingController = TestBed.inject(HttpTestingController);

    fixture.detectChanges();
  });
  afterEach(() => {
    httpTestingController.verify();
  });
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should append value to input', () => {
    component.appendToInput('5');
    expect(component.input).toBe('5');
    component.appendToInput('9');
    expect(component.input).toBe('59');
  });

  it('should clear input', () => {
    component.input = '123';
    component.clearInput();
    expect(component.input).toBe('');
  });

  it('should update input on screen', () => {
    component.input = '123';
    component.updateInput();
    expect(component.input).toBe('123');
  });

});
