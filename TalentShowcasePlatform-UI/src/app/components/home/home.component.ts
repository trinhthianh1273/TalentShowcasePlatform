import { AfterViewInit, Component, HostListener, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { Subscription } from 'rxjs';
import { ClientLogger } from '../../services/client-logger.service';
import { SharedModule } from '../shared/shared.module';
import { VideoCardComponent } from '../video-card/video-card.component';
import { Router } from '@angular/router';
import { VideosService } from '../../services/videos.service';
import { LoginComponent } from "../login/login.component";
import { AuthService } from '../../services/auth.service';
import { LoginResponse } from '../../interfaces/interface';
import { Enviroment } from '../../../environment';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { AuthStateService } from '../../services/auth-state.service';
import { DataService } from '../../services/data.service';
import { SubjectService } from '../../services/subject.service';

@Component({
  selector: 'app-home',
  imports: [
    VideoCardComponent,
    SharedModule,
    LoginComponent,
    NavbarComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  private videoSubscription: Subscription | undefined; // Để quản lý việc unsubscribe
  isLoggedIn: boolean = false;
  videoList: any[] = []; // Khai báo biến videoList để lưu trữ danh sách video
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  page = 1;
  pageSize = 12;
  loading = false;

  // for login
  isLoginPopupVisible: boolean = false;

  constructor(
    private subjectService: SubjectService,
    private dataService: DataService,
    private videoService: VideosService,
    private authStateService: AuthStateService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadVideos();
    this.authSubscription = this.authStateService.isLoggedIn$.subscribe(
      (loggedIn) => {
        this.isLoggedIn = loggedIn;
      }
    );

    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe((user) => {
        this.currentUser = user;
      })
    );
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
    this.videoService.getVideos(this.page, this.pageSize).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.videoList.push(...res.data);
          this.page++;
          this.loading = false;
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
        // Xử lý lỗi tại đây, ví dụ: hiển thị thông báo cho người dùng
      },
      complete: () => {
        // console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });
  }

  getAllVideo() {
    this.dataService.getVideos().subscribe({
      next: (res: any) => {
        // Sử dụng optional chaining để tránh lỗi nếu res hoặc res.data là null/undefined
        if (res?.data) {
          this.subjectService.sendVideo(res.data);
          this.videoList = res.data;
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
        // Xử lý lỗi tại đây, ví dụ: hiển thị thông báo cho người dùng
      },
      complete: () => {
        // console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });
  }

  navigateToDetail(id: any) {
    console.log(id);
    this.router.navigate(['/video'], { queryParams: { id: id } });
  }

  handleLoginSuccess(userData: LoginResponse['data']): void {
    this.isLoggedIn = true;
    this.currentUser = userData; // Lưu trực tiếp dữ liệu người dùng
    // console.log('Thông tin người dùng sau đăng nhập:', this.currentUser);
    // Bây giờ bạn có thể sử dụng this.currentUser?.userId cho các chức năng khác
  }

}