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

@Component({
  selector: 'app-community',
  imports: [
    RouterOutlet,
    SharedModule,
    CommunityLeftSidebarComponent
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

  constructor(
    private authStateService: AuthStateService,
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
      })
    );

    this.loadCommunities();
    this.loadPosts();
    
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
}
