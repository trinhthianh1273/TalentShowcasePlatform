import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { VideoDetailComponent } from './components/video-detail/video-detail.component';
import { AuthGuard } from './auth.guard';
import path from 'node:path';
import { CommunityHomeComponent } from './components/community/community-home/community-home.component';
import { CommunityPopularComponent } from './components/community/community-popular/community-popular.component';
import { CommunityExploreComponent } from './components/community/community-explore/community-explore.component';
import { CommunityAllComponent } from './components/community/community-all/community-all.component';

export const routes: Routes = [
    { path: '', loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent) },
    { path: 'home', loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent) },
    { path: 'login', loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent) },
    { path: 'register', loadComponent: () => import('./components/register/register.component').then(m => m.RegisterComponent) },
    { path: 'video', loadComponent: () => import('./components/video-detail/video-detail.component').then(m => m.VideoDetailComponent) },
    {
        path: 'library', loadComponent: () => import('./components/user/library.component').then(m => m.LibraryComponent),
    },
    {
        path: 'video-analysis',
        loadComponent: () => import('./components/user/user-video/user-video.component').then(m => m.UserVideoComponent)
    },
    { path: 'profile', loadComponent: () => import('./components/user-profile/user-profile.component').then(m => m.UserProfileComponent) },
    { path: 'profile-edit', loadComponent: () => import('./components/user-profile/edit-profile/edit-profile.component').then(m => m.EditProfileComponent) },
    {
        path: 'community', loadComponent: () => import('./components/community/community.component').then(m => m.CommunityComponent), canActivate: [AuthGuard],  // ✅ Chặn nếu chưa đăng nhập 
        children: [
            { path: '', redirectTo: 'popular', pathMatch: 'full' },
            { path: 'popular', loadComponent: () => import('./components/community/community-popular/community-popular.component').then(m => m.CommunityPopularComponent) },
            { path: 'post/:id', loadComponent: () => import('./components/community/community-post/community-post.component').then(m => m.CommunityPostComponent), canActivate: [AuthGuard] }, // ✅ Chặn nếu chưa đăng nhập
            { path: 'explore', loadComponent: () => import('./components/community/community-explore/community-explore.component').then(m => m.CommunityExploreComponent) },
            { path: 'all', loadComponent: () => import('./components/community/community-all/community-all.component').then(m => m.CommunityAllComponent) },
        ]
    }
];
