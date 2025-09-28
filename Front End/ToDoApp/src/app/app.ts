import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { HomePage } from "./Components/HomePage/home-page/home-page";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HomePage, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ToDoApp');

  constructo() {

  }
  currentPage: string = "";
  showLogin: boolean = true

}
