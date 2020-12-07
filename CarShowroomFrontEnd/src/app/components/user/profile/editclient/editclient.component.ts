import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { DialogComponent } from 'src/app/components/cars/details/dialog/dialog.component';
import { Client } from 'src/app/models/client';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-editclient',
  templateUrl: './editclient.component.html',
  styleUrls: ['./editclient.component.css']
})
export class EditclientComponent implements OnInit {
  isLoaded: boolean = false;
  id: string;
  userName: string;
  client: Client;
  clientFormCreate: FormGroup;
  constructor(private httpService: HttpService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    console.log(this.clientFormCreate);
    this.id = this.route.snapshot.paramMap.get('id');
    this.userName = sessionStorage.getItem('username');
    this.httpService.getClient(this.id).subscribe(
      (response) => {
        this.client = response.body as Client;
        console.log(this.client);

        if (this.client.avatar === "" || this.client.avatar === null) {
          this.client.avatar = "https://avios.pl/wp-content/uploads/2018/01/no-avatar.png";
        }

        this.clientFormCreate = new FormGroup({
          email: new FormControl(this.client.email),
          description: new FormControl(this.client.description),
          avatar: new FormControl(this.client.avatar)
        });

        console.log(this.clientFormCreate);


        this.isLoaded = true;
      },
      (error) => {
        var resp = error as HttpErrorResponse;
        this.openDialog('Error while loading Clients account', true)
      }
    );
  }

  openDialog(result: string, redirect: boolean) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
    dialogRef.componentInstance.okRedirect = redirect;
  }

  updateClient(client: Client) {
    client.identityId = sessionStorage.getItem('id');
    this.httpService.editClient(client, client.identityId, sessionStorage.getItem('access_token'))
      .subscribe(
        (response) => {
          this.openDialog('Account has been edited successfully!', true);
        },
        (error) => {
          this.openDialog('Error while editing an account :( Try again later...', true);
        }
    );
  }
}
