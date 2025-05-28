import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Enviroment } from '../../../environment';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { SharedModule } from '../../shared/shared.module';
import { LoginComponent } from '../login/login.component';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { BaseComponent } from '../base-component/base-component.component';
import { Router } from '@angular/router';
import { AvatarDropdownComponent } from "../avatar-dropdown/avatar-dropdown.component";
import { NotificationService } from '../../services/notifications/notification.service';

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

  // for login
  isLoginPopupVisible: boolean = false;

  // profile popup
  popupOpen = false;
  @ViewChild('popupWrapper', { static: false }) popupWrapper!: ElementRef;

  @Input() isSidebarOpen = true;
  @Output() onToggle = new EventEmitter<void>();

  isNotificationOpen = false;

  constructor(
    private subjectService: SubjectService,
    authStateService: AuthStateService,
    notiService: NotificationService,
    private router: Router
  ) {
    super(authStateService, notiService);
  }
  ngOnInit(): void {
    this.subscribeAuthState();
  }
  toggle() {
    this.onToggle.emit();
  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const targetElement = event.target as HTMLElement;

    if (this.popupWrapper && !this.popupWrapper.nativeElement.contains(targetElement)) {
      this.popupOpen = false;
    }

    // Nếu click ngoài vùng dropdown và icon
    if (!targetElement.closest('.notification-dropdown') && !targetElement.closest('.notification-btn')) {
      this.isNotificationOpen = false;
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

  toggleNotification() {
    this.isNotificationOpen = !this.isNotificationOpen;
  }

  closeNotification() {
    this.isNotificationOpen = false;
  }

  hideLoginPopup(): void {
    console.log("đóng popup");
    this.isLoginPopupVisible = false;
  }
}
