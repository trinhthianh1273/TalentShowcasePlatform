import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';
import { CreateGroupRequest } from '../../components/community/create-community/CreateGroupRequest';
import { Observable } from 'rxjs';
import { ReceivedDataModel } from '../../models/ReceivedDataMode';

@Injectable({
  providedIn: 'root'
})
export class CommunityService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getCommunities(): Observable<ReceivedDataModel> { 
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Groups`);  
  }

  getCommunity(id: string): Observable<ReceivedDataModel> { 
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Groups/${id}`);
  }

  createCommunity(data: any): Observable<ReceivedDataModel> { 
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/Groups`, data);
    }

  getYourCommunities(userId: string) : Observable<ReceivedDataModel>{
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Groups/createdby/${userId}`);
  }

   getJoinedCommunities(userId: string): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/Groups/joined-group/${userId}`);
  }

  getJoinedCommunitiesId(userId: string) : Observable<ReceivedDataModel>{
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/GroupMembers/joined-group/${userId}`);
  }

  getCommunityPost(): Observable<ReceivedDataModel> {
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/GroupPosts`);
  }

  getCommunityPostDetail(id: string) : Observable<ReceivedDataModel>{
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/GroupPosts/${id}`);
  }

  getCommunityPostComments(id: string) : Observable<ReceivedDataModel>{
    return this.http.get<ReceivedDataModel>(`${this.baseURL}/api/GroupPostComments/by-post/${id}`);
  }

  addUserToCommunity(groupId: any, userId: any ) : Observable<ReceivedDataModel>{
    return this.http.post<ReceivedDataModel>(`${this.baseURL}/api/GroupMembers`, {
      groupId: groupId,
      userId: userId
    });
  }

  cancelUserToCommunity(groupId: string, userId: string ) : Observable<ReceivedDataModel>{
    return this.http.delete<ReceivedDataModel>(`${this.baseURL}/api/GroupMembers/group/${groupId}/user/${userId}`);
  }
}
