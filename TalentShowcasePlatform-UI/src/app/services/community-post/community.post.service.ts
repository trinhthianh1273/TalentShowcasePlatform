import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommunityPostService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getPostByGroup(id: any) {
    return this.http.get<any>(`${this.baseURL}/api/GroupPosts/by-group/${id}`);
  }

  createPost(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/GroupPosts`, data);
  }

  getPostComments(id: any) {
    return this.http.get<any>(`${this.baseURL}/api/GroupPostComments/by-post/${id}`);
  }

  getPostLikes(id: any) { 
    return this.http.get<any>(`${this.baseURL}/api/LikeGroupPost/group-post/${id}`);
  }

  isLikePost(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/LikeGroupPost/check-like`, data);
  }

  sentCommentPost(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/GroupPostComments`, data);
  }

  sentLikePost(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/LikeGroupPost`, data);
  }
  
  deleteLikePost(Id: string) {
    return this.http.delete<any>(`${this.baseURL}/api/LikeGroupPost/${Id}`);
  }
}
