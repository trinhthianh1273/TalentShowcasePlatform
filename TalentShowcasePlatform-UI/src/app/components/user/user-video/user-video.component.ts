import { Component, OnInit } from '@angular/core';
import { VideosService } from '../../../services/videos.service';
import { AuthStateService } from '../../../services/auth-state.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { LoginResponse } from '../../../interfaces/interface';
import { Subscription } from 'rxjs';
import { Enviroment } from '../../../../environment';
import { DataService } from '../../../services/data.service';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-user-video',
  imports: [
    SharedModule
  ],
  templateUrl: './user-video.component.html',
  styleUrl: './user-video.component.css'
})
export class UserVideoComponent implements OnInit {
  isLoggedIn: boolean = false;
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  src = Enviroment.videoPath;
  videoId: any;
  videoData: any = null;
  categories: any[] = [];

  constructor(
    private videoService: VideosService,
    private authStateService: AuthStateService,
    private dataService: DataService,
    private toastr: ToastrService,
    private routeActive: ActivatedRoute
  ) { }
  ngOnInit(): void {
    this.videoId = this.routeActive.snapshot.queryParamMap.get('id');
    this.loadVideo(this.videoId);

    this.authSubscription = this.authStateService.isLoggedIn$.subscribe(
      (loggedIn) => {
        this.isLoggedIn = loggedIn;
      }
    );

    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe((user) => {
        this.currentUser = user;
        if (this.currentUser?.avatarUrl) {
          this.avatarUrl = this.avatarPath + this.currentUser.avatarUrl;
        }
      })
    );

    this.loadCategory();
  }

  updateVideo(videoId: any) {
    console.log("Video ID: ", videoId);
  }

  onTogglePublic(videoData: any) {
  console.log('Trạng thái mới:', videoData); // Thay đổi trạng thái nếu cần
}


  loadVideo(videoId: any) {
    this.videoService.getVideoById(videoId).subscribe(
      {
        next: (res: any) => {
          if (res?.data) {
            this.videoData = res.data;
            console.log("Video data: ", this.videoData);
          }
        },
        error: (err: any) => {
          console.error("Lỗi khi lấy dữ liệu video:", err.message);
        }
      }
    );
  }

  loadCategory() {
    this.dataService.getCategories().subscribe(
      {
        next: (res: any) => {
          if (res?.data) {
            this.categories = res.data;
            console.log("Category data: ", this.categories);
          }
        },
        error: (err: any) => {
          console.error("Lỗi khi lấy dữ liệu category:", err.message);
        }
      }
    );
  }

}
