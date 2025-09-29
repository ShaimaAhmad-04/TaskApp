import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { LoginServices } from '../../../../services/login-services';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  constructor(private _loginServices: LoginServices,
    private _router: Router
  ) {

  }
  loginForm = new FormGroup({
    Email: new FormControl(null, [Validators.required]),
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

      this._loginServices.Login(loginObj).subscribe({
        next: (res: any) => {
          localStorage.setItem("token", res.token)
          alert(res.message)
          this._router.navigate(["homePage"]);

        }
        ,
        error: err => alert(err.message)
      })
    }

  }
}

