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

  sendAccount(data: any) {
    this.account$.next(data);
  }

  sendUser(data: any) {
    this.user$.next(data);
  }
  sendVideo(data: any) {
    this.video$.next(data);
  }
}
