import { Component, Input, OnInit } from '@angular/core';
import { Enviroment } from '../../../environment';
import { AuthStateService } from '../../services/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { SharedModule } from '../shared/shared.module';
import { LoginComponent } from '../login/login.component';
import { LoginResponse } from '../../interfaces/interface';
import { Subscription } from 'rxjs';
import { ProfileAchivementsComponent } from "./profile-achivements/profile-achivements.component";
import { ProfileAwardsComponent } from './profile-awards/profile-awards.component';
import { ProfileUploadedJobComponent } from './profile-uploaded-job/profile-uploaded-job.component';
import { HeaderProfileComponent } from './header-profile/header-profile.component';
import { ActivatedRoute } from '@angular/router';

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

export class UserProfileComponent implements OnInit {
  userId: string = '';
  isLoggedIn: boolean = false;
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  // for login
  isLoginPopupVisible: boolean = false;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;
  tempAvatarPath = Enviroment.tempAvatarPath;

  tabs = ['Achivements', 'Awards', 'Uploaded job'];
  selectedTab = this.tabs[0]; // mặc định tab đầu tiên

  constructor(
    private authStateService: AuthStateService,
    private route: ActivatedRoute
  ) {
  }
  ngOnInit(): void {

    this.authSubscription = this.authStateService.isLoggedIn$.subscribe(
      (loggedIn) => {
        this.isLoggedIn = loggedIn;
      }
    );

    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe((user) => {
        this.currentUser = user;
        if (this.currentUser != null) {
          this.userId = user.userId;
          this.avatarUrl = this.avatarPath + this.currentUser.avatarUrl;
        }
      })
    );
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
