import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SharedModule } from './components/shared/shared.module';
import { AuthStateService } from './services/auth-state.service';
import { ToastrModule } from 'ngx-toastr';


@Component({
  selector: 'app-root',
  imports: [
     
    RouterOutlet,
    SharedModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'TalentShowcasePlatform-UI';
  constructor(
    private authStateService: AuthStateService
  ) { }
  ngOnInit(): void {
    this.authStateService.isLoggedIn$.subscribe(loggedIn => {
      // console.log('Trạng thái đăng nhập (AppComponent):', loggedIn);
    });

    this.authStateService.currentUser$.subscribe(user => {
      // console.log('Thông tin người dùng (AppComponent):', user);
    });
  }
}
