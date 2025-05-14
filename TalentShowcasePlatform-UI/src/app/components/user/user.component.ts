import { Component, ElementRef, Input, input, OnInit, ViewChild } from '@angular/core';
import { LoginResponse } from '../../interfaces/interface';
import { Subscription, take } from 'rxjs';
import { HeaderComponent } from '../header/header.component';
import { SharedModule } from '../shared/shared.module';
import { AuthStateService } from '../../services/auth-state.service';
import { SubjectService } from '../../services/subject.service';
import { AsideLeftComponent } from "../aside-left/aside-left.component";
import { VideosService } from '../../services/videos.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Route, Router, RouterLinkActive } from '@angular/router';
import { CompletingPopupComponent } from "../popup/completing-popup/completing-popup.component";

@Component({
  selector: 'app-user',
  imports: [
    HeaderComponent,
    SharedModule,
    AsideLeftComponent,
    CompletingPopupComponent
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  userId: any;

  videoList: any[] = []; // Khai báo biến videoList để lưu trữ danh sách video
  videoPath = 'https://localhost:7172/api/Videos/video-path/';

  isLoggedIn: boolean = false;
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  isOpenDropdownAnalysisVideo: boolean = false;
  dropdownVideoId: any;

  isCompletingPopup: boolean = false;
  handingVideo: boolean = false;
  uploadSuccess: boolean = false;
  uploadedVideoId: any;

  constructor(
    private authStateService: AuthStateService,
    private subjectService: SubjectService,
    private videoService: VideosService,
    private toastr: ToastrService,
    private route: Router,
    private routeActive: ActivatedRoute
  ) {

  }

  ngOnInit(): void {
    this.userId = this.routeActive.snapshot.queryParamMap.get('id');
    this.loadVideos(this.userId);

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
    console.log("user page: ", this.currentUser);
  }


  triggerFileInput() {
    this.fileInput.nativeElement.click();
  }
  onVideoSelected(event: any) {
    this.handingVideo = true;
    const file: File = event.target.files[0];

    if (file) {
      const formData = new FormData();
      formData.append('file', file);
      formData.append('title', file.name);
      formData.append('description', '');
      formData.append('url', '');
      formData.append('userId', this.userId);
      formData.append('categoryId', '');
      formData.append('isPrivate', 'false');

      console.log('FormData values:');
      formData.forEach((value, key) => {
        console.log(`${key}:`, value);
      });

      this.videoService.uploadVideo(formData)
        .subscribe({
          next: (res) => {
            this.uploadedVideoId = res.data;
            console.log("videoId: ", res.data);
            this.handingVideo = false;
            this.uploadSuccess = true;
            this.loadVideos(this.userId);
          },
          error: (err) => {
            this.handingVideo = false;
            this.toastr.error('Đã xảy ra lỗi');
            console.error(err);
          }
        });
    } else {
      this.toastr.error('Chưa chọn video');
    }
  }

  deleteVideo(video: any) {
    console.log("xóa video: ", video.id);
    this.videoService.deleteVideo(video.id).subscribe({
      next: (res) => {
        this.toastr.success('Xóa video thành công');
        this.loadVideos(this.userId);
      },
      error: (err) => {
        this.handingVideo = false;
        this.toastr.error('Đã xảy ra lỗi');
        console.error(err);
      }
    });
  }

  goToVideoConfig(uploadedVideoId: any) {
    this.route.navigate(['/video-analysis'], {
      queryParams: { id: uploadedVideoId }
    });
    this.uploadSuccess = false;
  }

  loadVideos(userId: any): void {
    this.videoService.getVideoByUser(userId).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.videoList = res.data;
          console.log("Video user: ", this.videoList);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
      },
    });
  }

  importVideo() {
    console.log("Import video");
    this.isCompletingPopup = true;
  }

  createVideo() {
    console.log("create video");
    this.isCompletingPopup = true;
  }

  recordVideo() {
    console.log("record video");
    this.isCompletingPopup = true;
  }

  closePopup() {
    console.log("đóng popup");
    this.isCompletingPopup = false;
  }
  onLoadedMetadata(event: Event, video: any): void {
    const duration = (event.target as HTMLVideoElement).duration;
    video.duration = this.formatDuration(duration);
  }

  formatDuration(seconds: number): string {
    const m = Math.floor(seconds / 60);
    const s = Math.floor(seconds % 60);
    return `${this.pad(m)}:${this.pad(s)}`;
  }

  pad(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }

  openDropdownAnalysisVideo(video: any) {
    this.dropdownVideoId = video.id;
    this.isOpenDropdownAnalysisVideo = !this.isOpenDropdownAnalysisVideo;
    console.log("mở dropdown cấu hình video");
  }
}
