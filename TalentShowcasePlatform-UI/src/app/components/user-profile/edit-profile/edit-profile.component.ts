import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { LoginResponse, UserData } from '../../../interfaces/interface';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { BasicInformationComponent } from './basic-information/basic-information.component';

@Component({
  selector: 'app-edit-profile',
  imports: [
    BasicInformationComponent,
    SharedModule
  ],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  userId: any;
  isLoggedIn: boolean = false;
  currentUser: UserData['data'] | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParamMap.get('id');
  }
}
