import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommunityService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getCommunities() { 
    return this.http.get<any>(`${this.baseURL}/api/Groups`);  
  }

  getCommunityPost() {
    return this.http.get<any>(`${this.baseURL}/api/GroupPosts`);
  }

  getCommunityPostDetail(id: any) {
    return this.http.get<any>(`${this.baseURL}/api/GroupPosts/${id}`);
  }

  getCommunityPostComments(id: any) {
    return this.http.get<any>(`${this.baseURL}/api/GroupPostComments/by-post/${id}`);
  }
}
