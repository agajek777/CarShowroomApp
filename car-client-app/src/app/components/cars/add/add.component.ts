import { Component, OnInit } from '@angular/core';
import { Car } from "../../../models/car";
import { HttpService } from "../../../services/http.service";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {
  carForm = this.fb.group({
    brand: ['', Validators.required],
    /*brand: new FormControl('', [
      Validators.required
    ]),*/
    model: ['', Validators.required],
    engine: [''],
    power: [''],
    production: ['', [Validators.min(1970), Validators.max(2020)]],
    price: [1],
    imagePath: [''],
    description: [''],
    mileage: ['']
  });
  public noImage: string = "https://softsmart.co.za/wp-content/uploads/2018/06/image-not-found-1038x576.jpg";

  constructor(private fb: FormBuilder, private httpService: HttpService, private sanitizer: DomSanitizer) {}


   public addCar() {
    let route: string = "https://localhost:44332/api/car/";
    if (this.carForm.value.imagePath === '') {
      this.carForm.value.imagePath = this.noImage;
    }
    this.carForm.value.production += '-01-01T00:00:00';
    var car: Car = this.carForm.value;
    this.httpService.addData(route, car)
      .subscribe((result) => {
        console.log("Udało się")
      },
      (error) => {
        console.error(error);
      });
   }

  public onSubmit() {
    console.log('car added');
    this.addCar();
   }

   getImgContent(url: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
}
