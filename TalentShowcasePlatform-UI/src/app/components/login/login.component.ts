import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  @Output() closed = new EventEmitter<void>();
  isVisible: boolean = false; // Thêm biến trạng thái hiển thị

  openPopup(): void {
    this.isVisible = true;
  }

  closePopup(): void {
    this.isVisible = false;
    this.closed.emit(); // Phát sự kiện đóng để component cha biết
  }

  // Thêm logic xử lý đăng nhập ở đây (ví dụ: gọi service)
  onSubmit(): void {
    console.log('Đăng nhập...');
    // Sau khi đăng nhập thành công hoặc thất bại, bạn có thể gọi closePopup() hoặc hiển thị thông báo lỗi.
  }
}
