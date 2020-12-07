import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Client } from 'src/app/models/client';

@Component({
  selector: 'app-clientform',
  templateUrl: './clientform.component.html',
  styleUrls: ['./clientform.component.css']
})
export class ClientformComponent implements OnInit {
  @Input()
  public clientForm: FormGroup;

  @Output() submitClient: EventEmitter<Client> = new EventEmitter();

  constructor() { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.clientForm.controls[controlName].hasError(errorName);
  }


  ngOnInit(): void {
  }

  onSubmit() {
    this.submitClient.emit(this.clientForm.value as Client);
    console.log(this.clientForm.value as Client);
  }
}
