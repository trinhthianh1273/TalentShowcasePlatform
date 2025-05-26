import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoCardComponent } from '../video-card/video-card.component';
import { Enviroment } from '../../../environment';
import { SharedModule } from '../../shared/shared.module';
import { VideosService } from '../../services/videos.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { DataService } from '../../services/data.service';
import { SubjectService } from '../../services/subject.service';
import { HeaderComponent } from '../header/header.component';
import { BaseComponent } from '../base-component/base-component.component';

@Component({
  selector: 'app-video-detail',
  standalone: true,
  imports: [
    SharedModule,
    HeaderComponent
],
  templateUrl: './video-detail.component.html',
  styleUrl: './video-detail.component.css'
})
export class VideoDetailComponent extends BaseComponent implements OnInit {
  
  src = Enviroment.videoPath;
  videoId!: any;
  videoDetail: any = null;
  comments: any[] = [];
  likes: any[] = [];

  commentForm!: FormGroup;
  private userSubscription: Subscription | undefined;

  constructor(
    private subjectService: SubjectService,
    private dataService: DataService,
    private videoService: VideosService,
    private route: ActivatedRoute,
    authStateService: AuthStateService
  ) {
    super(authStateService);
    this.commentForm = new FormGroup({
      videoId: new FormControl(''),
      userId: new FormControl(''),
      content: new FormControl('', Validators.required)
    });
  }
  ngOnInit(): void {
    this.videoId = this.route.snapshot.queryParamMap.get('id');
    this.subscribeAuthState();

    this.getVideoById(this.videoId);
    this.getVideoComment(this.videoId);
    this.getVideoLikes(this.videoId );
    console.log("check login:", this.isLoggedIn);

    const contentControl = this.commentForm.get('content');
    if (this.isLoggedIn) {
      contentControl?.enable();
    } else {
      contentControl?.disable();
    }
  }

  override onCurrentUserLoaded(user: any): void { 
    if (user != null) {
      this.userId = user.userId;
      this.avatarUrl = this.avatarPath + user.avatarUrl;
    }
    this.autoAddView();
  }

  autoAddView() {
    console.log("thực hiện tăng view");
    if (this.videoId && this.userId) {
      const viewData = {
        videoId: this.videoId,
        viewerId: this.userId
      };
      this.videoService.addView(viewData).subscribe({
        next: (res) => {
          console.log("Tăng view thành công:", res);
        },
        error: (err) => {
          console.error("Lỗi khi tăng view:", err);
        }
      });
    }
  }

  submitComment(): void {
    if (this.isLoggedIn && this.commentForm.valid) {
      const commentData = {
        videoId: this.videoId,
        userId: this.currentUser.userId,
        content: this.commentForm.get('content')?.value.trim(),
      };
      console.log("submit comment: ", commentData);

      this.videoService.postComment(commentData).subscribe({
        next: (response) => {
          console.log('Bình luận thành công:', response);
          this.commentForm.reset();
          // Tải lại bình luận
          this.getVideoComment(this.videoId);
        },
        error: (error) => {
          console.error('Lỗi khi bình luận:', error);
        },
      });
    } else if (!this.isLoggedIn) {
      alert('Bạn cần đăng nhập để bình luận.');
    }
  }

  getVideoById(id: any) {
    this.dataService.getVideoById(id).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.subjectService.sendVideo(res.data);
          this.videoDetail = res.data;
          console.log("Video data: ", this.videoDetail);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
      }
    });
  }

  getVideoComment(videoId: any) {
    this.videoService.getCommentsVideo(videoId).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.comments = res.data;
          console.log("Comment data: ", this.comments);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy comment video:", err.message);
      }
    });
  }

  getVideoLikes(videoId: any) {
    this.videoService.getLikeVideo(videoId).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.likes = res.data;
          console.log("Like data: ", this.likes);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy like video:", err.message);
      }
    });
  }

  logout(): void {
    console.log("Đăng xuất");
    this.authStateService.logout();
    window.location.reload(); // Hoặc phương pháp điều hướng khác}
  }
}
