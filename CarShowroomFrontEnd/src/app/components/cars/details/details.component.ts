import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';
import { FakeData } from 'src/app/models/fake-data';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public car: Car;
  private id: string;
  public isLoaded: boolean = false;

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.httpService.getData(this.id).subscribe(
      (result) => {
        console.log(result);
        this.car = result as Car;
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
        this.openDialog('Error while deleting.', false);
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
