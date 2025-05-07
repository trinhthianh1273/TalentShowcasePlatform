import { AfterViewInit, Component, ElementRef, Input, OnInit } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

@Component({
  selector: 'app-video-card',
  imports: [
    SharedModule
  ],
  templateUrl: './video-card.component.html',
  styleUrl: './video-card.component.css'
})
export class VideoCardComponent implements OnInit {
  videoPath = 'https://localhost:7172/api/Videos/video-path/';
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

  // ngOnDestroy(): void {
  //   if (this.player) {
  //     this.player.dispose();
  //   }
  // }
}