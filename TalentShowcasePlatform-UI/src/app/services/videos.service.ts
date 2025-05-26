import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { UserData, VideoData } from '../interfaces/interface';

@Injectable({
  providedIn: 'root'
})
export class VideosService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }
  getVideos() {
    return this.http.get<any>(`${this.baseURL}/api/Videos`);
  }

  getVideoById(id: any) {
    return this.http.get<VideoData>(`${this.baseURL}/api/Videos/${id}`);
  }

  getUserById(id: any) {
    return this.http.get<UserData>(`${this.baseURL}/api/Users/${id}`);
  }
  
  getVideosPage(page: number, size: number) {
    const params = new HttpParams()
      .set('PageNumber', page.toString())
      .set('PageSize', size.toString());

    return this.http.get<any>(`${this.baseURL}/api/Videos/page`, { params });
  }

  getVideoByUser(userId: any) {
    return this.http.get<any>(`${this.baseURL}/api/Videos/User/${userId}`);
  }

  getCommentsVideo(videoId: string) {
    return this.http.get<any>(`${this.baseURL}/api/CommentVideos/video/${videoId}`);
  }

  getLikeVideo(videoId: string) {
    return this.http.get<any>(`${this.baseURL}/api/VideoLikes/video/${videoId}`);
  }

  postComment(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/CommentVideos`, data);
  }

  updateVideo(videoId: any, data: any) {
    return this.http.put<any>(`${this.baseURL}/api/Videos/${videoId}`, data);
  }

  uploadVideo(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/Videos`, data);
  }

  deleteVideo(videoId: any) {
    return this.http.delete<any>(`${this.baseURL}/api/Videos/${videoId}`, videoId);
  }

  addView(data: any) { 
    return this.http.post<any>(`${this.baseURL}/api/Views`, data);
  }
}
