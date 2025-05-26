import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../base-component/base-component.component';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { Subscription } from 'rxjs';
import { JobShortModel } from '../models/job-short-model';
import { SubjectService } from '../../../services/subject.service';
import { Router } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { CategoryModel } from '../../../models/CategoryModel';
import { DataService } from '../../../services/data.service';
import { JobDetailComponent } from "../job-detail/job-detail.component";
import { CreateJobComponent } from '../create-job/create-job.component';

@Component({
  selector: 'app-find-job',
  imports: [
    SharedModule,
    JobDetailComponent,
     CreateJobComponent
  ],
  templateUrl: './find-job.component.html',
  styleUrl: './find-job.component.css'
})
export class FindJobComponent extends BaseComponent implements OnInit {

  ListJobs: JobShortModel[] = new Array<JobShortModel>();
  showJobs: JobShortModel[] = new Array<JobShortModel>();
  selectedJobId!: string;
  selectedJob: JobShortModel | null = null;
  showJobDetailPopup = false;

  Categories: CategoryModel[] = new Array<CategoryModel>();
  showAllCategories = false;
  selectedCategoryId: string = '';

  showCreateJobPopup = false;

  selectedProvince!: string;

  constructor(
    authStateService: AuthStateService,
    private subjectService: SubjectService,
    private router: Router,
    private dataService: DataService
  ) {
    super(authStateService);
  }

  ngOnInit(): void {
    this.subjectService.receivedJobs$.subscribe((jobs: JobShortModel[]) => {
      this.ListJobs = jobs;
      this.showJobs = this.ListJobs;
      console.log(this.ListJobs);
    });

    this.subjectService.receivedCategory$.subscribe((category: any) => {
      this.Categories = category;
      console.log(this.Categories);
    });

    this.dataService.getCityFromApi().subscribe((res: any[]) => {
      this.provinces = res.map(p => p.name); // Gán tên các tỉnh vào danh sách
    });
  }

  onOpenCreateJob() {
    this.showCreateJobPopup = true;
  }

  onCloseCreateJob() {
    this.showCreateJobPopup = false;
  }

  onShowJobDetail(job: JobShortModel) {
    console.log("show job detail");
    this.selectedJobId = job.id;
    this.selectedJob = job;
    this.showJobDetailPopup = true;
  }

  onCloseJobDetailPopup() {
    this.showJobDetailPopup = false;
    this.selectedJob = null;
  }

  onCategoryChange(categoryId: string) {
    this.selectedCategoryId = categoryId;
    this.showJobs = this.filteredJobsByCategory;
  }

  get filteredJobsByCategory() {
    if (!this.selectedCategoryId) return this.ListJobs;
    return this.ListJobs.filter(job => job.categoryId === this.selectedCategoryId);
  }

  get displayedCategories() {
    return this.showAllCategories ? this.Categories : this.Categories.slice(0, 2);
  }

  onViewAllCategories() {
    this.showAllCategories = true;
  }

  onHideCategories() {
    this.showAllCategories = false;
  }

  onLocationChange(event: any) {
    console.log(this.selectedProvince);
  }
}
