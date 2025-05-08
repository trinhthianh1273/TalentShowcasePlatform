// auth-state.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private currentUserSubject = new BehaviorSubject<any>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    this.loadInitialState();
  }

  private loadInitialState(): void {
    if (typeof localStorage !== 'undefined') {
      const token = localStorage.getItem('authToken');
      const user = localStorage.getItem('currentUser');
      this.isLoggedInSubject.next(!!token && !!user);
      this.currentUserSubject.next(user ? JSON.parse(user) : null);
    } else {
      // Xử lý trường hợp không có localStorage (ví dụ: SSR)
      this.isLoggedInSubject.next(false);
      this.currentUserSubject.next(null);
    }
  }

  setLoggedIn(loggedIn: boolean): void {
    this.isLoggedInSubject.next(loggedIn);
  }

  setCurrentUser(user: any): void {
    this.currentUserSubject.next(user);
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    this.isLoggedInSubject.next(false);
    this.currentUserSubject.next(null);
  }
}