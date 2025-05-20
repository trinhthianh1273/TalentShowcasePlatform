import { Component, Input } from '@angular/core';
import { NestedCommentComponent } from "../nested-comment/nested-comment.component";
import { SharedModule } from '../../shared/shared.module';
import { CommentModel, commentsMockData } from '../nested-comment/CommentModel';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthStateService } from '../../../services/auth-state.service';
import { CommunityService } from '../../../services/community/community.service';
import { error } from 'console';
import { GroupPostModel } from './group-post-model';

@Component({
  selector: 'app-community-post',
  imports: [NestedCommentComponent, SharedModule],
  templateUrl: './community-post.component.html',
  styleUrl: './community-post.component.css'
})
export class CommunityPostComponent {
  postId: string = '';
  postData!: GroupPostModel ; // Dữ liệu bài viết

  @Input() comments: CommentModel[] = commentsMockData; // Truyền từ parent hoặc lấy qua API
  constructor(
    private route: ActivatedRoute,
    private authStateService: AuthStateService,
    private communityService: CommunityService
  ) { }

  isLoggedIn: boolean = false;
  currentUser: any = null;
  private authSubscription: Subscription | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.postId = id;
        this.loadPost(this.postId);
        this.loadCommentPost(this.postId);
      }
    });

    this.authSubscription = this.authStateService.isLoggedIn$.subscribe(
      (loggedIn) => {
        this.isLoggedIn = loggedIn;
      }
    );
    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe((user) => {
        this.currentUser = user;
      })
    );
  }

  loadPost(id: any) {
    this.communityService.getCommunityPostDetail(id).subscribe({
      next: (res) => {
        this.postData = res?.data;
        console.log('Post Data:', this.postData);
      },
      error: (error) => {
        console.error('Error fetching post data:', error);
      }
    }
    );
  }

  loadCommentPost(id: any) {
    this.communityService.getCommunityPostComments(id).subscribe({
      next: (res) => {
        //this.comments = res?.data;
        console.log('Comment Data:', res.data);
      },
      error: (error) => {
        console.error('Error fetching comment data:', error);
      }
    });
  }
}
