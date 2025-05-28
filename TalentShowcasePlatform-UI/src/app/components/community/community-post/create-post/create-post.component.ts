import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../../shared/shared.module';
import { BaseComponent } from '../../../base-component/base-component.component';
import { AuthStateService } from '../../../../services/auth/auth-state.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommunityPostService } from '../../../../services/community-post/community.post.service';
import { GroupModel } from '../../../../models/GroupModel';
import { SubjectService } from '../../../../services/subject.service';
import { BaseCommnunityComponent } from '../../../base-component/base-community-component';
import { CommunityService } from '../../../../services/community/community.service';
import { NotificationService } from '../../../../services/notifications/notification.service';

@Component({
  selector: 'app-create-post',
  imports: [
    SharedModule
  ],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.css'
})
export class CreatePostComponent extends BaseCommnunityComponent {
  // groupId!: string;
  // groupData!: GroupModel; 

  createdPostId!: string;

  postForm!: FormGroup;
  selectedImageFile: File | null = null;
  imagePreview: string | null = null;

  isCreatedPost: boolean = false;
  createdPostNotiTitle!: string;
  createdPostNotiMessage!: string;
  createdPostNotiInfo: 'success' | 'error' | 'info' = 'info';

  constructor(
    private subjectService: SubjectService,
    private fb: FormBuilder,
    AuthStateService: AuthStateService,
    notiService: NotificationService,
    private route: ActivatedRoute,
    private communityPostService: CommunityPostService,
    communityService: CommunityService,
    private router: Router
  ) {
    super(AuthStateService, communityService, notiService);

    this.postForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(300)]],
      content: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.subscribeAuthState();
    this.route.paramMap.subscribe(params => {
      this.groupId = params.get('groupId') || '';
      console.log("Group ID from URL:", this.groupId);
      this.subscribeGroupData(this.groupId);
    });
  }

  gotoGroup(groupId: string) {
    console.log("go to group");
    this.router.navigate([`/community/group/${groupId}`]);
  }

  onImageSelected(event: Event): void {
    const file = (event.target as HTMLInputElement)?.files?.[0];
    if (file) {
      this.selectedImageFile = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  removeImage(): void {
    this.selectedImageFile = null;
    this.imagePreview = null;
  }

  onSubmit() {
    if (this.postForm.invalid) return;

    const formData = new FormData();
    formData.append('title', this.postForm.value.title);
    formData.append('content', this.postForm.value.content);
    formData.append('userId', this.currentUser.userId);
    formData.append('groupId', this.groupId);

    if (this.selectedImageFile) {
      formData.append('imgFile', this.selectedImageFile); // backend sẽ lấy file này
    }

    this.communityPostService.createPost(formData).subscribe({
      next: (res) => {
        console.log("Post created:", res);
        // redirect hoặc show toast
        this.isCreatedPost = true;
        this.createdPostId = res?.data;

        this.isCreatedPost = true;
        this.createdPostNotiTitle = "Created Post";
        this.createdPostNotiMessage = "Your post has been created successfully";
        this.createdPostNotiInfo = "success";

        this.postForm.reset();
        this.selectedImageFile = null;
        this.imagePreview = null; 
      },
      error: (err) => {
        console.error("Error creating post:", err);
        this.isCreatedPost = false;
        this.createdPostNotiTitle = "Create Post";
        this.createdPostNotiMessage = "Failed to create post";
        this.createdPostNotiInfo = "error";
      }
    });
  }

  // Hàm xử lý khi đóng popup
onPopupClosed(createdPostId: any) {
  this.isCreatedPost = false;
  this.router.navigate(['/community/post', createdPostId]);
}
}
