import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { SubjectService } from '../../../services/subject.service';
import { AuthStateService } from '../../../services/auth/auth-state.service';
import { GroupModel } from '../../../models/GroupModel';
import { CommunityService } from '../../../services/community/community.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Enviroment } from '../../../../environment';
import { SharedModule } from '../../../shared/shared.module';
import { PostCartComponent } from "../post-cart/post-cart.component";
import { CommunityPostService } from '../../../services/community-post/community.post.service';
import { BaseComponent } from '../../base-component/base-component.component';
import { NotificationService } from '../../../services/notifications/notification.service';

@Component({
  selector: 'app-community-group',
  imports: [
    SharedModule,
    PostCartComponent,
],
  templateUrl: './community-group.component.html',
  styleUrl: './community-group.component.css'
})
export class CommunityGroupComponent extends BaseComponent {
  

  isGroupOwner: boolean = false;
  isJoined: boolean = false;

  joinedGroupId: string[] = [];
  GroupData!: GroupModel;
  groupId: string = "";

  ListPost : any[] = [];

  groupPath = Enviroment.groupPath;

  constructor(
    private subjectService: SubjectService,
    authStateService: AuthStateService,
    notiService: NotificationService,
    private postService: CommunityPostService,
    private communityService: CommunityService,
    private route: ActivatedRoute,
    private router : Router
  ) { 
    super(authStateService, notiService);
  }

  ngOnInit() {
    this.subscribeAuthState();
    
    this.subjectService.receivedJoinedCommunitiesId$.subscribe((joinedGroupId) => {
      this.joinedGroupId = joinedGroupId;
      if(this.joinedGroupId.includes(this.groupId)) { 
        this.isJoined = true;
      }
      console.log("Joined Group ID:", this.joinedGroupId);
    });

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.groupId = id;
        // console.log("Group ID from URL:", this.groupId);
        this.loadGroupData(this.groupId);
        this.loadPostGroup(this.groupId);
      }
    });
  }

  gotoCreatePost(groupId: string) {
    console.log("go to create post");
    this.router.navigate([`/community/create-post/${groupId}`]);
  }

  loadGroupData(groupId: any) {
    this.communityService.getCommunity(groupId).subscribe({
      next: (res) => {
        this.GroupData = Array.isArray(res.data) ? res.data[0] : res.data;
        console.log("Group Data:", this.GroupData);
        if(this.GroupData.createdBy == this.currentUser.userId) { 
          this.isGroupOwner = true;
        }
      },
      error: (error) => {
        console.error('Error fetching group data:', error);
      }
    }
    );
  }

  loadPostGroup(groupId: any) { 
    this.postService.getPostByGroup(groupId).subscribe({
      next: (res) => {
        this.ListPost = res?.data;
        console.log("List Post:", this.ListPost);
      },
      error: (error) => {
        console.error('Error fetching group data:', error);
      }
    });
  }
}
