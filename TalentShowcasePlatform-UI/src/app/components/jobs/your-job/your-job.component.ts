import { Component, OnInit } from '@angular/core';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { BaseComponent } from '../../base-component/base-component.component';
import { JobFullModel } from '../models/job-full-model';
import { JobService } from '../../../services/job/job.service';
import { SharedModule } from '../../../shared/shared.module';
import { CreateJobComponent } from "../create-job/create-job.component";
import { CategoryModel } from '../../../models/CategoryModel';
import { SubjectService } from '../../../services/subject.service';
import { DetailJobComponent } from "../detail-job/detail-job.component";
import { UpdateJobComponent } from "../update-job/update-job.component";

@Component({
  selector: 'app-your-job',
  imports: [
    SharedModule,
    CreateJobComponent,
    DetailJobComponent,
    UpdateJobComponent
  ],
  templateUrl: './your-job.component.html',
  styleUrl: './your-job.component.css'
})
export class YourJobComponent extends BaseComponent implements OnInit {
  YourJobs: JobFullModel[] = new Array<JobFullModel>();
  showCreateJobPopup = false;
  showEditJobPopup = false;
  showJobDetailPopup = false;
  selectedJobId!: string;
  selectedJob: JobFullModel | null = null;

  Categories: CategoryModel[] = new Array<CategoryModel>();

  showDeleteConfirm = false;
  jobToDelete: any = null;

  constructor(
    authStateService: AuthStateService,
    private jobService: JobService,
    private subjectService: SubjectService
  ) {
    super(authStateService);
  }
  ngOnInit(): void {
    this.subscribeAuthState();
    this.subjectService.receivedCategory$.subscribe((category: any) => {
      this.Categories = category;
    });
    if (this.currentUser) {
      console.log(this.currentUser.avatarUrl);
      this.loadYourJob();
    }
  }

  loadYourJob() {
    this.jobService.getJobByUserId(this.currentUser.userId).subscribe({
      next: (response) => {
        this.YourJobs = response.data;
        console.log("Your jobs: ", this.YourJobs);
      }, error: (error) => {
        console.error(error);
      }
    });
  }

  onOpenCreateJob() { this.showCreateJobPopup = true; }
  onCloseCreateJob() { this.showCreateJobPopup = false; }
  onViewJob(job: JobFullModel) {
    this.selectedJob = job;
    this.showJobDetailPopup = true;
  }
  onCloseJobDetailPopup() {
    this.showJobDetailPopup = false;
    this.selectedJobId = '';
  }
  onEditJob(job: JobFullModel) {
    this.selectedJob = job;
    this.showEditJobPopup = true;
  }
  onCloseEditJob() {
    this.showEditJobPopup = false;
    this.selectedJobId = '';
  }
  onDeleteJob(job: JobFullModel) {
    this.selectedJobId = job.id;
    this.selectedJob = job;
    this.showDeleteConfirm = true;
    // Xử lý xác nhận và xóa job
  }

  onConfirmDeleteJob() {
    // Gọi service xóa job ở đây, ví dụ:
    if (this.selectedJob) {
      this.jobService.deleteJob(this.selectedJob.id).subscribe(() => {
        // Sau khi xóa thành công, cập nhật lại danh sách job
        this.YourJobs = this.YourJobs.filter(j => j.id !== this.selectedJob?.id);
        this.showDeleteConfirm = false;
        this.jobToDelete = null;
      });
    }
  }

  onCancelDeleteJob() {
    this.showDeleteConfirm = false;
    this.jobToDelete = null;
  }

}
