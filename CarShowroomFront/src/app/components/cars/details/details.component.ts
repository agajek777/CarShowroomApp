import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car';
import { HttpService } from 'src/app/services/http.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';
import { FakeData } from 'src/app/models/fake-data';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public car: Car;
  private id: string;
  public isLoaded: boolean = false;

  constructor(private httpService: HttpService, private route: ActivatedRoute, private dialog: MatDialog) { }

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
    this.httpService.deleteData(this.car.id).subscribe(
      (result) => {
        let text = JSON.parse(JSON.stringify(result));
        this.openDialog(text.text);
      },
      (error) =>
      {
        this.openDialog('Error while deleting.');
      }
    )
  }

  openDialog(result: string) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
  }
}
