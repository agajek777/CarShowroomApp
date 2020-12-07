import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/components/cars/details/dialog/dialog.component';
import { Client } from 'src/app/models/client';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  clientFormCreate: FormGroup = new FormGroup({
    email: new FormControl(''),
    description: new FormControl(''),
    avatar: new FormControl('')
  });

  constructor(private httpService: HttpService, private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  addClient(clientToAdd: Client) {
    clientToAdd.identityId = sessionStorage.getItem('id');
    this.httpService.addClient(clientToAdd, sessionStorage.getItem('access_token')).subscribe(
      (response) => {
        this.openDialog('Account created successfully!', true);
      },
      (error) => {
        var resp = error as HttpErrorResponse;
        if (resp.status === 400) {
          this.openDialog('No user account has been found. Register first!', false);
          return;
        }
        else {
          this.openDialog('Error while creating a client account :(', true);
          return;
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
