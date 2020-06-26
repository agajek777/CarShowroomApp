import { Component, OnInit } from '@angular/core';
import { Car } from "../../../models/car";
import { HttpService } from "../../../services/http.service";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {
  carForm = this.fb.group({
    brand: ['', Validators.required],
    model: ['', Validators.required],
    engine: [''],
    imagePath: [''],
    mileage: [''],
    description: [''],
    power: [''],
    production: ['', [Validators.min(1970), Validators.max(2020)]],
    price: ['']
  });
  public car: Car;
  public noImage: string = "https://softsmart.co.za/wp-content/uploads/2018/06/image-not-found-1038x576.jpg";

  constructor(private fb: FormBuilder, private httpService: HttpService, private sanitizer: DomSanitizer) {
    this.car = {
      id: 0,
      brand: "",
      model: "",
      engine: "",
      imagePath: "",
      mileage: 1,
      description: "",
      power: 1,
      production: "2020",
      price: 1
    }
   }

   public addCar() {
    let route: string = "https://localhost:44332/api/car/";
    if (this.car.imagePath === '') {
      this.car.imagePath = this.noImage;
    }
    this.car.production += '-01-01T00:00:00';
    this.httpService.addData(route, this.car)
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
