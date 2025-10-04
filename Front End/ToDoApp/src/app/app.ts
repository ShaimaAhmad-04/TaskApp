import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';


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


  isSignedIn(): boolean {
    return this._route.url !== '/login'
  }

  login(token: string) {
    localStorage.setItem("Token", token);
    this._route.navigate(["homePage"]);

  }

  signOut() {
    localStorage.clear();
    this._route.navigate(["login"]);
  }



}
