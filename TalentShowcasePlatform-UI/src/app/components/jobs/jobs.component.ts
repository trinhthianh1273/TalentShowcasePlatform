import { Component, OnInit } from '@angular/core';
import { JobHeaderComponent } from "./job-header/job-header.component";
import { SharedModule } from '../../shared/shared.module';
import { BaseComponent } from '../base-component/base-component.component';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { JobService } from '../../services/job/job.service';
import { SubjectService } from '../../services/subject.service';
import { Router } from '@angular/router';
import { ReceivedDataModel } from '../../models/ReceivedDataMode';
import { DataService } from '../../services/data.service';
import { CategoryModel } from '../../models/CategoryModel';

@Component({
  selector: 'app-jobs',
  imports: [JobHeaderComponent,
    SharedModule
  ],
  templateUrl: './jobs.component.html',
  styleUrl: './jobs.component.css'
})
export class JobsComponent extends BaseComponent implements OnInit {
  Categories: CategoryModel[] = new Array<CategoryModel>();
  
  constructor(
    authStateService: AuthStateService,
    private JobService: JobService,
    private subjectService: SubjectService,
    private dataService: DataService,
    private router: Router
  ) {
    super(authStateService);
  }
  ngOnInit(): void {
    this.getAllJob();
    this.loadCategory();
  }

  getAllJob(): void {
    this.JobService.getAllJob().subscribe({
      next: (data: ReceivedDataModel) => {
        this.subjectService.sendJobs(data?.data);
      },
      error: (error: any) => {
        console.error('Error fetching all jobs:', error);
      }
    });
  }

  loadCategory() {
    this.dataService.getCategories().subscribe(
      {
        next: (res: ReceivedDataModel) => {
          if (res.data) {
            this.subjectService.sendCategory(res.data);
          }
        },
        error: (err: any) => {
          console.error("Lỗi khi lấy dữ liệu category:", err.message);
        }
      }
    );
  }
}
