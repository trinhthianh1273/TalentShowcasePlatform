import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { HttpClient } from '@angular/common/http';
import { LoginResponse } from '../../interfaces/interface';
import { AuthService } from '../../services/auth.service';
import { AuthStateService } from '../../services/auth-state.service';

@Component({
  selector: 'app-login',
  imports: [
    SharedModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  @Output() closed = new EventEmitter<void>();
  @Output() loginSuccess = new EventEmitter<LoginResponse['data']>(); // Phát ra chỉ phần data
  isVisible: boolean = false; // Thêm biến trạng thái hiển thị

  // for login
  loginForm!: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;
  LoginResponse!: any;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private authStateService: AuthStateService
  ) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email, Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$')]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)])
    });
  }

  ngOnInit(): void {

  }

  openPopup(): void {
    this.isVisible = true;
  }

  closePopup(): void {
    this.isVisible = false;
    this.closed.emit(); // Phát sự kiện đóng để component cha biết
  }

  get emailControl() {
    return this.loginForm.get('email');
  }

  get passwordControl() {
    return this.loginForm.get('password');
  }

  // Thêm logic xử lý đăng nhập ở đây (ví dụ: gọi service)
  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log("form: ", this.loginForm.value);
      this.isLoading = true;
      //this.http.post<any>('https://localhost:7172/api/auth/login', this.loginForm.value)
      this.authService.authenticationLogin(this.loginForm.value)
        .subscribe({
          next: (response) => {
            console.log("đang thực hiện đăng nhập");
            this.isLoading = false;
            if (response.succeeded && response.data?.token) {
              this.authStateService.setLoggedIn(true); // Cập nhật trạng thái đăng nhập
              this.authStateService.setCurrentUser(response.data); // Cập nhật thông tin người dùng

              this.loginSuccess.emit(response.data); // Phát ra dữ liệu người dùng
              alert(response.messages?.[0] || 'Đăng nhập thành công!'); // Hiển thị thông báo thành công
              localStorage.setItem('authToken', response.data.token);
              localStorage.setItem('currentUser', JSON.stringify(response.data));

              // gửi thông tin user
              //this.SDService.sendUser(response.data);
              this.closePopup();
              window.location.reload();
            } else {
              this.errorMessage = response.messages?.[0] || 'Đăng nhập thất bại. Vui lòng thử lại.';
            }
          },
          error: (error) => {
            this.isLoading = false;
            console.error('Lỗi đăng nhập:', error);
            this.errorMessage = 'Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại sau.';
          }
        });
    } else {

      this.errorMessage = 'Vui lòng kiểm tra lại các trường đã nhập.';
      console.error(this.errorMessage);
    }
  }
}
