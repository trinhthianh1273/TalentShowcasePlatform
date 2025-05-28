import { Subscription } from 'rxjs';
import { Enviroment } from '../../../environment';
import { CommunityService } from '../../services/community/community.service';
import { GroupModel } from '../../models/GroupModel';
import { BaseComponent } from './base-component.component';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { NotificationService } from '../../services/notifications/notification.service';
// import { AuthStateService } from '../services/auth-state.service';

export abstract class BaseCommnunityComponent extends BaseComponent {
    groupData!: GroupModel;
    groupId: string = "";
    groupPath = Enviroment.groupPath;

    constructor(
        authStateService: AuthStateService,
        protected communityService: CommunityService,
        notiService: NotificationService
    ) { 
        super(authStateService, notiService);
    }

    protected subscribeGroupData(groupId: any) {
        this.communityService.getCommunity(groupId).subscribe({
            next: (res) => {
                this.groupData = Array.isArray(res.data) ? res.data[0] : res.data;
                console.log('Group Data:', this.groupData);
            },
            error: (error) => {
                console.error('Error fetching group data:', error);
            }
        }
        );
    }

    protected onGroupDataLoaded(user: any): void {

    }

}