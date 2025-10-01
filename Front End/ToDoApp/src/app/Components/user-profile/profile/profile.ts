import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserServices } from '../../../../services/user-services';
import { User } from '../../../../Interfaces/iUser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [ReactiveFormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile {
  constructor(private _userService: UserServices,
    private _router: Router
  ) { }

  user: User = { Name: '', Email: '', Bio: '' }
  editSaveButton: string = "Edit Info"
  isEditing: boolean = false

  profileForm = new FormGroup({
    Name: new FormControl("", [Validators.required]),
    Email: new FormControl("", [Validators.required, Validators.email]),
    Bio: new FormControl("")
  })

  ngOnInit() {

    this.profileForm.disable();
    this._userService.getInfo().subscribe({
      next: (res: any) => {
        this.user = {
          Name: res.name,
          Email: res.email,
          Bio: res.bio
        }
        this.patchValue()
      },
      error: err => alert((err.error.message ?? err.error ?? "Unexpected Error"))
    })
  }

  patchValue() {

    this.profileForm.patchValue({
      Name: this.user.Name,
      Email: this.user.Email,
      Bio: this.user.Bio
    })
  }



  toggleEditSave() {
    if (!this.isEditing) {
      // Switch to edit mode
      this.profileForm.enable();
      this.isEditing = true;
      this.editSaveButton = "Save Changes";
    } else {
      // Save changes
      if (this.profileForm.valid) {
        let updatedUser: User = {
          Name: this.profileForm.get("Name")?.value?.trim() || this.user.Name,
          Email: this.profileForm.get("Email")?.value?.trim() || this.user.Email,
          Bio: this.profileForm.get("Bio")?.value?.trim() || this.user.Bio
        };
        this._userService.updateInfo(updatedUser).subscribe({
          next: (res: any) => {
            this.user = updatedUser,
              this.patchValue()
            this.profileForm.disable()
            this.isEditing = false
            this.editSaveButton = "Edit Info"
            alert(res.message)
          },
          error: err => alert((err.error.message ?? err.error ?? "Unexpected Error"))
        })
      }
    }
  }

  cancelEdit() {
    this.profileForm.patchValue(this.user);
    this.profileForm.disable();
    this.isEditing = false;
    this.editSaveButton = "Edit Info";
  }

  changePassword() {
        console.log("Change password clicked");

    this._router.navigate(['reset-password'])
    
  }
}