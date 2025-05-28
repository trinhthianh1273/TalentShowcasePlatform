import { Component, EventEmitter, Input, input, OnInit, Output } from '@angular/core';
import { Route } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-aside-left',
  imports: [
    SharedModule
  ],
  templateUrl: './aside-left.component.html',
  styleUrl: './aside-left.component.css'
})
export class AsideLeftComponent implements OnInit {

  @Input() userId: any;
  @Input() isOpen = true;
  @Output() onToggle = new EventEmitter<void>();

  constructor(
  ) { }
  ngOnInit(): void {
  }

  toggleSidebar() {
    this.isOpen = !this.isOpen;
    this.onToggle.emit();
  }

  navigateToLibrary(userId: any) {
    // this.router.navigate(['/user'], { queryParams: { id: userId } });
  }
}

