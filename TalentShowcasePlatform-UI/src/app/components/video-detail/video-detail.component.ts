import { Component, OnInit } from '@angular/core';
import { SendDataService } from '../../services/send.data.service';
import { GetDataService } from '../../services/get.data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoCardComponent } from '../video-card/video-card.component';

@Component({
  selector: 'app-video-detail',
  imports: [
    VideoCardComponent
  ],
  templateUrl: './video-detail.component.html',
  styleUrl: './video-detail.component.css'
})
export class VideoDetailComponent implements OnInit{
  isLogin : boolean = true;
  id!: any;
  videoDetail: any;

  constructor(
    private SDService: SendDataService,
    private GDService: GetDataService,
    private route: ActivatedRoute,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.id = this.route.snapshot.queryParamMap.get('id');
    console.log('Video ID:', this.id);
  }

  getVideoById(id: any) {
    this.GDService.getVideoById(id).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.SDService.sendVideo(res.data);
          this.videoDetail = res.data;
          console.log("Video data: ", this.videoDetail);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy dữ liệu video:", err.message);
      },
      complete: () => {
        console.log("Hoàn thành việc lấy dữ liệu video");
      }
    });

  }

}
