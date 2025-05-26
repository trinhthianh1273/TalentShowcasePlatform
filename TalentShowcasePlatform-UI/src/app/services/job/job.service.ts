import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReceivedDataModel } from '../../models/ReceivedDataMode';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getAllJob(): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Jobs`);
  }

  getJobById(id: any): Observable<ReceivedDataModel>  {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Jobs/${id}`);
  }

  getJobByUserId(id: any): Observable<ReceivedDataModel>  {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Jobs/get-by-user/${id}`);
  }

  createJob(data: any) {
    return this.http.post<any>(`${this.baseURL}/api/Jobs`, data);
  }

  updateJob(id: string, data: any) {
    return this.http.put<any>(`${this.baseURL}/api/Jobs/${id}`, data);
  }

  deleteJob(id: any) {
    return this.http.delete<any>(`${this.baseURL}/api/Jobs/${id}`);
  }
}
