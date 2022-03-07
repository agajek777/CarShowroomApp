import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/models/car';
import { carWithUserDetails } from 'src/app/models/carWithUserDetails';
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
  public carWithUserDetails: carWithUserDetails;
  private id: string;
  public carFormEdit: FormGroup;
  public isLoaded: boolean = false;

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.httpService.getData(this.id).subscribe(
      (result) => {
        console.log(result);
        this.carWithUserDetails = result.body as carWithUserDetails;
        console.log(this.carWithUserDetails);


        this.carFormEdit = new FormGroup({
          brand: new FormControl(this.carWithUserDetails.car.brand, [Validators.required]),
          model: new FormControl(this.carWithUserDetails.car.model, [Validators.required]),
          engine: new FormControl(this.carWithUserDetails.car.engine),
          power: new FormControl(this.carWithUserDetails.car.power, [Validators.min(0)]),
          production: new FormControl(this.carWithUserDetails.car.production?.substr(0, 4), [
            Validators.required,
            Validators.min(1950),
            Validators.max((new Date()).getFullYear())
          ]),
          price: new FormControl(this.carWithUserDetails.car.price, [
            Validators.required,
            Validators.min(0),
          ]),
          imagePath: new FormControl(this.carWithUserDetails.car.imagePath),
          description: new FormControl(this.carWithUserDetails.car.description),
          mileage: new FormControl(this.carWithUserDetails.car.mileage, [Validators.min(0)])
        });
        this.isLoaded = true;
      },
      (error) => {
        console.log(error);
        var fkData = new FakeData();
        this.carWithUserDetails.car = fkData.cars.find(c => c.id.toString() == this.id);
        this.isLoaded = true;
      }
    );
  }

  onSubmit(editedCar: Car) {
    editedCar.id = +this.id;
    editedCar.ownerId = this.carWithUserDetails.userId;
    this.carAdapter(editedCar);
    this.httpService.editData(editedCar, this.id, sessionStorage.getItem('access_token')).subscribe(
      (result) => {
        this.openDialog('Offer edited successfully!', true);
        console.log(result as Car);
      },
      (error) => {
        console.log(error);
        var response = error as HttpErrorResponse;
        if (response.status === 401) {
          this.openDialog('You must be logged in to edit this offer!', false);
          return;
        }
        if (response.status === 403) {
          this.openDialog('You must create an client account to edit models!', false);
          return;
        }
        if (response.status === 404) {
          this.openDialog('You must must be an owner of an offer to edit models!', false);
          return;
        }
        else {
          this.openDialog('Error while editing an offer. Try again later.', false);
          return;
        }

      }
    );
  }

  carAdapter(model: Car) {
    model.production += '-06-22T00:00:00';
    if (model.mileage === null) {
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
}
