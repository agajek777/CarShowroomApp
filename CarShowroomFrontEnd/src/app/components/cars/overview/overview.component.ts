import { Component, OnDestroy, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { FakeData } from 'src/app/models/fake-data';
import { HttpResponse } from '@angular/common/http';
import { PagesInfo } from 'src/app/models/pages-info';
import { SignalRService } from 'src/app/services/signal-r.service';
import { Message } from 'src/app/models/message';
import Swal from 'sweetalert2';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit, OnDestroy {
  public cars: Car[];
  public hasNext: boolean;
  public hasPrevious: boolean;
  public totalPages: number;
  public currentPage: number;
  public signalRSub: Subscription;
  constructor(private httpService: HttpService, private signalRService: SignalRService) { }

  ngOnDestroy(): void {
    this.signalRSub.unsubscribe();
    console.log('signalR unsubscribed.');

  }

  ngOnInit(): void {
    this.currentPage = 1;

    console.log('waited 5s');

    this.signalRSub = this.signalRService.signalReceived.subscribe((signal: Message) => {
      console.log(signal);

      Swal.fire({
        position: 'top-end',
        icon: 'info',
        toast: true,
        title: 'New message from ' + signal.senderName,
        showConfirmButton: false,
        timer: 5000,
        timerProgressBar: true,
        didOpen: (toast) => {
          toast.addEventListener('mouseenter', Swal.stopTimer)
          toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
      });
    })


    this.loadPage(0);
  }

  async loadPage(num: number) {

    await this.httpService.getData("?PageNumber=" + (this.currentPage+num)).subscribe(
      (result) => {
        this.cars = new Array<Car>();

        this.cars = result.body as Car[];

        var pages = result.headers.get('x-pagination');

        var pagesInfo: PagesInfo = JSON.parse(pages);

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

  scrollTop(el: HTMLElement) {
    el.scrollIntoView({
      behavior: 'smooth'
    });
  }

}
