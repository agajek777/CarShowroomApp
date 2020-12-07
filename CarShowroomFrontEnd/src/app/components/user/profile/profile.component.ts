import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { DialogComponent } from '../../cars/details/dialog/dialog.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private id: string;
  userName: string;
  client: Client;
  hasAccount: boolean = false;
  constructor(private router: Router, private jwtTokenService: JWTTokenServiceService, private httpService: HttpService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.userName = sessionStorage.getItem('username');
    this.httpService.getClient(this.id).subscribe(
      (response) => {
        this.client = response.body as Client;
        console.log(this.client);

        if (this.client.avatar === "" || this.client.avatar === null) {
          this.client.avatar = "https://avios.pl/wp-content/uploads/2018/01/no-avatar.png";
        }

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
