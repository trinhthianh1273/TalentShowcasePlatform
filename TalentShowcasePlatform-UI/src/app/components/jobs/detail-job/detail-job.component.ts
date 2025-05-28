import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import { BaseComponent } from '../../base-component/base-component.component';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { JobFullModel } from '../models/job-full-model';
import { NotificationService } from '../../../services/notifications/notification.service';

@Component({
  selector: 'app-detail-job',
  imports: [
    SharedModule
  ],
  templateUrl: './detail-job.component.html',
  styleUrl: './detail-job.component.css'
})
export class DetailJobComponent extends BaseComponent implements OnInit {
  @Input() JobData : JobFullModel | null = null;
  @Output() close = new EventEmitter<void>();

  constructor(
    authStateService: AuthStateService,
    notiService: NotificationService,
  ) {
    super( authStateService , notiService);
  }
  ngOnInit(): void {
    console.log(this.JobData);
  }

  splitSentences(text: string | undefined | null): string[] {
    if (!text) return [];
    // Tách theo dấu chấm, dấu xuống dòng, hoặc dấu chấm hỏi, dấu chấm than
    return text.split(/(?<=[.?!])\s+|\n+/).map(s => s.trim()).filter(s => s.length > 0);
  }
}
