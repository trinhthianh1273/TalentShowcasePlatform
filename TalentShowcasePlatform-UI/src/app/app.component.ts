import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SharedModule } from './components/shared/shared.module';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    SharedModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'TalentShowcasePlatform-UI';
}
