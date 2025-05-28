import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReceivedDataModel } from '../../models/ReceivedDataMode';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) {}

  getNotificationByUser(id: string) : Observable<ReceivedDataModel>  {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Notifications/user/${id}`);
  }

  getVideoNotifications() : Observable<ReceivedDataModel>  {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Notifications`);
  }
}
