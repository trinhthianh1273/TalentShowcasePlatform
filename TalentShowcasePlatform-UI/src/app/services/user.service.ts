import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { ReceivedDataModel } from '../models/ReceivedDataMode';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

   getUserById(id: string): Observable<ReceivedDataModel> { 
      return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Users/${id}`);  
    }

  updateProfile(userId: string, data: any): Observable<ReceivedDataModel> {
    return this.http.put<ReceivedDataModel>(`${this.baseURL}/api/Users/${userId}`, data);
  }

  changeAvatar(userId: string, data: any): Observable<ReceivedDataModel> {
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/Users/Change-avatar/${userId}`, data);
  }
}
