import { Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { Enviroment } from '../../../environment';
import { AuthStateService } from '../../services/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { SharedModule } from '../shared/shared.module';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-header',
  imports: [
    SharedModule,
    LoginComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  userId: string = '';
  @Input() isLoggedIn!: boolean;
  @Input() currentUser!: any;
  // isLoggedIn: boolean = false;
  // currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu

  // for login
  isLoginPopupVisible: boolean = false;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;
  tempAvatarPath = Enviroment.tempAvatarPath;

  // profile popup
  popupOpen = false;
  @ViewChild('popupWrapper', { static: false }) popupWrapper!: ElementRef;

  constructor(
    private subjectService: SubjectService,
    private authStateService: AuthStateService
  ) {}
  ngOnInit(): void {
    this.userId = this.currentUser.userId;
    if(this.currentUser.avatarUrl) {
      this.avatarUrl = this.avatarPath + this.currentUser.avatarUrl;
    }
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
