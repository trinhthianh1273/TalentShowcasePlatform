import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-popup',
  imports: [
    SharedModule
  ],
  templateUrl: './popup.component.html',
  styleUrl: './popup.component.css'
})
export class PopupComponent {
  @Input() isOpen: boolean = false;
  @Input() title: string = 'Thông báo';
  @Input() message: string = '';
  @Input() type: 'success' | 'error' | 'info' = 'info';
  @Output() closed = new EventEmitter<void>();

  close() {
    this.closed.emit();
  }

  get iconClass() {
    switch (this.type) {
      case 'success': return 'fas fa-check-circle text-green-500';
      case 'error': return 'fas fa-times-circle text-red-500';
      default: return 'fas fa-info-circle text-gray-500';
    }
  }

  get headerClass() {
    switch (this.type) {
      case 'success': return 'text-green-600';
      case 'error': return 'text-red-600';
      default: return 'text-gray-600';
    }
  }

  get ButtonClass() {
    switch (this.type) {
      case 'success': return ' bg-green-300 ';
      case 'error': return ' bg-red-300 ';
      default: return ' bg-gray-300 ';
    }
  }

  get ButtonHoverClass() {
    switch (this.type) {
      case 'success': return ' hover:bg-green-400 ';
      case 'error': return ' hover:bg-red-400 ';
      default: return ' hover:bg-gray-400 ';
    }
  }
}
