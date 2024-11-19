import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import { FeatureService } from '../feature.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  constructor(private router : Router,
    private toastr: ToastrService,
    private authService : FeatureService
  ) {}

  onLogout(){
    // localStorage.removeItem('token');
    this.authService.deleteToken();
    this.router.navigate(['/user/login']);
    this.toastr.success('See you later!', 'Logout Successful');
  }
}
