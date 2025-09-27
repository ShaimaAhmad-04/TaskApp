import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Interfaces/iTask';

@Injectable({
  providedIn: 'root'
})
export class TasksServices {
  
  constructor(private _http : HttpClient){}
  
  getAll(){

    return this._http.get("https://localhost:44327/api/Task/GetALL")
  }
   
  add(task : Task){
    
    return this._http.post("https://localhost:44327/api/Task/Add",task)
  }

  update(task : Task){


    return this._http.put("https://localhost:44327/api/Task/Update",task)
  }

  delete(taskId :number){

    let params = new HttpParams ()

    params = params.set("Id",taskId)

    return this._http.delete("https://localhost:44327/api/Task/Delete",{params})

  }
}
