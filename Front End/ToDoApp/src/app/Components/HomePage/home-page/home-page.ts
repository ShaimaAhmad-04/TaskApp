import { Component } from '@angular/core';

@Component({
  selector: 'app-home-page',
  imports: [],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css'
})
export class HomePage {

   Tasks = []

   openModal(id:number){
    //find the task based on its id 
   }

}
