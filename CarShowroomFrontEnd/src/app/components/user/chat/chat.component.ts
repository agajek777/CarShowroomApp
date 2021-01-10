import { OnDestroy } from '@angular/core';
import { AfterViewChecked, Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Message } from 'src/app/models/message';
import { UserDto } from 'src/app/models/user-dto';
import { HttpService } from 'src/app/services/http.service';
import { JWTTokenServiceService } from 'src/app/services/jwttoken-service.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import Swal from 'sweetalert2';
import { DialogComponent } from '../../cars/details/dialog/dialog.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked, OnDestroy {
  userId: string;
  isLogged: boolean = false;
  clicked: boolean = true;
  recipientControl = new FormControl('', Validators.required);
  textControl = new FormControl('');
  users: UserDto[];
  messages: Message[] = undefined;
  options: string[] = [];
  filteredOptions: Observable<string[]>;
  chatSub: Subscription;

  constructor(private jwtService: JWTTokenServiceService, private httpService: HttpService, private dialog: MatDialog, private signalRService: SignalRService) { }


  ngOnDestroy(): void {
    this.chatSub.unsubscribe();
  }

  ngAfterViewChecked(): void {
    var objDiv = document.getElementById("messages");
    objDiv.scrollIntoView(false);
  }

  ngOnInit(): void {
    this.isUserLogged();

    this.userId = sessionStorage.getItem('id');

    this.filteredOptions = this.recipientControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );

    this.chatSub = this.signalRService.signalReceived.subscribe((signal: Message) => {
      console.log(signal);

      if (this.recipientControl.value == signal.senderName) {
        this.messages.push(signal);
        return;
      }

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

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    if (filterValue === "") {
      return;
    }

    this.httpService.getUsers(value, sessionStorage
      .getItem('access_token'))
      .subscribe(
        (result) =>
        {
          this.users = result.body as UserDto[];
          console.log(this.users);

          this.options.splice(0, this.options.length);

          this.users.forEach(user => {
            this.options.push(user.userName);
          });
        }
      );

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
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
          console.log('Message sent.');
          var dt = new Date();
          var message: Message = {
            receiverId: recipientId,
            receiverName: recipient,
            senderId: sessionStorage.getItem('id'),
            senderName: sessionStorage.getItem('username'),
            sent: dt.toISOString(),
            text: text
          }

          this.messages.push(message);
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
