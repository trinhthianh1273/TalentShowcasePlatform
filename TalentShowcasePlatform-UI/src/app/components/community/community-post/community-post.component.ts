import { Component, Input } from '@angular/core';
import { NestedCommentComponent } from "../nested-comment/nested-comment.component";
import { SharedModule } from '../../../shared/shared.module';
import { CommentModel, commentsMockData } from '../../../models/CommentModel';
import { ActivatedRoute } from '@angular/router';
import { EMPTY, empty, Subscription } from 'rxjs';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { CommunityService } from '../../../services/community/community.service';
import { error } from 'console';
import { GroupPostModel } from '../../../models/group-post-model';
import { CommunityPostService } from '../../../services/community-post/community.post.service';
import { Enviroment } from '../../../../environment';
import { GroupModel } from '../../../models/GroupModel';
import { SubjectService } from '../../../services/subject.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '../../base-component/base-component.component';

@Component({
  selector: 'app-community-post',
  imports: [NestedCommentComponent, SharedModule],
  templateUrl: './community-post.component.html',
  styleUrl: './community-post.component.css'
})
export class CommunityPostComponent extends BaseComponent {
  postId: string = '';
  postData!: GroupPostModel; // Dữ liệu bài viết
  groupId: string = ''; // ID của nhóm
  groupData!: GroupModel;

  groupPostPath = Enviroment.groupPostPath;

  @Input() comments: CommentModel[] = []; // Truyền từ parent hoặc lấy qua API
  likes: any[] = []; // Dữ liệu like
  isLikePost: boolean = false;
  isLikePostId!: string;

  constructor(
    private route: ActivatedRoute,
    authStateService: AuthStateService,
    private communityService: CommunityService,
    private cPostService: CommunityPostService,
    private subjectService: SubjectService,
  ) {
    super(authStateService);
    this.commentForm = new FormGroup({
      content: new FormControl('', Validators.required)
    });
  }

  isJoined: boolean = false;
  joinedGroupId: string[] = [];

  commentForm!: FormGroup;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.postId = id;
        this.loadPost(this.postId);

        this.loadCommentPost(this.postId);
        this.loadPostLike(this.postId);
      }
    });

    this.subscribeAuthState();
    this.checkLike();
    this.subjectService.receivedJoinedCommunitiesId$.pipe().subscribe((joinedGroupId) => {
      this.joinedGroupId = joinedGroupId;
      console.log("Joined Group ID:", this.joinedGroupId);
    });

  }

  checkLike() {
    const checkLike = {
      postId: this.postId,
      userId: this.userId
    }
    this.cPostService.isLikePost(checkLike).subscribe({
      next: (res) => {
         this.isLikePost = true;
         this.isLikePostId = res.data;
          console.log("isLikePost:", res, this.isLikePost);
      },
      error: (error) => {
        this.isLikePost = error.data;
        console.error('Error fetching like data:', error);
      }
    });
  }

  protected override onCurrentUserLoaded(user: any): void {
    if (user != null) {
      this.userId = user.userId;
      this.avatarUrl = this.avatarPath + user.avatarUrl;
    }
  }

  handleLikePost(groupPostId: string, userId: string) {
    if (this.isLikePost) {
      // hủy like
      this.cPostService.deleteLikePost(this.isLikePostId).subscribe({
        next: (res) => {
          this.isLikePost = !this.isLikePost;
          this.isLikePostId = '';
          this.loadPostLike(groupPostId);
        },
        error: (error) => {
          console.error('Error submitting like:', error);
        }
      })
    } else {
      // thực hiện like
      this.cPostService.sentLikePost({ groupPostId, userId }).subscribe({
        next: (res) => {
          this.isLikePost = !this.isLikePost;
          this.isLikePostId = res.data;
          this.loadPostLike(groupPostId);
        },
        error: (error) => {
          console.error('Error submitting like:', error);
        }
      })
    }
  }

  submitComment(): void {
    if (this.joinedGroupId.includes(this.groupId) == false) {
      alert("Bạn cần tham gia nhóm để thực hiện");
      return;
    }
    if (this.commentForm.invalid) {
      console.log('Comment form is invalid');
      return;
    }
    if (this.joinedGroupId.includes(this.groupId) == false) {
      alert('You must join the group to comment');
      return;
    }
    const commentData = {
      groupPostId: this.postData.id,
      userId: this.currentUser.userId,
      content: this.commentForm.get('content')?.value,
      parentCommentId: null
    };
    console.log('Comment Data:', commentData);
    this.cPostService.sentCommentPost(commentData).subscribe({
      next: (res) => {
        console.log('Comment thành công:', res);
        this.commentForm.reset();
        this.loadCommentPost(this.postId);
      }, error: (error) => {
        console.error('Error submitting comment:', error);
      }
    })
  }

  onEnterSubmit(event: any) {
    event.preventDefault(); // tránh reload hoặc double submit
    this.submitComment();
  }

  loadPost(id: any) {
    this.communityService.getCommunityPostDetail(id).subscribe({
      next: (res) => {
        this.postData = res?.data;
        this.groupId = res?.data?.groupId;
        console.log('Post Data:', this.postData);
        console.log("Group ID:", this.groupId);
        this.isJoined = this.joinedGroupId.includes(this.groupId);
        this.loadCommnuity(this.groupId);
      },
      error: (error) => {
        console.error('Error fetching post data:', error);
      }
    }
    );
  }

  loadCommentPost(id: any) {
    this.cPostService.getPostComments(id).subscribe({
      next: (res) => {
        this.comments = res?.data;
        console.log('Comment Data:', res.data);
      },
      error: (error) => {
        console.error('Error fetching comment data:', error);
      }
    });
  }

  loadPostLike(id: any) {
    this.cPostService.getPostLikes(id).subscribe({
      next: (res) => {
        this.likes = res?.data;
        console.log('Like Data:', res.data);
      },
      error: (error) => {
        console.error('Error fetching like data:', error);
      }
    });
  }

  loadCommnuity(id: any) {
    this.communityService.getCommunity(id).subscribe({
      next: (res) => {
        this.groupData = res?.data;
        console.log('Group Data:', this.groupData);
      },
      error: (error) => {
        console.error('Error fetching group data:', error);
      }
    });
  }

  submitJoinGroup(groupId: string, userId: string) {
    console.log("Group ID:", groupId);
    console.log("User ID:", userId);
    this.communityService.addUserToCommunity(groupId, userId).subscribe({
      next: (res) => {
        console.log('Đã tham gia:', res);
        this.isJoined = !this.isJoined;
        this.joinedGroupId.push(groupId);
        this.subjectService.sendJoinedCommunitiesId(this.joinedGroupId);
      },
      error: (error) => {
        console.error('Error joining group:', error);
      }
    });
  }

  cancelJoinGroup(groupId: string, userId: string) {
    console.log("Group ID:", groupId);
    console.log("User ID:", userId);
    this.communityService.cancelUserToCommunity(groupId, userId).subscribe({
      next: (res) => {
        console.log('Đã rời:', res);
        this.isJoined = !this.isJoined;
        this.joinedGroupId = this.joinedGroupId.filter(id => id != groupId);
        this.subjectService.sendJoinedCommunitiesId(this.joinedGroupId);
      },
      error: (error) => {
        console.error('Error joining group:', error);
      }
    });
  }

}
