import { Routes } from '@angular/router';
import { HomePage } from './Components/HomePage/home-page/home-page';
import { Login } from './Components/LogIn/login/login';
import { ResetPassword } from './Components/forgot-password/reset-password';
import { Profile } from './Components/user-profile/profile/profile';

export const routes: Routes = [
    { path: "", redirectTo: "/login", pathMatch: 'full' },
    { path: 'homePage', component: HomePage },
    { path: 'login', component: Login },
    { path: 'signUp', component: ResetPassword },
    {path: "profile" , component:Profile}

];
