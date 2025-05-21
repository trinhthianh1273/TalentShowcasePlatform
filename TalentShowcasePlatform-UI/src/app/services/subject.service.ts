import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
constructor() { }

  private account$ = new BehaviorSubject<any>({});
  receivedAccount$ = this.account$.asObservable();

  private user$ = new BehaviorSubject<any>({});
  receivedUser$ = this.user$.asObservable();

  private video$ = new BehaviorSubject<any>({});
  receivedVideo$ = this.video$.asObservable();

  private categiry$ = new BehaviorSubject<any>({});
  receivedCategory$ = this.categiry$.asObservable();

  private community$ = new BehaviorSubject<any>({});
  receivedCommunity$ = this.community$.asObservable();

  private yourCommunity$ = new BehaviorSubject<any>({});
  receivedYourCommunity$ = this.yourCommunity$.asObservable();

  private joinedCommunity$ = new BehaviorSubject<any>({});
  receivedJoinedCommunity$ = this.joinedCommunity$.asObservable();

  private joinedCommunitiesId$ = new BehaviorSubject<any>({});
  receivedJoinedCommunitiesId$ = this.joinedCommunitiesId$.asObservable();

  private communityPost$ = new BehaviorSubject<any>({});
  receivedCommunityPost$ = this.communityPost$.asObservable();

  sendAccount(data: any) {
    this.account$.next(data);
  }

  sendUser(data: any) {
    this.user$.next(data);
  }
  sendVideo(data: any) {
    this.video$.next(data);
  }

  sendCategory(data: any) {
    this.categiry$.next(data);
  }

  sendCommunity(data: any) {
    this.community$.next(data);
  }

  sendYourCommunity(data: any) {
    this.yourCommunity$.next(data);
  }

  sendJoinedCommunity(data: any) {
    this.joinedCommunity$.next(data);
  }

  sendJoinedCommunitiesId(data: any) {
    this.joinedCommunitiesId$.next(data);
  }

  sendCommunityPost(data: any) {
    this.communityPost$.next(data);
  }
}
