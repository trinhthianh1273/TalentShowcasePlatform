import { Component, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserData } from '../../../../interfaces/interface';
import { DataService } from '../../../../services/data.service';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Enviroment } from '../../../../../environment';
import { AuthStateService } from '../../../../services/auth/auth-state.service';

@Component({
  selector: 'app-basic-information',
  imports: [
    SharedModule
  ],
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.css'
})
export class BasicInformationComponent implements OnInit {
  @Input() userId: any;
  @Input() currentUser: UserData['data'] | null = null; // Cập nhật kiểu dữ liệu

  profileForm!: FormGroup;
  isEditing = false;
  initialValues: any;
  provinces: string[] = [''];

  avatarUrl: string = Enviroment.tempAvatarPath;
  avatarPath = Enviroment.avatarPath;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private dataService: DataService,
    private userService: UserService,
    private toastr: ToastrService,
    private authStateService: AuthStateService
  ) {

  }
  ngOnInit(): void {
    this.profileForm = this.fb.group({
      userId: [this.userId],
      fullName: [{ value: '', disabled: true }, Validators.required],
      userName: [{ value: '', disabled: true }, Validators.required],
      email: [{ value: '', disabled: true }, [Validators.required, Validators.email]],
      bio: [{ value: '', disabled: true }, Validators.required],
      location: [{ value: '', disabled: true }, Validators.required]
    });
    this.profileForm.disable(); // ban đầu là readonly
    this.loadUserData();

    this.http.get<any[]>('https://provinces.open-api.vn/api/p/')
      .subscribe(data => {
        this.provinces = data.map(p => p.name); // Gán tên các tỉnh vào danh sách
      });
    console.log("tỉnh: ", this.provinces);
  }

  loadUserData() {
    this.dataService.getUserById(this.userId)
      .subscribe((response: any) => {
        if (response && response.data) {
          this.currentUser = response.data;
          console.log("user: ", this.currentUser);

          this.avatarUrl = this.avatarPath + this.currentUser?.avatarUrl;
          this.profileForm.patchValue({
            fullName: this.currentUser?.fullName,
            userName: this.currentUser?.userName,
            email: this.currentUser?.email,
            bio: this.currentUser?.bio,
            location: this.currentUser?.location
          });
          this.initialValues = this.profileForm.getRawValue(); // Lưu giá trị ban đầu
        }
      });
  }

  enableEdit() {
    this.isEditing = true;
    this.profileForm.enable();
    console.log("cho sửa: ", this.isEditing);
  }

  cancelEdit() {
    this.isEditing = false;
    this.profileForm.patchValue(this.initialValues);
    this.profileForm.disable();
    console.log("đóng sửa");
  }

  get fullNameControl() {
    return this.profileForm.get('fullName');
  }

  get userNameControl() {
    return this.profileForm.get('userName');
  }

  get bioControl() {
    return this.profileForm.get('bio');
  }

  get locationControl() {
    return this.profileForm.get('location');
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const avatarData = new FormData();
      avatarData.append('File', file); // phải viết đúng tên property trong UploadAvatarCommand
      avatarData.append('UserId', this.userId); // giống như tên field trong C#

      console.log("avatar data: ", avatarData);

      this.userService.changeAvatar(this.userId, avatarData)
        .subscribe({
          next: (res) => {
            this.authStateService.setCurrentUser(res.data);
            console.log("new user: ", res.data);
            this.avatarUrl = this.avatarPath + res.data?.avatarUrl; // cập nhật UI
            this.toastr.success('Avatar updated successfully');
          },
          error: (err) => {
            this.toastr.error('Failed to update avatar')
            console.log(err);
          }
        });
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      const updatedUser = {
        id: this.profileForm.get('userId')?.value,
        userName: this.profileForm.get('userName')?.value,
        fullName: this.profileForm.get('fullName')?.value,
        email: this.profileForm.get('email')?.value,
        bio: this.profileForm.get('bio')?.value,
        location: this.profileForm.get('location')?.value
      };

      console.log('Form submitted:', this.profileForm.value);
      this.userService.updateProfile(this.userId, updatedUser).subscribe({
        next: (response) => {
          console.log("user response: ", response);
          if (response.succeeded == true) {
            this.authStateService.setCurrentUser(response.data);
            this.loadUserData();
            this.isEditing = false;
            this.profileForm.disable();
            this.toastr.success('Cập nhật thành công!', 'Thành công');

          } else {
            this.toastr.error(response.messages, 'Lỗi');
            console.error('Error updating profile:', response.messages);
          }

        }, error: (error) => {
          console.error('Error updating profile:', error?.error);
          this.toastr.error('Cập nhật thất bại!', 'Lỗi');
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

}
