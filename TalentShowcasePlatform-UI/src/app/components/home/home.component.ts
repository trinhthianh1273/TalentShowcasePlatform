import { AfterViewInit, Component, HostListener, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { Subscription } from 'rxjs';
import { SharedModule } from '../../shared/shared.module';
import { VideoCardComponent } from '../video-card/video-card.component';
import { Router } from '@angular/router';
import { VideosService } from '../../services/videos.service';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { HeaderComponent } from '../header/header.component';
import { AsideLeftComponent } from "../aside-left/aside-left.component";
import { BaseComponent } from '../base-component/base-component.component';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { NotificationService } from '../../services/notifications/notification.service';

@Component({
  selector: 'app-home',
  imports: [
    VideoCardComponent,
    SharedModule,
    HeaderComponent,
    AsideLeftComponent,
    AsideLeftComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent extends BaseComponent implements OnInit {
  videoList: any[] = []; // Khai báo biến videoList để lưu trữ danh sách video

  page = 1;
  pageSize = 12;
  loading = false;

  // for login
  isLoginPopupVisible: boolean = false;
  isSidebarOpen = true;
  constructor(
    private subjectService: SubjectService,
    private videoService: VideosService,
    private router: Router,
    authStateService: AuthStateService,
    notiService: NotificationService
  ) {
    super(authStateService, notiService);
  }

  ngOnInit() {
    this.loadVideos();
    this.subscribeAuthState();
    console.log("user home: ", this.currentUser);
  }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  ngOnDestroy(): void {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  @HostListener('window:scroll', [])
  onScroll(): void {
    console.log('SCROLL DETECTED');
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 300 && !this.loading) {
      this.loadVideos();
    }
  }

  loadVideos(): void {
    console.log('LOADING VIDEOS page', this.page);
    this.loading = true;
    this.videoService.getVideosPage(this.page, this.pageSize).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.videoList.push(...res.data);
          this.page++;
          this.loading = false;
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
      }
    });
  }

  getAllVideo() {
    this.videoService.getVideos().subscribe({
      next: (res: any) => {
        // Sử dụng optional chaining để tránh lỗi nếu res hoặc res.data là null/undefined
        if (res?.data) {
          this.subjectService.sendVideo(res.data);
          this.videoList = res.data;
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
      }
    });
  }

  navigateToDetail(id: any) {
    console.log(id);
    this.router.navigate(['/video', id]);
  }


  handleLoginSuccess(userData: CurrentUserModel): void {
    this.isLoggedIn = true;
    this.currentUser = userData; // Lưu trực tiếp dữ liệu người dùng
  }


}