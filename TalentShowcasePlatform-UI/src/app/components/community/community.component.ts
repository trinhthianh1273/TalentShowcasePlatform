import { Component, OnInit } from '@angular/core';
import { share, Subscription } from 'rxjs';
import { SharedModule } from '../shared/shared.module';
import { Router, RouterOutlet } from '@angular/router';
import { CommunityLeftSidebarComponent } from './community-left-sidebar/community-left-sidebar.component';
import { LoginResponse } from '../../interfaces/interface';
import { AuthStateService } from '../../services/auth-state.service';
import { CommunityService } from '../../services/community/community.service';
import { Enviroment } from '../../../environment';
import { SubjectService } from '../../services/subject.service';
import { DataService } from '../../services/data.service';
import { CreateCommunityComponent } from './create-community/create-community.component';
import { CompletingPopupComponent } from "../popup/completing-popup/completing-popup.component";

@Component({
  selector: 'app-community',
  imports: [
    RouterOutlet,
    SharedModule,
    CommunityLeftSidebarComponent,
    CreateCommunityComponent,
    CompletingPopupComponent
],
  templateUrl: './community.component.html',
  styleUrl: './community.component.css'
})
export class CommunityComponent implements OnInit {
  communities: any[] = [];

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  isLoggedIn: boolean = false;
  userId: any;
  currentUser: LoginResponse['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  yourCommnunity: any[] = [];
  JoinedCommunity: any[] = [];

  isCreateCommunityPopupOpen: boolean = false;

  constructor(
    private authStateService: AuthStateService,
    private dataService: DataService,
    private subjectService: SubjectService,
    private router: Router,
    private communityService: CommunityService
  ) { }

  ngOnInit(): void {
    this.authSubscription = this.authStateService.isLoggedIn$.subscribe(
      (loggedIn) => {
        this.isLoggedIn = loggedIn;
      }
    );

    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe((user) => {
        this.currentUser = user;
        this.userId = user?.userId;
        this.avatarUrl = this.avatarPath + user?.avatarUrl;
        console.log("user community: ", this.currentUser);
        this.loadJoinedGroupId(this.userId);
        this.loadYourGroup(this.userId);
        this.loadJoinedGroup(this.userId);
      })
    );

    this.loadCommunities();
    this.loadPosts();
    this.loadCategory();
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
