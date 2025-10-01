import { Component } from '@angular/core';
import { FormControl, FormControlName, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserServices } from '../../../../services/user-services';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css'
})
export class ResetPassword {

  constructor(private _userService: UserServices) { }

  resetPasswordForm = new FormGroup({
    CurrentPassword: new FormControl("", Validators.required),
    NewPassword: new FormControl("", Validators.required)

  })

  resetPassword() {
    if (this.resetPasswordForm.valid) {
      let forgotDTO = {
        currentPassword : this.resetPasswordForm.get("CurrentPassword")?.value,
        newPassword : this.resetPasswordForm.get("NewPassword")?.value
      }

      this._userService.resetPassword(forgotDTO).subscribe({
        next:(res:any)=>
        {
          this.resetPasswordForm.reset()
        },
        error:err => alert(err.error ?? err.message?? "Unexpected error")
      })
    }
  }
}
