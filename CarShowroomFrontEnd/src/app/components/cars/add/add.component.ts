import { Component, OnDestroy, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../details/dialog/dialog.component';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SignalRService } from 'src/app/services/signal-r.service';
import { Message } from 'src/app/models/message';
import Swal from 'sweetalert2';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit, OnDestroy {
  public signalRSub: Subscription;

  constructor(private httpService: HttpService, private dialog: MatDialog, private signalRService: SignalRService) { }

  ngOnDestroy(): void {
    this.signalRSub.unsubscribe();
    console.log('signalR unsubscribed.');
  }

  carFormAdd: FormGroup = new FormGroup({
    brand: new FormControl('', [Validators.required]),
    model: new FormControl('', [Validators.required]),
    engine: new FormControl(''),
    power: new FormControl('', [Validators.min(0)]),
    production: new FormControl('', [
      Validators.required,
      Validators.min(1950),
      Validators.max((new Date()).getFullYear())
    ]),
    price: new FormControl('', [
      Validators.required,
      Validators.min(0),
    ]),
    imagePath: new FormControl(''),
    description: new FormControl(''),
    mileage: new FormControl('', [Validators.min(0)])
  });

  addCar(model: Car) {
    console.log(model);
    this.carAdapter(model);
    this.httpService.addData(model, sessionStorage.getItem('access_token')).subscribe(
      (result) => {
        this.openDialog('Offer added successfully!', true);
        console.log(result as Car);
        var car = result as Car;
        car.id
      },
      (error) => {
        console.log(error);
        var response = error as HttpErrorResponse;
        if (response.status === 401) {
          this.openDialog('You must be logged in to add new models!', false);
          return;
        }
        if (response.status === 403) {
          this.openDialog('You must create an client account to add new models!', false);
          return;
        }
        else {
          this.openDialog('Error while creating an offer. Try again later.', false);
          return;
        }

      }
    );
  }

  carAdapter(model: Car) {
    model.production += '-06-22T00:00:00';
    if (model.mileage.toString() === "") {
      model.mileage = 0;
    }
    if (model.power.toString() === "") {
      model.power = 0;
    }
    if (model.imagePath.toString() === "") {
      model.imagePath = 'https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png';
    }
  }

  openDialog(result: string, redirect: boolean) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
    dialogRef.componentInstance.okRedirect = redirect;
  }

  ngOnInit(): void {
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
  }

}
