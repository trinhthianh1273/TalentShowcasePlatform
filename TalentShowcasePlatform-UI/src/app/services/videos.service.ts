import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Enviroment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class VideosService {
private baseURL:string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getVideos(page: number, size: number) {
    const params = new HttpParams()
      .set('PageNumber', page.toString())
      .set('PageSize', size.toString());

    return this.http.get<any>(`${this.baseURL}/api/Videos/page`, { params });
  }

  getCommentsVideo(videoId: string) { 
    return this.http.get<any>(`${this.baseURL}/api/Comments/video/${videoId}`);
  }

  postComment(data: any) { 
    return this.http.post<any>(`${this.baseURL}/api/Comments`, data);
  }
}
