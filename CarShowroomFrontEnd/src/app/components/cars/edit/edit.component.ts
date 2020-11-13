import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/models/car';
import { FakeData } from 'src/app/models/fake-data';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { DialogComponent } from '../details/dialog/dialog.component';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  public car: Car;
  private id: string;
  public carFormEdit: FormGroup;
  public isLoaded: boolean = false;

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.httpService.getData(this.id).subscribe(
      (result) => {
        console.log(result);
        this.car = result.body as Car;
        console.log(this.car);


        this.carFormEdit = new FormGroup({
          brand: new FormControl(this.car.brand, [Validators.required]),
          model: new FormControl(this.car.model, [Validators.required]),
          engine: new FormControl(this.car.engine),
          power: new FormControl(this.car.power, [Validators.min(0)]),
          production: new FormControl(this.car.production?.substr(0, 4), [
            Validators.required,
            Validators.min(1970),
            Validators.max(2020)
          ]),
          price: new FormControl(this.car.price, [
            Validators.required,
            Validators.min(0),
          ]),
          imagePath: new FormControl(this.car.imagePath),
          description: new FormControl(this.car.description),
          mileage: new FormControl(this.car.mileage, [Validators.min(0)])
        });
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

  onSubmit(editedCar: Car) {
    this.httpService.editData(editedCar, sessionStorage.getItem('access_token')).subscribe(
      (result) => {
        this.openDialog('Offer edited successfully!', true);
        console.log(result as Car);
      },
      (error) => {
        console.log(error);
        var response = error as HttpErrorResponse;
        if (response.status === 401) {
          this.openDialog('You must be logged in to edit this offer!', false);
        }
        else {
          this.openDialog('Error while editing an offer. Try again later.', false);
        }

      }
    );
  }

  openDialog(result: string, redirect: boolean) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
    dialogRef.componentInstance.okRedirect = redirect;
  }
}
