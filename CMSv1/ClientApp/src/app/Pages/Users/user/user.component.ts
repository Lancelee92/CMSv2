import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @ViewChild('userForm', { static: false }) userForm: NgForm;
  @ViewChild('DeleteSelection', { static: false }) DeleteSelection: ElementRef;

  username: string;
  password: string;
  verifyPassword: string;
  passwordInput: string;
  verifyPasswordInput: string;
  isLoading: boolean = false;

  constructor() { }

  onSubmit() {

    //if (this.userForm.valid) {
    //  const newUser = new User();
    //  newUser.Name = [];
    //  newUser.Name[0] = this.userForm.value.username;
    //  newUser.Password = this.userForm.value.passwords.password;
    //  this.isLoading = true;

    //  this.userService.createUserHttp(newUser, this.userForm);
    //  this.isLoading = false;

    //}
  }

  deleteUser(id) {
    //this.userService.deleteUser(id);
  }

  ngOnInit() {
    //this.userService.getUserList();
    const strUser = localStorage.getItem('currentUser');
    if (strUser !== null) {
      const jUser = JSON.parse(strUser);
      this.username = jUser.name;
    }
  }

}
