import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { JWTTokenServiceService } from '../services/jwttoken-service.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent {
  public username: string = 'xxx';
  public isLogged: boolean = false;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private jwtService: JWTTokenServiceService, private router: Router) {
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.isUserLogged();
        this.getUsername();
      }
    });
  }

  isUserLogged() {
    this.isLogged = this.jwtService.isUserLogged();
  }
  public getUsername() {
    console.log(this.username);

    this.username = sessionStorage.getItem('username');
  }

  public usernameClick() {
    this.router.navigate(['profile/' + sessionStorage.getItem('id')]);

    //sessionStorage.removeItem('username');
    //sessionStorage.removeItem('id');
    //this.jwtService.removeToken();

    //window.location.reload();
  }
}
