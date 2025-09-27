import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Interfaces/iTask';

@Injectable({
  providedIn: 'root'
})
export class LookUpServices {

  constructor(private _http: HttpClient) { }

  getPrios(majorCode: number) {

    let params = new HttpParams();
    params = params.set("MajorCode", majorCode)

    return this._http.get("https://localhost:44327/api/LookUps/getPriorities", { params })
  }
}
