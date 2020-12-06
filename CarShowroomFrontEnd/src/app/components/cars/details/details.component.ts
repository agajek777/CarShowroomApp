import { Component, OnDestroy, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';
import { FakeData } from 'src/app/models/fake-data';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import { Subscription } from 'rxjs';
import { Message } from 'src/app/models/message';
import Swal from 'sweetalert2';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {
  public car: Car;
  private id: string;
  public isLoaded: boolean = false;
  public signalRSub: Subscription;

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private route: ActivatedRoute, private dialog: MatDialog, private signalRService: SignalRService) { }

  ngOnDestroy(): void {
    this.signalRSub.unsubscribe();
    console.log('signalR unsubscribed.');
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');

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

    this.httpService.getData(this.id).subscribe(
      (result) => {
        console.log(result);
        this.car = result.body as Car;
        this.isLoaded = true;
      },
      (error) => {
        console.log(error);
        var fkData = new FakeData();
        this.car = fkData.cars.find(c => c.id.toString() == this.id);
        this.isLoaded = true;
      }
    );
  }

  onClickDelete() {
    if (!this.jwtService.isUserLogged()) {
      this.openDialog('You must be logged in to delete models.', false)
      return;
    }

    this.httpService.deleteData(this.car.id, sessionStorage.getItem('access_token')).subscribe(
      (result) => {
        let text = JSON.parse(JSON.stringify(result));
        this.openDialog('Car deleted successfully.', true);
      },
      (error) =>
      {
        var response = error as HttpErrorResponse;
        if (response.status === 401) {
          this.openDialog('You must be logged in to delete models!', false);
          return;
        }
        if (response.status === 403) {
          this.openDialog('You must create an client account to delete models!', false);
          return;
        }
        if (response.status === 404) {
          this.openDialog('You must must be an owner of an offer to delete models!', false);
          return;
        }
        else
        {
          this.openDialog('Error while deleting.', false);
          return;
        }
      }
    )
  }

  openDialog(result: string, redirect: boolean) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
    dialogRef.componentInstance.okRedirect = redirect;
  }
}
