import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from './../../Services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
    constructor(private AuthService: AuthenticationService) {
    }
    ngOnInit(): void {
    }

    get IsLoggedIn(): boolean {
      return this.AuthService.isLogged;
    }

    trigLogout(){
      this.AuthService.Logout();
    }
}
