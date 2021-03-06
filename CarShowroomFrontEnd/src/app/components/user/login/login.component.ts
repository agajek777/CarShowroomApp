import { HttpErrorResponse } from '@angular/common/http';
import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginDto } from 'src/app/models/login-dto';
import { Token } from 'src/app/models/token';
import { User } from 'src/app/models/user';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import { DialogComponent } from '../../cars/details/dialog/dialog.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private dialog: MatDialog, private router: Router, private injector: Injector) { }

  ngOnInit(): void {
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.loginForm.controls[controlName].hasError(errorName);
  }

  public onSubmit() {
    var user = this.loginForm.value as User;
    this.httpService.login(user.userName, user.password).subscribe(
      (result) => {
        console.log(JSON.stringify(result));
        var response = result as LoginDto;

        this.jwtService.setToken(response.token);
        sessionStorage.setItem('username', user.userName);
        sessionStorage.setItem('id', response.id);

        var chatService = this.injector.get(SignalRService);
        chatService.buildConnection();
        chatService.startConnection();

        this.router.navigate(['/overview']);
      },
      (error) => {
        console.log(error);
        var response = error as HttpErrorResponse;
        if (response.status === 400) {
          this.openDialog('Username and password are required.', false);
        }
        else if (response.status === 401 || response.status === 404) {
          this.openDialog('Opss... Invalid username or password.', false);
        }
        else {
          this.openDialog('Error while logging in. Try again later.', false);
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
