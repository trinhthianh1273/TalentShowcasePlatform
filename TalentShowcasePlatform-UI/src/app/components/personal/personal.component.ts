import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base-component/base-component.component';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { HeaderComponent } from "../header/header.component";
import { SharedModule } from '../../shared/shared.module';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDtoModel } from '../../models/UserDtoModel';
import { VideoDataModel } from '../../models/VideoDataModel';
import { VideosService } from '../../services/videos.service';
import { VideoCardComponent } from "../video-card/video-card.component";
import { AsideLeftComponent } from "../aside-left/aside-left.component";
import { PopupComponent } from '../popup/popup.component';
import { NotificationService } from '../../services/notifications/notification.service';

@Component({
  selector: 'app-personal',
  imports: [
    HeaderComponent, 
    SharedModule,
    PopupComponent
  ],
  templateUrl: './personal.component.html',
  styleUrl: './personal.component.css',
})
export class PersonalComponent extends BaseComponent implements OnInit {
  personalId: string | null = null;
  personalUser: UserDtoModel | null = null;

  videoOfUser: VideoDataModel[] = [];

  selectedVideo: VideoDataModel | null = null;
  selectedVideoIndex: number | 0 = 0;
  showVideoModal = false;

  showFeaturePopup = false;

  constructor(
    authStateService: AuthStateService,
    notiService: NotificationService,
    private userService: UserService,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private videoService: VideosService
  ) {
    super(authStateService, notiService);
  }

  ngOnInit(): void {
    this.subscribeAuthState();
    this.personalId = this.activeRoute.snapshot.paramMap.get('id');
    if (this.personalId) {
      this.loadPersonalUser(this.personalId);
      this.loadVideoOfUser(this.personalId);
    }
  }

  handleFollow() {
    this.showFeaturePopup = true;
  }

  handleMessage() {
    this.showFeaturePopup = true;
  }

  onCloseFeaturePopup() {
    console.log("đóng popup");
    this.showFeaturePopup = false;
  }

  onOpenVideoModal(video: VideoDataModel, index: number) {
    console.log("hiển thị modal");
    this.selectedVideo = video;
    this.selectedVideoIndex = index;
    this.showVideoModal = true;
    this.autoAddView(this.selectedVideo.id);
  }

  onPreviousVideo() {
    if (this.selectedVideoIndex > 0) {
      this.selectedVideoIndex--;
      this.selectedVideo = this.videoOfUser[this.selectedVideoIndex];
    }
  }

  onNextVideo() {
    if (this.selectedVideoIndex < this.videoOfUser.length - 1) {
      this.selectedVideoIndex++;
      this.selectedVideo = this.videoOfUser[this.selectedVideoIndex];
    }
  }

  onCloseVideoModal() {
    this.showVideoModal = false;
    this.selectedVideoIndex = 0;
    this.selectedVideo = null;
  }

  loadPersonalUser(id: string): void {
    this.userService.getUserById(id).subscribe({
      next: (res) => {
        if (res.succeeded) {
          this.personalUser = Array.isArray(res.data) ? res.data[0] : res.data;
          console.log(this.personalUser);
        } else {
          console.error('Failed to load user data:', res.messages);
        }
      },
      error: (err) => {
        console.error('Error fetching user data:', err);
      },
    });
  }

  loadVideoOfUser(id: string): void {
    this.videoService.getVideoByUser(id).subscribe({
      next: (res) => {
        if (res.succeeded) {
          this.videoOfUser = res.data;
          console.log(this.videoOfUser);
        } else {
          console.error('Failed to load user videos:', res.messages);
        }
      },
      error: (err) => {
        console.error('Error fetching user videos:', err);
      },
    });
  }

  autoAddView(videoId: string) {
    console.log('thực hiện tăng view');
    if (videoId && this.userId) {
      const viewData = {
        videoId: videoId,
        viewerId: this.userId,
      };
      this.videoService.addView(viewData).subscribe({
        next: (res) => {
          console.log('Tăng view thành công:', res);
        },
        error: (err) => {
          console.error('Lỗi khi tăng view:', err);
        },
      });
    }
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
}
