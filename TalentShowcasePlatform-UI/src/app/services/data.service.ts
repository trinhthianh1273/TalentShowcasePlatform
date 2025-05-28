import { inject, Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReceivedDataModel } from '../models/ReceivedDataMode';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  //private http = inject(HttpClient);
  private baseURL: string = Enviroment.baseURL;
  constructor(
    private http: HttpClient
  ) { }

  getCategories(): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Categories`);
  }

  getAchievementByUser(userId: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Achievements/get-by-user/${userId}`);
  }

  getVideos(): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos`);
  }

  getVideoById(id: string) : Observable<ReceivedDataModel>{
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos/${id}`);
  }

  getUserById(id: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Users/${id}`);
  }

  getCityFromApi(): Observable<any[]> {
    return this.http.get<any[]>('https://provinces.open-api.vn/api/p/');
  }
}
