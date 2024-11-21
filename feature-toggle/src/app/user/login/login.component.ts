import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { FeatureService } from '../../feature.service';
import { ILoginAccept, ILoginForm } from '../../interface/feature.interface';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  userForm: FormGroup<ILoginForm>;
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

  ngOnInit(): void {
    if(this.userService.isLoggedIn()){
      this.router.navigate(['/home']);
    }
  }

  onSubmit() {
    if (this.userForm.valid) {
      const userDetails : ILoginAccept = {
        email: this.userForm.value.email ?? '',
        password: this.userForm.value.password ?? ''
      }

      this.userService.login(userDetails).subscribe({
        next: (response:any) => {
          console.log(response);
          // localStorage.setItem('token',response.token);
          this.userService.saveToken(response.token);

          this.router.navigate(['/home']);
          this.toastr.success('Welcome back!', 'Login Successful')


        },
        error: (error) => {
          if(error.status === 400){
          this.toastr.error('Invalid login credentials', 'Login failed')
          }
          else{
            // console.log(error);
            this.toastr.error('Something went wrong','Login failed')
          }
        }
      });
    }
    else {
      this.toastr.error('Something went wrong','Login failed')
    }
  }

  hasDisplayableError(controlName: string): boolean {
    const control = this.userForm.get(controlName);
    return !!control?.invalid && (this.isSubmitted || control?.touched)

  }
}



