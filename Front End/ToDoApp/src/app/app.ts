import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { HomePage } from "./Components/HomePage/home-page/home-page";
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink,],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(private _route: Router) {

  }

  isSignedIn = signal(localStorage.getItem("Token") ? true : false)

  login(token: string) {
    localStorage.setItem("Token", token);
    console.log(this.isSignedIn.set(true))// reactive update
  }

  signOut() {
    localStorage.clear();
    this.isSignedIn.set(false);
    this._route.navigate(["login"]);
  }
}
