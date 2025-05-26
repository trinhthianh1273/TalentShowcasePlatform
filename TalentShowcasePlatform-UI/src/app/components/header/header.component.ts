import { Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { Enviroment } from '../../../environment';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { SharedModule } from '../../shared/shared.module';
import { LoginComponent } from '../login/login.component';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { BaseComponent } from '../base-component/base-component.component';
import { Router } from '@angular/router';
import { AvatarDropdownComponent } from "../avatar-dropdown/avatar-dropdown.component";

@Component({
  selector: 'app-header',
  imports: [
    SharedModule,
    LoginComponent,
    AvatarDropdownComponent
],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent extends BaseComponent implements OnInit {
  // isLoggedIn: boolean = false;
  // currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu

  // for login
  isLoginPopupVisible: boolean = false;

  // profile popup
  popupOpen = false;
  @ViewChild('popupWrapper', { static: false }) popupWrapper!: ElementRef;

  constructor(
    private subjectService: SubjectService,
    authStateService: AuthStateService,
    private router: Router
  ) {
    super(authStateService);
  }
  ngOnInit(): void {
    this.subscribeAuthState();
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const targetElement = event.target as HTMLElement;

    if (this.popupWrapper && !this.popupWrapper.nativeElement.contains(targetElement)) {
      this.popupOpen = false;
    }
  }

  togglePopup() {
    this.popupOpen = !this.popupOpen;
  }

  logout(): void {
    this.authStateService.logout();
    this.router.navigate(['/']);
    window.location.reload(); // Hoặc phương pháp điều hướng khác}
  }

  showLoginPopup(): void {
    this.isLoginPopupVisible = true;
  }

  hideLoginPopup(): void {
    console.log("đóng popup");
    this.isLoginPopupVisible = false;
  }
}
