import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Car } from 'src/app/models/car';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  @Input()
  public carForm: FormGroup;

  @Output() submitCar: EventEmitter<Car> = new EventEmitter();

  constructor() { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.carForm.controls[controlName].hasError(errorName);
  }


  ngOnInit(): void {
  }

  onSubmit() {
    if (this.carForm.invalid) {
      return;
    }
    this.submitCar.emit(this.carForm.value as Car);
    console.log(this.carForm.value as Car);
  }

}
