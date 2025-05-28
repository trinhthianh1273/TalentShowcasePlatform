import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { BaseComponent } from '../../base-component/base-component.component';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { JobFullModel } from '../models/job-full-model';
import { CategoryModel } from '../../../models/CategoryModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../shared/shared.module';
import { JobService } from '../../../services/job/job.service';
import { DataService } from '../../../services/data.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../../services/notifications/notification.service';

@Component({
  selector: 'app-update-job',
  imports: [
    SharedModule
  ],
  templateUrl: './update-job.component.html',
  styleUrl: './update-job.component.css'
})
export class UpdateJobComponent extends BaseComponent implements OnInit {
  @Input() JobData: JobFullModel | null = null;
  @Input() Categories: CategoryModel[] = new Array<CategoryModel>();
  @Output() close = new EventEmitter<void>();

  jobForm!: FormGroup;
  today: string = new Date().toISOString().split('T')[0];

  updatedJobId!: string;
  isUpdatedJob: boolean = false;
  updatedJobNotiTitle!: string;
  updatedJobNotiMessage!: string;
  updatedJobNotiInfo: 'success' | 'error' | 'info' = 'info';

  constructor(
    authStateService: AuthStateService,
    notiService: NotificationService,
    private fb: FormBuilder,
    private jobService: JobService,
    private dataService: DataService,
    private router: Router
  ) {
    super(authStateService, notiService);
    this.jobForm = this.fb.group({
      title: ['', Validators.required],
      companyName: [''],
      location: ['', Validators.required],
      addressDetail: ['', Validators.required],
      description: ['', Validators.required],
      requirements: ['', Validators.required],
      benefits: ['', Validators.required],
      jobType: ['', Validators.required],
      salaryFrom: [0, Validators.required],
      salaryTo: [0, Validators.required],
      expiryDate: [null],
      contactEmail: ['', [Validators.required, Validators.email]],
      contactPhone: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.subscribeAuthState();

    this.dataService.getCityFromApi().subscribe((res: any[]) => {
      this.provinces = res.map(p => p.name); // Gán tên các tỉnh vào danh sách
    });

    // Nếu JobData đã có khi khởi tạo
    console.log(this.JobData);
    if (this.JobData) {
      this.setFormValues(this.JobData);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['JobData'] && changes['JobData'].currentValue) {
      this.setFormValues(changes['JobData'].currentValue);
    }
  }

  onSubmit() {

    if (this.jobForm.valid && this.JobData) {
      console.log("thực hiện cập nhật data");
      const formValue = {
        ...this.jobForm.value,
        postedBy: this.currentUser?.userId,
        id: this.JobData?.id
        // Convert string dates to Date objects if needed
      };
      console.log(formValue);
      this.jobService.updateJob(this.JobData?.id, formValue).subscribe({
        next: (res) => {
          this.isUpdatedJob = true;
          this.updatedJobNotiTitle = "Updated Job";
          this.updatedJobNotiMessage = "Your job has been updated successfully";
          this.updatedJobNotiInfo = "success";
        }, error: (err) => {
          this.isUpdatedJob = true;
          this.updatedJobNotiTitle = "Update Fail";
          this.updatedJobNotiMessage = "Something went wrong, please try again later";
          this.updatedJobNotiInfo = "error";
          console.error(err);
        },
      });


    } else {
      console.log("form invalid");
    }
  }

  private setFormValues(data: JobFullModel) {
    this.jobForm.patchValue({
      title: data.title || '',
      companyName: data.companyName || '',
      location: data.location || '',
      addressDetail: data.addressDetail || '',
      description: data.description || '',
      requirements: data.requirements || '',
      benefits: data.benefits || '',
      jobType: data.jobType || '',
      salaryFrom: data.salaryFrom || 0,
      salaryTo: data.salaryTo || 0,
      expiryDate: data.expiryDate ? data.expiryDate.split('T')[0] : null,
      contactEmail: data.contactEmail || '',
      contactPhone: data.contactPhone || '',
      categoryId: data.categoryId || ''
    });
  }

  onPopupClosed(updatedJobId: any) {
    console.log("đóng popup");
    this.isUpdatedJob = false;
    window.location.reload();
  }

  onClose() {
    this.close.emit();
  }
}
