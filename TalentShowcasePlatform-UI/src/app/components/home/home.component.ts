import { AfterViewInit, Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { SendDataService } from '../../services/send.data.service';
import { GetDataService } from '../../services/get.data.service';
import { Subscription } from 'rxjs';
import { ClientLogger } from '../../services/client-logger.service';
import { SharedModule } from '../shared/shared.module';
import { VideoCardComponent } from '../video-card/video-card.component';

@Component({
  selector: 'app-home',
  imports: [
    VideoCardComponent,
    SharedModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  private videoSubscription: Subscription | undefined; // Để quản lý việc unsubscribe
  isLogin : boolean = true;
  videoList: Array<any> = []; // Khai báo biến videoList để lưu trữ danh sách video
  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private SDService: SendDataService,
    private GDService: GetDataService,
    private logger: ClientLogger
  ) { }

  ngOnInit() {
    
    console.log("home page");
    this.getAllVideo();
  }


  getAllVideo() {
    this.videoSubscription = this.GDService.getVideos().subscribe({
      next: (res: any) => {
        console.log("Video data: ", res?.data); // Sử dụng optional chaining để tránh lỗi nếu res hoặc res.data là null/undefined
        if (res?.data) {
          this.SDService.sendVideo(res.data);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err);
        // Xử lý lỗi tại đây, ví dụ: hiển thị thông báo cho người dùng
      },
      complete: () => {
        console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });
  }
}
