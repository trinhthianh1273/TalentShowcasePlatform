import { AfterViewInit, Component, HostListener, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { SendDataService } from '../../services/send.data.service';
import { GetDataService } from '../../services/get.data.service';
import { Subscription } from 'rxjs';
import { ClientLogger } from '../../services/client-logger.service';
import { SharedModule } from '../shared/shared.module';
import { VideoCardComponent } from '../video-card/video-card.component';
import { Router } from '@angular/router';
import { VideosService } from '../../services/videos.service';
import { LoginComponent } from "../login/login.component";

@Component({
  selector: 'app-home',
  imports: [
    VideoCardComponent,
    SharedModule,
    LoginComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  private videoSubscription: Subscription | undefined; // Để quản lý việc unsubscribe
  isLogin: boolean = false;
  videoList: any[] = []; // Khai báo biến videoList để lưu trữ danh sách video

  page = 1;
  pageSize = 12;
  loading = false;

  // for login
  isLoginPopupVisible: boolean = false;

  constructor(
    private SDService: SendDataService,
    private GDService: GetDataService,
    private videoService: VideosService,
    private logger: ClientLogger,
    private router: Router
  ) { }

  ngOnInit() {
    console.log("home page");
    // this.getAllVideo();
    this.loadVideos();
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
        console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });
    console.log("video: ", this.videoList);
  }

  getAllVideo() {
    this.GDService.getVideos().subscribe({
      next: (res: any) => {
        // Sử dụng optional chaining để tránh lỗi nếu res hoặc res.data là null/undefined
        if (res?.data) {
          this.SDService.sendVideo(res.data);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
        // Xử lý lỗi tại đây, ví dụ: hiển thị thông báo cho người dùng
      },
      complete: () => {
        console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });
  }

  navigateToDetail(id: any) {
    console.log(id);
    this.router.navigate(['/video'], { queryParams: { id: id } });
  }

  showLoginPopup():void {
    console.log("đăng nhập");
    this.isLoginPopupVisible = true;
  }

  hideLoginPopup(): void{
    console.log("đóng popup");
    this.isLoginPopupVisible = false;
  }
}