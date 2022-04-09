import { Component, OnInit } from '@angular/core';
import * as CryptoJS from 'crypto-js';  

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  title: string = 'EncryptionDecryptionSample';
  password: string = '';

  constructor() {

  }

  ngOnInit() {

    const encodedWord = CryptoJS.enc.Utf8.parse("admin@1234"); // encodedWord Array object
    this.password = CryptoJS.enc.Base64.stringify(encodedWord);
    //this.password = CryptoJS.enc.Base64.stringify("admin@1234");
    //this.password = CryptoJS.SHA256("admin@1234").toString(CryptoJS.enc.Base64);
    //this.password = CryptoJS.AES.encrypt("admin@1234", "jA5ng497ST10CKLZKFxfu7NyYGXvnCNt").toString();
  }
}
