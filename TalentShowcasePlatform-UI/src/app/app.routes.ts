import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { VideoDetailComponent } from './components/video-detail/video-detail.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'video', loadComponent: () => import('./components/video-detail/video-detail.component').then(m => m.VideoDetailComponent) },
    {
        path: 'user', loadComponent: () => import('./components/user/user.component').then(m => m.UserComponent),
    },
    {
        path: 'video-analysis',
        loadComponent: () => import('./components/user/user-video/user-video.component').then(m => m.UserVideoComponent)
    },
    { path: 'profile', loadComponent: () => import('./components/user-profile/user-profile.component').then(m => m.UserProfileComponent) },
    { path: 'profile-edit', loadComponent: () => import('./components/user-profile/edit-profile/edit-profile.component').then(m => m.EditProfileComponent) }
];
