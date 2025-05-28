import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';
import { ReceivedDataModel } from '../../models/ReceivedDataMode';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL:string = Enviroment.baseURL;
  constructor(
    private http: HttpClient
  ) { }

  authenticationLogin(value: any): Observable<ReceivedDataModel> { 
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/auth/login`, value);  
  }
}
