import { AfterContentChecked, AfterViewChecked, AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Message } from 'src/app/models/message';
import { UserDto } from 'src/app/models/user-dto';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  userId: string;
  isLogged: boolean = false;
  clicked: boolean = true;
  myControl = new FormControl('', Validators.required);
  users: UserDto[];
  messages: Message[] = undefined;
  options: string[] = [];
  filteredOptions: Observable<string[]>;

  constructor(private jwtService: JWTTokenServiceService, private httpService: HttpService) { }

  ngAfterViewChecked(): void {
    var objDiv = document.getElementById("messages");
    objDiv.scrollIntoView(false);
  }

  ngOnInit(): void {
    this.isUserLogged();

    this.userId = sessionStorage.getItem('id');

    this.httpService.getUsers().subscribe(
      (result) => {
        this.users = result.body as UserDto[];
        console.log(this.users);

        this.users.forEach(user => {
          this.options.push(user.userName);
        });
      }
    );

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().indexOf(filterValue) === 0);
  }

  isUserLogged() {
    this.isLogged = this.jwtService.isUserLogged();
  }

  startChat() {
    this.clicked = true;
    this.httpService.getMessages('db3db68c-16c0-4e66-84a2-05366383e197', '73efe592-c097-43fa-91ad-36b2ca37e1c9').subscribe(
      (result) => {
        console.log(result);

        this.messages = new Array<Message>();
        console.log(this.messages);


        var msg = result.body as Message[];
        console.log(msg);


        this.messages = msg;
        console.log(this.messages);
      }
    );
  }
}
