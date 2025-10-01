import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServices {

  constructor(private _http: HttpClient) { }

  Login(loginForm: any) {

    return this._http.post("https://localhost:44327/api/Auth/LogIn", loginForm)
  }

  SignUp(signUpDTO: any) {

    return this._http.post("https://localhost:44327/api/Auth/SignUp", signUpDTO)
  }

}
