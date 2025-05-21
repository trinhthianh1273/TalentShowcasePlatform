import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { SubjectService } from '../../../services/subject.service';
import { AuthStateService } from '../../../services/auth-state.service';
import { GroupModel } from '../../../models/GroupModel';
import { CommunityService } from '../../../services/community/community.service';
import { ActivatedRoute } from '@angular/router';
import { Enviroment } from '../../../../environment';
import { SharedModule } from '../../shared/shared.module';
import { PostCartComponent } from "../post-cart/post-cart.component";
import { CommunityPostService } from '../../../services/community-post/community.post.service';

@Component({
  selector: 'app-community-group',
  imports: [
    SharedModule,
    PostCartComponent,
],
  templateUrl: './community-group.component.html',
  styleUrl: './community-group.component.css'
})
export class CommunityGroupComponent {
  isLoggedIn: boolean = false;
  currentUser: any = null;
  private authSubscription: Subscription | undefined;

  isGroupOwner: boolean = false;
  isJoined: boolean = false;

  joinedGroupId: string[] = [];
  GroupData!: GroupModel;
  groupId: string = "";

  ListPost : any[] = [];

  groupPath = Enviroment.groupPath;

  constructor(
    private subjectService: SubjectService,
    private authStateService: AuthStateService,
    private postService: CommunityPostService,
    private communityService: CommunityService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
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

  loadGroupData(groupId: any) {
    this.communityService.getCommunity(groupId).subscribe({
      next: (res) => {
        this.GroupData = res?.data;
        console.log("Group Data:", this.GroupData);
        if(this.GroupData.createdBy == this.currentUser?.userId) { 
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
