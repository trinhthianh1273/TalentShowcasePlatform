import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { JobService } from '../../../services/job/job.service';
import { SharedModule } from '../../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '../../base-component/base-component.component';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { DataService } from '../../../services/data.service';
import { Router } from '@angular/router';
import { PopupComponent } from '../../popup/popup.component';

@Component({
  selector: 'app-create-job',
  imports: [
    SharedModule,
    PopupComponent
  ],
  templateUrl: './create-job.component.html',
  styleUrl: './create-job.component.css'
})
export class CreateJobComponent extends BaseComponent implements OnInit {
  @Output() close = new EventEmitter<void>();
  @Input() Categories: any;

  jobForm!: FormGroup;
  today: string = new Date().toISOString().split('T')[0];

  createdJobId!: string;
  isJobPost: boolean = false;
  createdJobNotiTitle!: string;
  createdJobNotiMessage!: string;
  createdJobNotiInfo: 'success' | 'error' | 'info' = 'info';

  constructor(
    authStateService: AuthStateService,
    private jobService: JobService,
    private fb: FormBuilder,
    private dataService: DataService,
    private router: Router
  ) {
    super(authStateService);
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
    console.log("Category: ", this.Categories)
    this.dataService.getCityFromApi().subscribe((res: any[]) => {
      this.provinces = res.map(p => p.name); // Gán tên các tỉnh vào danh sách
    });
  }

  onSubmit() {
    console.log("thực hiện submit form");
    if (this.jobForm.valid) {
      const formValue = {
        ...this.jobForm.value,
        postedBy: this.currentUser.userId,
        // Convert string dates to Date objects if needed
        expiryDate: this.jobForm.value.expiryDate ? new Date(this.jobForm.value.expiryDate) : null
      };
      console.log(formValue);

      this.jobService.createJob(formValue).subscribe({
        next: (res) => {
          console.log(res?.data);
          this.isJobPost = true;
          this.createdJobNotiTitle = "Created Job";
          this.createdJobNotiMessage = "Your job has been created successfully";
          this.createdJobNotiInfo = "success";
        }, error: (err) => {
          console.error(err);

          this.isJobPost = false;
          this.createdJobNotiTitle = "Error";
          this.createdJobNotiMessage = "Something went wrong, please try again later";
          this.createdJobNotiInfo = "error";
        }
      });

    } else {
      console.log('Invalid form');
    }
  }

  onPopupClosed(createdPostId: any) {
    this.isJobPost = false;
    if (this.router.url === '/job/your-jobs') {
      window.location.reload();
    } else {
      this.router.navigate(['/job/your-jobs']);
    }
  }

  onClose() {
    this.close.emit();
  }
}
