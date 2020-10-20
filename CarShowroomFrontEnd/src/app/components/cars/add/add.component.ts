import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../details/dialog/dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  constructor(private httpService: HttpService, private dialog: MatDialog) { }

  addCar(model: Car) {
    console.log(model);
    this.carAdapter(model);
    this.httpService.addData(model, localStorage.getItem('access_token')).subscribe(
      (result) => {
        this.openDialog('Offer added successfully!');
        console.log(result as Car);
        var car = result as Car;
        car.id
      },
      (error) => {
        this.openDialog('Error while creating an offer.');
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

  openDialog(result: string) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
  }

  ngOnInit(): void {
  }

}
