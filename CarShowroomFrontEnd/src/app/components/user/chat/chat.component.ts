import { AfterViewChecked, Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Message } from 'src/app/models/message';
import { UserDto } from 'src/app/models/user-dto';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import { DialogComponent } from '../../cars/details/dialog/dialog.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  userId: string;
  isLogged: boolean = false;
  clicked: boolean = true;
  recipientControl = new FormControl('', Validators.required);
  textControl = new FormControl('');
  users: UserDto[];
  messages: Message[] = undefined;
  options: string[] = [];
  filteredOptions: Observable<string[]>;

  constructor(private jwtService: JWTTokenServiceService, private httpService: HttpService, private dialog: MatDialog, private signalRService: SignalRService) { }

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

    this.filteredOptions = this.recipientControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );

    this.signalRService.signalReceived.subscribe((signal: Message) => {
      console.log(signal);

      this.messages.push(signal);
    })
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
    var recipient = this.recipientControl.value;
    console.log(recipient);

    this.httpService.getMessages(sessionStorage.getItem('id'), this.users.find(u => u.userName === recipient).id).subscribe(
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

  sendMessage() {
    var recipient = this.recipientControl.value;
    var recipientId = this.users.find(u => u.userName === recipient).id;

    var text = this.textControl.value;
    console.log(recipientId + text);
    if (recipientId !== null && text !== null)
    {
      this.httpService.sendMessage(recipientId, text, sessionStorage.getItem('access_token')).subscribe(
        (result) => {
          console.log('x');
        }
      );


    }
    else
    {
      return;
    }
  }

  openDialog(result: string, redirect: boolean) {
    let dialogRef: MatDialogRef<DialogComponent> = this.dialog.open(DialogComponent);
    dialogRef.componentInstance.title = 'Result'
    dialogRef.componentInstance.message = result;
    dialogRef.componentInstance.okRedirect = redirect;
  }
}
