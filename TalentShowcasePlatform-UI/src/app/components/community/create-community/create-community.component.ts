import { Component, EventEmitter, Output } from '@angular/core';
import { CommunityService } from '../../../services/community/community.service';
import { SubjectService } from '../../../services/subject.service';
import { Router } from '@angular/router';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../../shared/shared.module';
import { BaseComponent } from '../../base-component/base-component.component';

@Component({
  selector: 'app-create-community',
  imports: [
    SharedModule
  ],
  templateUrl: './create-community.component.html',
  styleUrl: './create-community.component.css'
})
export class CreateCommunityComponent extends BaseComponent {
  @Output() close = new EventEmitter<void>();

  categorys: any[] = [];

  form!: FormGroup;
  avatarFile: File | null = null;
  avatarPreviewUrl: string | ArrayBuffer | null = null;

  constructor(
    private communityService: CommunityService,
    private subjectService: SubjectService,
    private router: Router,
    authStateService: AuthStateService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private http: HttpClient
  ) {
    super(authStateService);
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      categoryId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.subscribeAuthState();
    this.subjectService.receivedCategory$.subscribe((data) => {
      this.categorys = data;
      console.log("category: ", this.categorys);
    });
  }

  onAvatarChange(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    const file = fileInput.files?.[0];

    if (file) {
      this.avatarFile = file;

      // Preview ảnh
      const reader = new FileReader();
      reader.onload = () => {
        this.avatarPreviewUrl = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const formData = new FormData();
    formData.append('name', this.form.value.name);
    formData.append('description', this.form.value.description);
    formData.append('createdBy', this.currentUser.userId);
    formData.append('categoryId', this.form.value.categoryId);
    if (this.avatarFile) {
      formData.append('groupAvatar', this.avatarFile);
    }

    this.communityService.createCommunity(formData).subscribe({
      next: (res) => {
        console.log("thành công: ", res.data);
        
        this.toastr.success(
          'Cộng đồng của bạn đã được tạo thành công.',
          'Tạo cộng đồng thành công',
          {
            positionClass: 'toast-top-right',
            timeOut: 5000
          }
        );
        // this.closePopup();
        console.log('Tạo cộng đồng thành công:', res);
        setTimeout(() => {
          this.router.navigate(['/group', res.data]);
        }, 5000);
      },
      error: (err) => {
        console.error('Lỗi khi tạo cộng đồng:', err);
      }
    });
  }

   onCancel() {
    // Xử lý đóng modal hoặc reset form...
  }

  closePopup() {
    this.close.emit();
  }
}
