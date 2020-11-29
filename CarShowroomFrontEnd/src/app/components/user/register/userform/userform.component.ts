import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-userform',
  templateUrl: './userform.component.html',
  styleUrls: ['./userform.component.css']
})
export class UserformComponent implements OnInit {
  @Input()
  public userForm: FormGroup;

  @Output() submitUser: EventEmitter<User> = new EventEmitter();

  constructor() { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.userForm.controls[controlName].hasError(errorName);
  }


  ngOnInit(): void {
  }

  onSubmit() {
    if (this.userForm.invalid) {
      return;
    }
    this.submitUser.emit(this.userForm.value as User);
    console.log(this.userForm.value as User);
  }
}
