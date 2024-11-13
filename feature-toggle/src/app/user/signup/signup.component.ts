import { booleanAttribute, Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FeatureService } from '../../feature.service';
import { Router, RouterLink } from '@angular/router';
import { ValidatorFn, ValidationErrors,ReactiveFormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [ReactiveFormsModule,RouterLink,CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
  userForm: FormGroup;


  passwordMatchValidator: ValidatorFn = (control: AbstractControl) : null => {
    const password = control.get('password')
    const confirmPassword = control.get('confirmPassword')

    if(password && confirmPassword && password.value != confirmPassword.value)
      confirmPassword?.setErrors({ passwordMismatch: true })
    else
    confirmPassword?.setErrors(null)
  
    return null;
  }

  constructor(private fb: FormBuilder, private userService: FeatureService, private router: Router) {

    this.userForm = this.fb.group<IuseForm>({
      fullName: new FormControl('', Validators.required),
      email: new FormControl('', [
        Validators.required,Validators.email,
        //Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/),
      ]),
      password: new FormControl('', [
        Validators.required, 
        Validators.minLength(6),
        Validators.pattern(/(?=.*[^a-zA-Z0-9])/)
        //Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]),
      // confirmPassword: new FormControl('', [
      //   Validators.required,
      //   Validators.minLength(8),
      //   // Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      // ])

      confirmPassword: new FormControl('')
      ,
    },{validators:this.passwordMatchValidator})
  }


  isSubmitted: boolean = false;


  onSubmit() {
    this.isSubmitted = true;
    if (this.userForm.valid) {
      const { fullName, email, password } = this.userForm.value;

      // Create user object for API
      const userData = {
        name: fullName,
        email: email,
        password: password
      };

      // Call addUser method from the UserService
      this.userService.addUser(userData).subscribe({
        next: (response: any) => {
          console.log('User registered successfully:', response);
          alert("User registered successfully");


          this.router.navigate(['user/login']);
        },
        error: (error: any) => {
          console.error('Error during registration:', error);

        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

  hasDisplayableError(controlName: string ):Boolean {
      const control = this.userForm.get(controlName);
      return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched))

  }
}


interface IuseForm {
  fullName: FormControl<string | null>;
  email: FormControl<string | null>;
  password: FormControl<string | null>;
  confirmPassword: FormControl<string | null>;
}