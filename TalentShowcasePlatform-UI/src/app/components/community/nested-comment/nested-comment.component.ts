import { Component, Input } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { CommentModel } from './CommentModel';

@Component({
  selector: 'app-nested-comment',
  imports: [
    SharedModule
  ],
  templateUrl: './nested-comment.component.html',
  styleUrl: './nested-comment.component.css'
})
export class NestedCommentComponent {
  @Input() userName!: string;
  @Input() userAvatar!: string;
  @Input() timeDiff!: string;
  @Input() content!: string;

  @Input() comment!: CommentModel;
}
