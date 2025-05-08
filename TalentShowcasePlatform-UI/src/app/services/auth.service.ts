import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { LoginResponse } from '../interfaces/interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL:string = Enviroment.baseURL;
  constructor(
    private http: HttpClient
  ) { }

  authenticationLogin(value: any) { 
    return this.http.post<LoginResponse>(`${this.baseURL}/api/auth/login`, value);  
  }
}
