import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './user/signup/signup.component';
import { DemoComponent } from './demo/demo.component';


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
    {path: 'demo', component: DemoComponent},
    {path: '',component: HomeComponent},
];
