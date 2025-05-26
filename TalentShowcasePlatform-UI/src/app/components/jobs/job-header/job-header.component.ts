import { Component, HostListener, OnInit } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { BaseComponent } from '../../base-component/base-component.component';
import { Router } from '@angular/router';
import { AvatarDropdownComponent } from "../../avatar-dropdown/avatar-dropdown.component";

@Component({
  selector: 'app-job-header',
  imports: [
    SharedModule,
    AvatarDropdownComponent
],
  templateUrl: './job-header.component.html',
  styleUrl: './job-header.component.css'
})
export class JobHeaderComponent extends BaseComponent implements OnInit {
  // Giá trị opacity và scale cho ảnh
  bgOpacity = 1;
  bgScale = 1;

  // profile popup
  popupOpen = false;

  constructor(
    authStateService: AuthStateService,
    private router: Router
  ) {
    super(authStateService);
  }
  ngOnInit(): void {
    this.subscribeAuthState();
  }

  togglePopup() {
    this.popupOpen = !this.popupOpen;
  }

  logout(): void {
    this.authStateService.logout();
    this.router.navigate(['/']);
    window.location.reload(); // Hoặc phương pháp điều hướng khác}
  }

  // Khi scroll, tính lại opacity & scale dựa trên scrollY
  @HostListener('window:scroll', [])
  onWindowScroll() {
    console.log("test scroll");
    const scrollY = window.scrollY;
    // Mờ dần từ full tới 0 khi scroll 0 → 300px (tùy chỉnh)
    this.bgOpacity = Math.max(0, 1 - scrollY / 300);
    // Zoom nhẹ max +10% tại 300px
    this.bgScale = 1 + Math.min(0.1, scrollY / 3000);
  }
}
