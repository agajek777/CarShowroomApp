import { Component, OnInit } from '@angular/core';
import { Car } from "../../../models/car";
import { HttpService } from "../../../services/http.service";

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {
  public cars: Car[];

  constructor(private httpService: HttpService) { }

  public getCars() {
    let route: string = "https://localhost:44332/car/";
    this.httpService.getData(route)
      .subscribe((result) => {
        this.cars = result as Car[];
      },
      (error) => {
        console.error(error);
      });
  }
}
