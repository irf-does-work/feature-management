import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { FeatureService } from '../../feature.service';
import { LoginAccept, LoginForm } from '../../interface/feature.interface';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  userForm: FormGroup<LoginForm>;
  isSubmitted: boolean = false;

  constructor(private fb: FormBuilder,
    private router: Router,
    private userService: FeatureService,
    private toastr: ToastrService
  ) {
    this.userForm = this.fb.group({
      email: new FormControl(
        '',
        [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@geekywolf\.com$/)]
      ),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      //const { email, password } = this.userForm.value;
      const userDetails: LoginAccept = {
        email: this.userForm.value.email ?? '',
        password: this.userForm.value.password ?? ''
      }

      this.userService.login(userDetails).subscribe({
        next: (response) => {
          const userId = response.userId;
          // console.log('User logged in successfully:', userId);

          this.router.navigate(['/homepage']);
          this.toastr.success('Welcome back!', 'Login Successful')

          // console.log(this.userService.userId);

        },
        error: (error) => {
          // console.error('Error during login:', error);
          // alert('Invalid login credentials');
          this.toastr.error('Invalid login credentials', 'Login Unsuccessful')
        }
      });
    }
    else {
      this.toastr.error('Invalid login credentials', 'Login Unsuccessful')
    }
  }

  hasDisplayableError(controlName: string): boolean {
    const control = this.userForm.get(controlName);
    return !!control?.invalid && (this.isSubmitted || control?.touched)

  }
}




