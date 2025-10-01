import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Interfaces/iUser';
@Injectable({
  providedIn: 'root'
})
export class UserServices {
  constructor(private _http:HttpClient){}

  forgotPassword(user:any){

    return this._http.put("https://localhost:44327/api/Users/forgotPassword",user)
  }

  getInfo(){
    return this._http.get("https://localhost:44327/api/Users/GetUserInfo")
  }

  updateInfo(user : User){

    return this._http.put("https://localhost:44327/api/Users/UpdateUserInfo",user)
  }

  resetPassword(forgotDTO:any){

    return this._http.put("https://localhost:44327/api/Users/resetPassword",forgotDTO)
  }
}
