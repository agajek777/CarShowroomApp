import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { DialogComponent } from 'src/app/components/cars/details/dialog/dialog.component';
import { Client } from 'src/app/models/client';
import { Message } from 'src/app/models/message';
import { HttpService } from 'src/app/services/http.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit, OnDestroy {
  public signalRSub: Subscription;
  clientFormCreate: FormGroup = new FormGroup({
    email: new FormControl(''),
    description: new FormControl(''),
    avatar: new FormControl('')
  });

  constructor(private signalRService: SignalRService, private httpService: HttpService, private dialog: MatDialog) { }

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
    })
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
