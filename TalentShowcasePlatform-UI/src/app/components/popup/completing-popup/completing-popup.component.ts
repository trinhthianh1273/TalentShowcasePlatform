import { Component, Input } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-completing-popup',
  imports: [
    SharedModule
  ],
  templateUrl: './completing-popup.component.html',
  styleUrl: './completing-popup.component.css'
})
export class CompletingPopupComponent {
  @Input() isCompletingPopup: any;
  constructor() { }

  closePopup() {
    console.log("đóng popup");
    this.isCompletingPopup = false;
  }
}
