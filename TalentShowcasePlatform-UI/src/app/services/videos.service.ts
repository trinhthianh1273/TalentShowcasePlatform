import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';
import { Observable } from 'rxjs';
import { ReceivedDataModel } from '../models/ReceivedDataMode';

@Injectable({
  providedIn: 'root'
})
export class VideosService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }
  getVideos() : Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos`);
  }

  getVideoById(id: any) : Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos/${id}`);
  }

  checkVideoLiked(videoId: any, userId: any) : Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/VideoLikes/check-like/${videoId}/${userId}`);
  }

  getUserById(id: any) : Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Users/${id}`);
  }
  
  
  getVideosPage(page: number, size: number): Observable<ReceivedDataModel> {
    const params = new HttpParams()
      .set('PageNumber', page.toString())
      .set('PageSize', size.toString());

    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos/page`, { params });
  }

  getVideoByUser(userId: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Videos/User/${userId}`);
  }

  getCommentsVideo(videoId: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/CommentVideos/video/${videoId}`);
  }

  getLikeVideo(videoId: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/VideoLikes/video/${videoId}`);
  }

  deleteVideoLike(id: any): Observable<ReceivedDataModel> {
    return this.http.delete<ReceivedDataModel>(`${this.baseURL}/api/VideoLikes/${id}`);
  }

  addVideoLike(data: any): Observable<ReceivedDataModel> {
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/VideoLikes`, data);
  }

  postComment(data: any): Observable<ReceivedDataModel> {
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/CommentVideos`, data);
  }

  updateVideo(videoId: string, data: any) : Observable<ReceivedDataModel>{
    return this.http.put<ReceivedDataModel>(`${this.baseURL}/api/Videos/${videoId}`, data);
  }

  uploadVideo(data: any): Observable<ReceivedDataModel> {
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/Videos`, data);
  }

  deleteVideo(videoId: any) {
    return this.http.delete<any>(`${this.baseURL}/api/Videos/${videoId}`, videoId);
  }

  addView(data: any): Observable<ReceivedDataModel> { 
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/Views`, data);
  }
}
