import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Token } from 'src/app/models/token';
import { User } from 'src/app/models/user';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';


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

  constructor(private httpService: HttpService, private jwtService: JWTTokenServiceService, private dialog: MatDialog) { }

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
        var token: Token = JSON.parse(JSON.stringify(result))

        this.jwtService.setToken(token.token);
        console.log(this.jwtService.jwtToken);

      },
      (error) => {
        console.log(error);
      }
    );
  }
}
