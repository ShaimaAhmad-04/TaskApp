import { Routes } from '@angular/router';
import { HomePage } from './Components/HomePage/home-page/home-page';
import { Login } from './Components/LogIn/login/login';
import { ForgotPassword } from './Components/forgot-password/forgot-password';
import { Profile } from './Components/user-profile/profile/profile';
import { ResetPassword } from './Components/reset-password/reset-password/reset-password';
import { Signup } from './Components/signUp/signup/signup';

export const routes: Routes = [
    { path: "", redirectTo: "/login", pathMatch: 'full' },
    { path: 'homePage', component: HomePage },
    { path: 'login', component: Login },
    { path: 'forgot-password', component: ForgotPassword },
    { path: "profile", component: Profile },
    { path: "reset-password", component: ResetPassword },
    { path: "signUp", component: Signup }

];
