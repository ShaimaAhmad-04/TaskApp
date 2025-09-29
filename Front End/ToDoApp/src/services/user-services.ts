import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserServices {
  constructor(private _http:HttpClient){}

  reset(user:any){

    return this._http.put("https://localhost:44327/api/Users/forgotPassword",user)
  }
}
