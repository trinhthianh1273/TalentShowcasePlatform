import { Component, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AuthStateService } from '../../../services/auth-state.service';
import { Enviroment } from '../../../../environment';

@Component({
  selector: 'app-header-profile',
  imports: [
    SharedModule
  ],
  templateUrl: './header-profile.component.html',
  styleUrl: './header-profile.component.css'
})
export class HeaderProfileComponent implements OnInit {
  
  @Input() isLoggedIn!: boolean;
  @Input() currentUser!: any;

  // for login
  isLoginPopupVisible: boolean = false;

  avatarUrl: string = '';
  avatarPath = Enviroment.avatarPath;
  tempAvatarPath = Enviroment.tempAvatarPath;

  constructor(
    private authStateService: AuthStateService
  ) {}

  ngOnInit(): void {
    if(this.currentUser?.avatarUrl) {
      this.avatarUrl = this.avatarPath + this.currentUser.avatarUrl;
    }
  }
}
