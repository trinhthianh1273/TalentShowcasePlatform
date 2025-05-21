import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { SubjectService } from '../../../services/subject.service';
import { Enviroment } from '../../../../environment';

@Component({
  selector: 'app-community-left-sidebar',
  imports: [
    SharedModule
  ],
  templateUrl: './community-left-sidebar.component.html',
  styleUrl: './community-left-sidebar.component.css'
})
export class CommunityLeftSidebarComponent {

  YourCommunity: any[] = [];
  JoinedCommunity: any[] = [];
  groupPath = Enviroment.groupPath;
  
  constructor(
    private subjectService : SubjectService
  ) {  }

  ngOnInit(): void {
    this.subjectService.receivedYourCommunity$.subscribe((data) => {
      this.YourCommunity = data;
      // console.log("YourCommunity side bar: ", this.YourCommunity);
    });
    this.subjectService.receivedJoinedCommunity$.subscribe((data) => {
      this.JoinedCommunity = data;
    });
  }

  
}
