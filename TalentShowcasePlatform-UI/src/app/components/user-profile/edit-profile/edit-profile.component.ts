import { Component, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import {  UserData } from '../../../interfaces/interface';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { BasicInformationComponent } from './basic-information/basic-information.component';
import { HeaderProfileComponent } from "../header-profile/header-profile.component";
import { UserDataModel } from '../../../models/UserDataModel';

@Component({
  selector: 'app-edit-profile',
  imports: [
    BasicInformationComponent,
    SharedModule,
],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  
  userId: any;
  isLoggedIn: boolean = false;
  currentUser: UserDataModel | null = null; // Cập nhật kiểu dữ liệu
  private authSubscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id') ?? '';
  }
}
