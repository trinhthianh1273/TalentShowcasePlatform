import { Component, Input, OnInit } from '@angular/core';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { SharedModule } from '../../shared/shared.module';
import { ProfileAchivementsComponent } from "./profile-achivements/profile-achivements.component";
import { ProfileAwardsComponent } from './profile-awards/profile-awards.component';
import { ProfileUploadedJobComponent } from './profile-uploaded-job/profile-uploaded-job.component';
import { HeaderProfileComponent } from './header-profile/header-profile.component';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../base-component/base-component.component';
import { NotificationService } from '../../services/notifications/notification.service';

@Component({
  selector: 'app-user-profile',
  imports: [
    SharedModule,
    ProfileAchivementsComponent,
    ProfileAwardsComponent,
    ProfileUploadedJobComponent,
    HeaderProfileComponent
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})

export class UserProfileComponent extends BaseComponent implements OnInit {
  // for login
  isLoginPopupVisible: boolean = false;

  tabs = ['Achivements', 'Awards', 'Uploaded job'];
  selectedTab = this.tabs[0]; // mặc định tab đầu tiên

  constructor(
    authStateService: AuthStateService,
    notiService: NotificationService,
    private route: ActivatedRoute
  ) {
    super(authStateService, notiService);
  }
  ngOnInit(): void {

    this.subscribeAuthState();
    console.log("user: ", this.currentUser);
  }


  selectTab(tab: string) {
    this.selectedTab = tab;
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
