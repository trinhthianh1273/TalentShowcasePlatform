import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import { JobShortModel } from '../models/job-short-model';
import { BaseComponent } from '../../base-component/base-component.component';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { JobFullModel } from '../models/job-full-model';
import { JobService } from '../../../services/job/job.service';
import { NotificationService } from '../../../services/notifications/notification.service';

@Component({
  selector: 'app-job-detail',
  imports: [SharedModule],
  templateUrl: './job-detail.component.html',
  styleUrl: './job-detail.component.css',
})
export class JobDetailComponent extends BaseComponent implements OnInit {
  @Input() jobId: string | '' = '';
  @Output() close = new EventEmitter<void>();

  JobData: JobFullModel | null = null;

  constructor(
    authStateService: AuthStateService,
    notiService: NotificationService,
    private jobService: JobService
  ) {
    super(authStateService, notiService);
  }
  ngOnInit(): void {
    console.log('jobId: ', this.jobId);
    if (this.jobId) {
      this.loadJobData();
    }
  }

  applyViaEmail() {
    if (!this.JobData?.contactEmail || !this.JobData?.title) {
      console.log('Không thể gửi email');
      return;
    };
    const subject = encodeURIComponent('Apply ' + this.JobData.title);
    const to = encodeURIComponent(this.JobData.contactEmail);
    const gmailUrl = `https://mail.google.com/mail/?view=cm&fs=1&to=${to}&su=${subject}`;
    window.open(gmailUrl, '_blank');
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['jobId'] && changes['jobId'].currentValue) {
      this.loadJobData();
    }
  }

  private loadJobData() {
    this.jobService.getJobById(this.jobId).subscribe((res) => {
      this.JobData = Array.isArray(res.data) ? res.data[0] : res.data;
      console.log('JobData: ', this.JobData);
    });
  }

  splitSentences(text: string | undefined | null): string[] {
    if (!text) return [];
    // Tách theo dấu chấm, dấu xuống dòng, hoặc dấu chấm hỏi, dấu chấm than
    return text.split(/(?<=[.?!])\s+|\n+/).map(s => s.trim()).filter(s => s.length > 0);
  }
}
