import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from 'src/app/models/client';
import { HttpService } from 'src/app/services/http.service';

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
  constructor(private httpService: HttpService, private route: ActivatedRoute) { }

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
}
