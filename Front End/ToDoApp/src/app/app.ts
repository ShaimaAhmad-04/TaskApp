import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { HomePage } from "./Components/HomePage/home-page/home-page";
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink,],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(private _route:Router){

  }

  isSignedIn = localStorage.getItem("Token")? true : false;

  login(token: string) {
    localStorage.setItem("Token", token);
    this.isSignedIn = true // reactive update
  }

  signOut() {
    localStorage.clear();
    this.isSignedIn=false;
    this._route.navigate(["login"]);
  }
}
