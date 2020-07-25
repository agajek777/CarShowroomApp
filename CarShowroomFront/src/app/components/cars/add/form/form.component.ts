import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Car } from 'src/app/models/car';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  public carForm = new FormGroup({
    brand: new FormControl('', [Validators.required]),
    model: new FormControl('', [Validators.required]),
    engine: new FormControl(''),
    power: new FormControl('', [Validators.min(0)]),
    production: new FormControl('', [
      Validators.required,
      Validators.min(1970),
      Validators.max(2020)
    ]),
    price: new FormControl('', [
      Validators.required,
      Validators.min(0),
    ]),
    imagePath: new FormControl(''),
    description: new FormControl(''),
    mileage: new FormControl('', [Validators.min(0)])
  });

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
