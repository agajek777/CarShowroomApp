import { Component, OnInit } from '@angular/core';
import { Car } from "../../../models/car";
import { HttpService } from "../../../services/http.service";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  public cars: Car[];

  constructor(private httpService: HttpService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.getCars();
  }

  public getCars() {
    let route: string = "https://localhost:44332/api/car/";
    this.httpService.getData(route)
      .subscribe((result) => {
        this.cars = result as Car[];
      },
      (error) => {
        console.error(error);
      });
  }

  getImgContent(url: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
}
