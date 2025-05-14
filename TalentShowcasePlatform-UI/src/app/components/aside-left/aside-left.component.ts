import { Component, Input, input } from '@angular/core';
import { Route } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

@Component({
  selector: 'app-aside-left',
  imports: [
    SharedModule
  ],
  templateUrl: './aside-left.component.html',
  styleUrl: './aside-left.component.css'
})
export class AsideLeftComponent {

  @Input() userId: any;

  constructor(
    // private router: Route
  ) { }

  navigateToLibrary(userId: any) {
    // this.router.navigate(['/user'], { queryParams: { id: userId } });
  }
}

