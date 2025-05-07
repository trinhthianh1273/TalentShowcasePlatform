import { Component, OnInit } from '@angular/core';
import { SendDataService } from '../../services/send.data.service';
import { GetDataService } from '../../services/get.data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoCardComponent } from '../video-card/video-card.component';
import { Enviroment } from '../../../environment';
import { SharedModule } from '../shared/shared.module';
import { VideosService } from '../../services/videos.service';

@Component({
  selector: 'app-video-detail',
  imports: [
    VideoCardComponent,
    SharedModule
  ],
  templateUrl: './video-detail.component.html',
  styleUrl: './video-detail.component.css'
})
export class VideoDetailComponent implements OnInit{
  src = Enviroment.videoPath;
  isLogin : boolean = true;
  id!: any;
  videoDetail: any = null;
  comments : any[] = [];

  constructor(
    private SDService: SendDataService,
    private GDService: GetDataService,
    private videoService: VideosService,
    private route: ActivatedRoute,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.id = this.route.snapshot.queryParamMap.get('id');
    this.getVideoById(this.id);
    this.getVideoComment(this.id);
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

  getVideoComment(videoId: any) {
    this.videoService.getCommentsVideo(videoId).subscribe({
      next: (res: any) => {
        if (res?.data) {
          this.comments = res.data;
          console.log("Comment data: ", this.comments);
        }
      },
      error: (err: any) => {
        console.error("Lỗi khi lấy comment video:", err.message);
      }
    });
  }
}
