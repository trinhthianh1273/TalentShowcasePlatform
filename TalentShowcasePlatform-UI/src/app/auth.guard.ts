import { isPlatformBrowser } from '@angular/common';
import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private platformId = inject(PLATFORM_ID);
  constructor(private router: Router) { }

  canActivate(): boolean {
    let isLoggedIn = false;
    // const isLoggedIn = !!localStorage.getItem('authToken'); // hoặc gọi AuthService
    if (isPlatformBrowser(this.platformId)) {
      isLoggedIn = !!localStorage.getItem('authToken'); // hoặc gọi AuthService
    } else {
      console.warn('localStorage is not available (SSR). Authentication check might need a different approach.');
      // Trong môi trường server-side, bạn có thể cần kiểm tra authentication bằng cookie, session, hoặc một phương pháp khác.
      // Tùy thuộc vào logic ứng dụng của bạn, bạn có thể trả về true hoặc false ở đây.
      // Ví dụ: có thể coi như đã đăng nhập để render một phần của trang.
      isLoggedIn = true; // Hoặc false tùy theo yêu cầu SSR của bạn
    }

    if (isLoggedIn) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
