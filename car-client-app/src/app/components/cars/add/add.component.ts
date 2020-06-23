import { Component, OnInit } from '@angular/core';
import { Car } from "../../../models/car";
import { HttpService } from "../../../services/http.service";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {
  public car: Car;
  public noImage: string = "https://softsmart.co.za/wp-content/uploads/2018/06/image-not-found-1038x576.jpg";

  constructor(private httpService: HttpService, private sanitizer: DomSanitizer) {
    this.car = {
      id: 0,
      brand: "",
      model: "",
      engine: "",
      imagePath: "",
      mileage: 1,
      description: "",
      power: 1,
      production: "2015-01-01T00:00:00",
      price: 1
    }
   }

   public addCar() {
    let route: string = "https://localhost:44332/api/car/";
    this.httpService.addData(route, this.car)
      .subscribe((result) => {
        alert("Udało się")
      },
      (error) => {
        console.error(error);
      });
   }

   getImgContent(url: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
}
