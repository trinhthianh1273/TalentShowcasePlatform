import { Subscription } from 'rxjs';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { Enviroment } from '../../../environment';
import { SubjectService } from '../../services/subject.service';
import { HttpClient } from '@angular/common/http';
import { CurrentUserModel } from '../../models/CurrentUserModel';
// import { AuthStateService } from '../services/auth-state.service';

export abstract class BaseComponent {
  userId!: string;
  isLoggedIn: boolean = false;
  currentUser!: CurrentUserModel;
  protected authSubscription: Subscription = new Subscription();

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  provinces: string[] = [''];

  constructor(
    protected authStateService: AuthStateService
  ) { }

  protected subscribeAuthState() {
    this.authSubscription.add(
      this.authStateService.isLoggedIn$.subscribe(
        (loggedIn) => {
          this.isLoggedIn = loggedIn;
        }
      )
    );
    this.authSubscription.add(
      this.authStateService.currentUser$.subscribe(
        (user) => {
          this.currentUser = user;
          // this.isLoggedIn = true;
          this.onCurrentUserLoaded(user); // gọi hàm này sau khi currentUser đã có giá trị
        }
      )
    );
  }

  // Component con override nếu cần xử lý sau khi currentUser có giá trị
  protected onCurrentUserLoaded(user: any): void {
    if (user != null) {
      this.userId = user.userId;
      this.avatarUrl = this.avatarPath + user.avatarUrl;
    }
  }
}