import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { FakeData } from 'src/app/models/fake-data';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  public cars: Car[];
  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.getData().subscribe(
      (result) => {
        console.log(result);
        this.cars = result as Car[];
      },
      (error) =>
      {
        var fkData = new FakeData();
        this.cars = fkData.cars;
      }
    );
  }

}
