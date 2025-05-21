import { Component, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { CommentModel } from '../../../models/CommentModel';
import { Enviroment } from '../../../../environment';

@Component({
  selector: 'app-nested-comment',
  imports: [
    SharedModule
  ],
  templateUrl: './nested-comment.component.html',
  styleUrl: './nested-comment.component.css'
})
export class NestedCommentComponent implements OnInit {
  @Input() userName!: string;
  @Input() userAvatar!: string;
  @Input() timeDiff!: string;
  @Input() content!: string;

  @Input() comment!: CommentModel;

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  avatarSrc: string = "";

  constructor() {
  }

  ngOnInit(): void {

  }

  onImageError(event: Event) {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = Enviroment.tempAvatarPath; // Đường dẫn ảnh mặc định
  }

}
