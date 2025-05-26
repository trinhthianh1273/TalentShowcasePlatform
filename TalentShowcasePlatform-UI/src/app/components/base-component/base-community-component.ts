import { Subscription } from 'rxjs';
import { Enviroment } from '../../../environment';
import { CommunityService } from '../../services/community/community.service';
import { GroupModel } from '../../models/GroupModel';
import { BaseComponent } from './base-component.component';
import { AuthStateService } from '../../services/auth/auth-state.service';
// import { AuthStateService } from '../services/auth-state.service';

export abstract class BaseCommnunityComponent extends BaseComponent {
    groupData!: GroupModel;
    groupId: string = "";
    groupPath = Enviroment.groupPath;

    constructor(
        authStateService: AuthStateService,
        protected communityService: CommunityService
    ) { 
        super(authStateService);
    }

    protected subscribeGroupData(groupId: any) {
        this.communityService.getCommunity(groupId).subscribe({
            next: (res) => {
                this.groupData = res?.data;
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