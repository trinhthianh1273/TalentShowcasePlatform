import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { VideosService } from '../../../services/videos.service';
import { AuthStateService } from '../../../services/auth-state.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginResponse } from '../../../interfaces/interface';
import { Subscription } from 'rxjs';
import { Enviroment } from '../../../../environment';
import { DataService } from '../../../services/data.service';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-video',
  imports: [
    SharedModule
  ],
  templateUrl: './user-video.component.html',
  styleUrl: './user-video.component.css'
})
export class UserVideoComponent implements OnInit {
  isLoggedIn: any;
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  src = Enviroment.videoPath;
  videoId: any;
  videoData: any = null;
  categories: any[] = [];
  commentVideo: any[] = [];

  updateVideoForm!: FormGroup;

  // profile popup
  popupOpen = false;
  @ViewChild('popupWrapper', { static: false }) popupWrapper!: ElementRef;
  
  constructor(
    private videoService: VideosService,
    private authStateService: AuthStateService,
    private dataService: DataService,
    private toastr: ToastrService,
    private routeActive: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.videoId = this.routeActive.snapshot.queryParamMap.get('id');
    this.loadVideo(this.videoId);
    this.loadCommentVideo(this.videoId);

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


  refreshComponent() {
    const currentUrl = this.router.url.split('?')[0]; // chỉ lấy path
    const queryParams = this.routeActive.snapshot.queryParams;

    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl], { queryParams });
    });
  }


  onSubmit(videoId: any) {
    if (this.updateVideoForm.invalid) {
      // Đánh dấu tất cả ô là touched để hiện lỗi
      this.markAllAsTouched(this.updateVideoForm);

      // Scroll đến input lỗi đầu tiên
      setTimeout(() => {
        const firstInvalid = document.querySelector('.ng-invalid');
        if (firstInvalid) {
          (firstInvalid as HTMLElement).scrollIntoView({
            behavior: 'smooth',
            block: 'center'
          });
          (firstInvalid as HTMLElement).focus();
        }
      }, 0);

      return;
    }
    console.log("thực hiện submit form: ", this.updateVideoForm.value);

    // Gửi form value, chuyển đổi isPublic → isPrivate đúng theo BE
    try {
      this.videoService.updateVideo(this.videoId, this.updateVideoForm.value).subscribe({
        next: (res) => {
          this.toastr.success('Cập nhật video thành công!', '', {
            timeOut: 10000 // thời gian hiển thị tính bằng mili giây (3000ms = 3 giây),
          });
          this.refreshComponent();
        },
        error: (err) => {
          console.log(err);
          this.toastr.error(' Cập nhật video thất bại!', '', {
            timeOut: 10000
          });
        }
      });
    } catch (error) {
      console.error("Lỗi khi gửi form:", error);
      this.toastr.error(' Lỗi hệ thống khi cập nhật video!', '', {
        timeOut: 5000
      });
      return;
    }
  }

  markAllAsTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if ((control as FormGroup).controls) {
        this.markAllAsTouched(control as FormGroup);
      }
    });
  }

  resizeTextarea(el: HTMLTextAreaElement): void {
    el.style.height = 'auto'; // reset height
    el.style.height = `${el.scrollHeight}px`; // set to full content
  }

  onTogglePublic(event: any) {
    console.log('Checkbox được chọn:', event.checked);
    this.videoData.IsPrivate = !this.videoData.IsPrivate; // Đảo ngược trạng thái isPublic
    console.log('Trạng thái mới:', this.videoData?.IsPrivate); // Thay đổi trạng thái nếu cần
  }


  loadVideo(videoId: any) {
    this.videoService.getVideoById(videoId).subscribe(
      {
        next: (res: any) => {
          if (res?.data) {
            this.videoData = res.data;
            console.log("Video data: ", this.videoData);
            this.formInit();
          }
        },
        error: (err: any) => {
          console.error("Lỗi khi lấy dữ liệu video:", err.message);
        }
      }
    );
  }

  formInit() {
    //Tạo form phù hợp backend, nhưng hiển thị isPublic
    this.updateVideoForm = new FormGroup({
      id: new FormControl(this.videoData.id || null),
      title: new FormControl(this.videoData.title, Validators.required),
      description: new FormControl(this.videoData.description, Validators.required),
      url: new FormControl(this.videoData.url),
      categoryId: new FormControl(this.videoData.categoryId),
      IsPrivate: new FormControl(this.videoData.isPrivate) // để hiển thị checkbox
    });
  }

  loadCommentVideo(videoId: any) {
    this.videoService.getCommentsVideo(videoId).subscribe(
      {
        next: (res: any) => {
          if (res?.data) {
            this.commentVideo = res.data;
            console.log("Comment length: ", this.commentVideo.length);
            console.log("Comment data: ", this.commentVideo);
          }
        },
        error: (err: any) => {
          console.error("Lỗi khi lấy dữ liệu comment:", err.message);
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

  togglePopup() {
    this.popupOpen = !this.popupOpen;
  }

  logout(): void {
    this.authStateService.logout();
    this.router.navigate(['/']);
  }

}
