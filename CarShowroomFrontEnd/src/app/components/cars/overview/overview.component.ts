import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { FakeData } from 'src/app/models/fake-data';
import { HttpResponse } from '@angular/common/http';
import { PagesInfo } from 'src/app/models/pages-info';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  public cars: Car[];
  public hasNext: boolean;
  public hasPrevious: boolean;
  public totalPages: number;
  public currentPage: number;
  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.getData().subscribe(
      (result) => {
        console.log(result);
        this.cars = new Array<Car>();
        console.log('cleared');

        this.cars = result.body as Car[];
        console.log(this.cars);

        console.log(result.headers.keys());
        var pages = result.headers.get('x-pagination');
        console.log(pages);

        var pagesInfo: PagesInfo = JSON.parse(pages);
        console.log(pagesInfo);

        this.hasNext = pagesInfo.HasNext;
        this.hasPrevious = pagesInfo.HasPrevious;
        this.totalPages = pagesInfo.TotalPages;
        this.currentPage = pagesInfo.CurrentPage;
      },
      (error) =>
      {
        var fkData = new FakeData();
        this.cars = fkData.cars;
      }
    );
  }

  nextPage() {
    this.httpService.getData("?PageNumber=" + (this.currentPage+1)).subscribe(
      (result) => {
        console.log(result);
        this.cars = new Array<Car>();
        console.log('cleared');

        this.cars = result.body as Car[];
        console.log(this.cars);

        console.log(result.headers.keys());
        var pages = result.headers.get('x-pagination');
        console.log(pages);

        var pagesInfo: PagesInfo = JSON.parse(pages);
        console.log(pagesInfo);

        this.hasNext = pagesInfo.HasNext;
        this.hasPrevious = pagesInfo.HasPrevious;
        this.totalPages = pagesInfo.TotalPages;
        this.currentPage = pagesInfo.CurrentPage;
        console.log('edited');

      },
      (error) =>
      {
        var fkData = new FakeData();
        this.cars = fkData.cars;
      }
    );
  }

  previousPage() {
    this.httpService.getData("?PageNumber=" + (this.currentPage-1)).subscribe(
      (result) => {
        console.log(result);
        this.cars = new Array<Car>();
        console.log('cleared');

        this.cars = result.body as Car[];
        console.log(this.cars);

        console.log(result.headers.keys());
        var pages = result.headers.get('x-pagination');
        console.log(pages);

        var pagesInfo: PagesInfo = JSON.parse(pages);
        console.log(pagesInfo);

        this.hasNext = pagesInfo.HasNext;
        this.hasPrevious = pagesInfo.HasPrevious;
        this.totalPages = pagesInfo.TotalPages;
        this.currentPage = pagesInfo.CurrentPage;
      },
      (error) =>
      {
        var fkData = new FakeData();
        this.cars = fkData.cars;
      }
    );
  }

  customTB(index, car: Car) {
    return `${index}-${car.id}`;
  }

  scrollTop(el: HTMLElement) {
    el.scrollIntoView({
      behavior: 'smooth'
    });
  }

}
