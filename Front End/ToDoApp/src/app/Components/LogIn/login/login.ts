import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { AuthServices } from '../../../../services/auth-services';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  constructor(private _authServices: AuthServices,
    private _router: Router
  ) {

  }
  loginForm = new FormGroup({
    Email: new FormControl(null, [Validators.required, Validators.email]),
    Password: new FormControl(null, [Validators.required])
  })

  login() {
    if (this.loginForm.valid) {
      let Email = this.loginForm.get('Email')?.value
      let Password = this.loginForm.get("Password")?.value

      let loginObj = {
        email: Email,
        password: Password
      }

      this._authServices.Login(loginObj).subscribe({
        next: (res: any) => {
          localStorage.setItem("token", res.token)
          this._router.navigate(["homePage"]);

        }
        ,
        error: (err: any) => alert(err.error.message ?? err.error ?? "Unexpected Error")

      })
    }

  }
  signUpPage() {
    this._router.navigate(["signUp"])
  }
}

