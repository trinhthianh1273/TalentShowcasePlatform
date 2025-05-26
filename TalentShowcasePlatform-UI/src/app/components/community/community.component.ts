import { Component, OnInit } from '@angular/core';
import { share, Subscription } from 'rxjs';
import { SharedModule } from '../../shared/shared.module';
import { Router, RouterOutlet } from '@angular/router';
import { CommunityLeftSidebarComponent } from './community-left-sidebar/community-left-sidebar.component';
import { LoginResponse } from '../../interfaces/interface';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { CommunityService } from '../../services/community/community.service';
import { SubjectService } from '../../services/subject.service';
import { DataService } from '../../services/data.service';
import { CreateCommunityComponent } from './create-community/create-community.component';
import { BaseComponent } from '../base-component/base-component.component';
import { AvatarDropdownComponent } from "../avatar-dropdown/avatar-dropdown.component";

@Component({
  selector: 'app-community',
  imports: [
    RouterOutlet,
    SharedModule,
    CommunityLeftSidebarComponent,
    CreateCommunityComponent,
    AvatarDropdownComponent
],
  templateUrl: './community.component.html',
  styleUrl: './community.component.css'
})
export class CommunityComponent extends BaseComponent implements OnInit {
  communities: any[] = [];

  yourCommnunity: any[] = [];
  JoinedCommunity: any[] = [];

  isCreateCommunityPopupOpen: boolean = false;

  constructor(
    authStateService: AuthStateService,
    private dataService: DataService,
    private subjectService: SubjectService,
    private router: Router,
    private communityService: CommunityService
  ) {
    super(authStateService);
  }

  ngOnInit(): void {
    this.subscribeAuthState();

    this.loadCommunities();
    this.loadPosts();
    this.loadCategory();
  }

  protected override onCurrentUserLoaded(user: any): void {
    this.userId = user?.userId;
    this.avatarUrl = this.avatarPath + user?.avatarUrl;
    console.log("user community: ", this.currentUser);
    this.loadJoinedGroupId(this.userId);
    this.loadYourGroup(this.userId);
    this.loadJoinedGroup(this.userId);
  }

  loadCategory() {
    this.dataService.getCategories().subscribe({
      next: (res) => {
        this.subjectService.sendCategory(res.data);
      }, error: (err) => { }
    })
  }

  loadCommunities() {
    this.communityService.getCommunities().subscribe({
      next: (res) => {
        this.subjectService.sendCommunity(res.data);
        this.communities = res.data;
      }, error: (err) => {

      }
    })
  }

  loadPosts() {
    this.communityService.getCommunityPost().subscribe({
      next: (res) => {
        this.subjectService.sendCommunityPost(res.data);
      }, error: (err) => { }
    })
  }

  loadYourGroup(userId: string) {
    this.communityService.getYourCommunities(userId).subscribe({
      next: (res) => {
        // console.log("Your Group home: ", res.data);
        this.subjectService.sendYourCommunity(res.data);
      },
      error: (err) => {
        console.error("lỗi your group: ", err);
      }
    });
  }

  loadJoinedGroup(userId: string) {
    this.communityService.getJoinedCommunities(userId).subscribe({
      next: (res) => {
        // console.log("Joined Group: ", res.data);
        this.subjectService.sendJoinedCommunity(res.data);
      },
      error: (err) => { }
    });
  }

  loadJoinedGroupId(userId: string) {
    this.communityService.getJoinedCommunitiesId(userId).subscribe({
      next: (res) => {
        this.subjectService.sendJoinedCommunitiesId(res.data);
      },
      error: (err) => { }
    });
  }
  openCreateCommunityPopup() {
    this.isCreateCommunityPopupOpen = true;
  }

  closeCreateCommunityPopup() {
    this.isCreateCommunityPopupOpen = false;
  }
}
