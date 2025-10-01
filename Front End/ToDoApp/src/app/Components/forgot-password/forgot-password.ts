import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserServices } from '../../../services/user-services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule,],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.css'
})
export class ForgotPassword {

  constructor(private _resetService: UserServices,
    private _router: Router

  ) { }

  resetForm = new FormGroup({
    Email: new FormControl(null, [Validators.required,Validators.email]),
    HashedPassword: new FormControl(null, [Validators.required])
  })

  reset() {
    let user = {
      Email: this.resetForm.get("Email")?.value,
      HashedPassword: this.resetForm.get("HashedPassword")?.value,
    }
    this._resetService.forgotPassword(user).subscribe({
      next: (res:any) => {
        alert(res.message);
        this._router.navigate(["login"])
      },
      error: err => alert((err.error.message ?? err.error ?? "Unexpected Error"))
    })
  }
}
