import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserServices } from '../../../../services/user-services';
import { Router } from '@angular/router';
import { AuthServices } from '../../../../services/auth-services';

@Component({
  selector: 'app-signup',
  imports: [ReactiveFormsModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css'
})
export class Signup {

  constructor(private _authServices: AuthServices,
    private _route: Router
  ) { }

  signUpForm = new FormGroup({
    Name: new FormControl("", [Validators.required]),
    Email: new FormControl("", [Validators.required, Validators.email]),
    Password: new FormControl(null, Validators.required)
  })

  signUp() {
    if (this.signUpForm.valid) {
      let newUser = {
        Name: this.signUpForm.get("Name")?.value,
        Email: this.signUpForm.get("Email")?.value,
        Password: this.signUpForm.get("Password")?.value
      }
      this._authServices.SignUp(newUser).subscribe({
        next: (res: any) => {
          this.signUpForm.reset()
          this._route.navigate(['login'])


        },
        error: err => {
          alert(err.error ?? err.message ?? "Unexpected Error")
        },
      })
    }
  }
}

