import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthStateService } from '../../../services/auth-state.service';
import { SubjectService } from '../../../services/subject.service';
import { Router } from '@angular/router';
import { CommunityService } from '../../../services/community/community.service';
import { SharedModule } from '../../shared/shared.module';
import { Enviroment } from '../../../../environment';

@Component({
  selector: 'app-community-popular',
  imports: [
    SharedModule
  ],
  templateUrl: './community-popular.component.html',
  styleUrl: './community-popular.component.css'
})
export class CommunityPopularComponent implements OnInit {
  private authSubscription: Subscription | undefined;

  posts: any[] = [];

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;
  groupPostImgUrl = '';
  groupPostPath = Enviroment.groupPostPath;

  joinedGroupId: string[] = [];

  constructor(
    private authStateService: AuthStateService,
    private subjectService: SubjectService,
    private router: Router,
    private communityService: CommunityService
  ) { }
  ngOnInit(): void {
    this.subjectService.receivedCommunityPost$.subscribe((res) => {
      this.posts = res;
      console.log("post community: ", this.posts);
    })

    this.subjectService.receivedJoinedCommunitiesId$.subscribe((joinedGroupId) => {
      this.joinedGroupId = joinedGroupId;
      console.log("Joined Group ID:", this.joinedGroupId);
    });
  }

  gotoPost(postId: string) {
    this.router.navigate(['/community/post', postId]);
  }
}
