import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Client } from 'src/app/models/client';
import { clientWithUsername } from 'src/app/models/clientWithUsername';
import { Message } from 'src/app/models/message';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import Swal from 'sweetalert2';
import { DialogComponent } from '../../cars/details/dialog/dialog.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  public signalRSub: Subscription;
  private id: string;
  isOwner: boolean;
  userName: string;
  client: Client;
  clientWithUsername: clientWithUsername;
  hasAccount: boolean = false;
  constructor(private signalRService: SignalRService, private router: Router, private jwtTokenService: JWTTokenServiceService, private httpService: HttpService, private route: ActivatedRoute, private dialog: MatDialog) { }

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

    this.id = this.route.snapshot.paramMap.get('id');
    this.userName = sessionStorage.getItem('username');
    this.httpService.getClient(this.id).subscribe(
      (response) => {
        this.clientWithUsername = response.body as clientWithUsername;
        this.client = this.clientWithUsername.client;
        this.userName = this.clientWithUsername.userName;
        console.log(this.client);

        if (this.client.avatar === "" || this.client.avatar === null) {
          this.client.avatar = "https://avios.pl/wp-content/uploads/2018/01/no-avatar.png";
        }

        this.isOwner = this.id === sessionStorage.getItem('id');
        this.hasAccount = true;
      },
      (error) => {
        var resp = error as HttpErrorResponse;
        if (resp.status === 400) {
          this.hasAccount = false;
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

  onClickDelete() {
    this.httpService.deleteClient(this.id, sessionStorage.getItem('access_token')).subscribe(
      (response) => {
        this.openDialog("Account has been successfully deleted!", true);
      }
    );
  }

  onClickLogOut() {
    sessionStorage.removeItem('username');
    sessionStorage.removeItem('id');
    this.jwtTokenService.removeToken();

    this.router.navigate(['']);
  }

  onClickCreate() {
    this.router.navigate(['create']);
  }

  onClickEdit() {
    this.router.navigate(['editclient/' + this.id]);
  }
}
