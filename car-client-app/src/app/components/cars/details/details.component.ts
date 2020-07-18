import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  id: any;
  paramsSub: any;
  car: Car;

  constructor(private activatedRoute: ActivatedRoute, private httpService: HttpService) { }

  ngOnInit(): void {
    let route: string = "https://localhost:44332/api/car/";
    this.paramsSub = this.activatedRoute.params.subscribe(params => this.id = parseInt(params['id'], 10))
    this.httpService.getData(route + this.id)
      .subscribe((result) => {
        this.car = result as Car;
      },
      (error) =>
      {
        console.error(error);
      });
  }

  public onClickDelete()
  {
    let route: string = "https://localhost:44332/api/car/" + this.id;
    this.httpService.deleteData(route).subscribe(
      (result) => {
        console.log(result);
      }
    )
  }

}
