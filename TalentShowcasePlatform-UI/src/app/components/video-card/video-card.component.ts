import { AfterViewInit, Component, ElementRef, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Enviroment } from '../../../environment';

@Component({
  selector: 'app-video-card',
  imports: [
    SharedModule
  ],
  templateUrl: './video-card.component.html',
  styleUrl: './video-card.component.css'
})
export class VideoCardComponent implements OnInit {
  videoPath = Enviroment.videoPath;
  @Input() videoData!: any;
  @Input() src!: string;
  visible = false;

  constructor(private el: ElementRef) { }
  ngOnInit(): void {
    // console.log("video data: ", this.videoData);
    // console.log("video src: ", this.src);
  }

  ngAfterViewInit() {
    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          this.visible = true;
          observer.unobserve(this.el.nativeElement);
        }
      },
      { threshold: 0.1 }
    );
    observer.observe(this.el.nativeElement);
  }

  onLoadedMetadata(event: Event, video: any): void {
    const duration = (event.target as HTMLVideoElement).duration;
    video.duration = this.formatDuration(duration);
  }

  formatDuration(seconds: number): string {
    const m = Math.floor(seconds / 60);
    const s = Math.floor(seconds % 60);
    return `${this.pad(m)}:${this.pad(s)}`;
  }

  pad(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }
}