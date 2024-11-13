import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import path from 'path';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { DrawerComponent } from './drawer/drawer.component';
import { SignupComponent } from './user/signup/signup.component';


export const routes: Routes = [
    {
        path:'user',
        component: UserComponent,
        children:[
            {path: 'login', component: LoginComponent},
            {path: 'signup', component: SignupComponent}
        ]
        
    },
    {path: 'home', component: HomeComponent},
    {path: 'drawer', component: DrawerComponent}
];
