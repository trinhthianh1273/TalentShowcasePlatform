import { Component, Input, OnInit } from '@angular/core';
import { LoginResponse } from '../../interfaces/interface';
import { Subscription } from 'rxjs';
import { Enviroment } from '../../../environment';
import { VideosService } from '../../services/videos.service';
import { AuthStateService } from '../../services/auth-state.service';
import { Router } from 'express';
import { DataService } from '../../services/data.service';
import { SubjectService } from '../../services/subject.service';
import { SharedModule } from '../shared/shared.module';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-navbar',
  imports: [
    SharedModule,
    LoginComponent
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  @Input() isLoggedIn!: boolean;
  @Input() currentUser!: any;
  // isLoggedIn: boolean = false;
  // currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu

  // for login
  isLoginPopupVisible: boolean = false;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;
  tempAvatarPath = Enviroment.tempAvatarPath;

  constructor(
    private subjectService: SubjectService,
    private authStateService: AuthStateService
  ) {}
  ngOnInit(): void {
    if(this.currentUser.avatarUrl) {
      this.avatarUrl = this.avatarPath + this.currentUser.avatarUrl;
    }
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
