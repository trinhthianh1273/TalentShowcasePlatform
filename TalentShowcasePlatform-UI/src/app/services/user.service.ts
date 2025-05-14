import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { UserData } from '../interfaces/interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

   getUserById(id: any) { 
      return this.http.get<UserData>(`${this.baseURL}/api/Users/${id}`);  
    }

  updateProfile(userId: any, data: any) {
    return this.http.put<any>(`${this.baseURL}/api/Users/${userId}`, data);
  }

  changeAvatar(userId: any, data: any) {
    return this.http.post<any>(`${this.baseURL}/api/Users/Change-avatar/${userId}`, data);
  }
}
