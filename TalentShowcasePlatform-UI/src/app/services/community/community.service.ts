import { Injectable } from '@angular/core';
import { Enviroment } from '../../../environment';
import { HttpClient } from '@angular/common/http';
import { CreateGroupRequest } from '../../components/community/create-community/CreateGroupRequest';

@Injectable({
  providedIn: 'root'
})
export class CommunityService {
  private baseURL: string = Enviroment.baseURL;
  constructor(private http: HttpClient) { }

  getCommunities() { 
    return this.http.get<any>(`${this.baseURL}/api/Groups`);  
  }

  getCommunity(id: any) { 
    return this.http.get<any>(`${this.baseURL}/api/Groups/${id}`);
  }

  createCommunity(data: any) { 
    return this.http.post<any>(`${this.baseURL}/api/Groups`, data);
    }

  getYourCommunities(userId: any) {
    return this.http.get<any>(`${this.baseURL}/api/Groups/createdby/${userId}`);
  }

   getJoinedCommunities(userId: any) {
    return this.http.get<any>(`${this.baseURL}/api/Groups/joined-group/${userId}`);
  }

  getJoinedCommunitiesId(userId: any) {
    return this.http.get<any>(`${this.baseURL}/api/GroupMembers/joined-group/${userId}`);
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

  addUserToCommunity(groupId: any, userId: any ) {
    return this.http.post<any>(`${this.baseURL}/api/GroupMembers`, {
      groupId: groupId,
      userId: userId
    });
  }

  cancelUserToCommunity(groupId: any, userId: any ) {
    return this.http.delete<any>(`${this.baseURL}/api/GroupMembers/group/${groupId}/user/${userId}`);
  }
}
