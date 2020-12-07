import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { DialogComponent } from 'src/app/components/cars/details/dialog/dialog.component';
import { Client } from 'src/app/models/client';
import { Message } from 'src/app/models/message';
import { HttpService } from 'src/app/services/http.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-editclient',
  templateUrl: './editclient.component.html',
  styleUrls: ['./editclient.component.css']
})
export class EditclientComponent implements OnInit, OnDestroy {
  public signalRSub: Subscription;
  isLoaded: boolean = false;
  id: string;
  userName: string;
  client: Client;
  clientFormCreate: FormGroup;
  constructor(private signalRService: SignalRService, private httpService: HttpService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnDestroy(): void {
    this.signalRSub.unsubscribe();
    console.log('signalR unsubscribed.');
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
    });

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
