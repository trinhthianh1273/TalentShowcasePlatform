import { inject, Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { UserData } from '../interfaces/interface';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  //private http = inject(HttpClient);
  private baseURL:string = Enviroment.baseURL;
  constructor(
    private http: HttpClient
  ) { }

  getCategories() { 
    return this.http.get<any>(`${this.baseURL}/api/Categories`);  
  }

  getAchievementByUser(userId: any) { 
    return this.http.get<any>(`${this.baseURL}/api/Achievements/get-by-user/${userId}`);  
  }

  getVideos() { 
    return this.http.get<any>(`${this.baseURL}/api/Videos`);  
  }

  getVideoById(id: any) { 
    return this.http.get<any>(`${this.baseURL}/api/Videos/${id}`);  
  }

  getUserById(id: any) { 
    return this.http.get<UserData>(`${this.baseURL}/api/Users/${id}`);  
  }
}
