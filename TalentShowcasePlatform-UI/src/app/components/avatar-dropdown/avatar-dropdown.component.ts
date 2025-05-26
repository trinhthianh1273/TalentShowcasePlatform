import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from '../base-component/base-component.component';
import { AuthStateService } from '../../services/auth/auth-state.service';
import { Router } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-avatar-dropdown',
  imports: [
    SharedModule
  ],
  templateUrl: './avatar-dropdown.component.html',
  styleUrl: './avatar-dropdown.component.css'
})
export class AvatarDropdownComponent extends BaseComponent implements OnInit {
  popupOpen = false;
  @ViewChild('popupWrapper', { static: false }) popupWrapper!: ElementRef;
  constructor(
    authStateService: AuthStateService,
    private router: Router
  ) { super(authStateService); }
  ngOnInit(): void {
    this.subscribeAuthState();
  }
  togglePopup() {
    this.popupOpen = !this.popupOpen;
  }

  logout(): void {

    this.authStateService.logout();
    this.router.navigate(['/']);
    window.location.reload(); // Hoặc phương pháp điều hướng khác}
  }
}
