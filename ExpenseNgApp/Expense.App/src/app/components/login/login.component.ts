import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TOAST_CONFIG, ToastrService } from 'ngx-toastr';
import ValidateForm from 'src/app/helpers/validate-form';
import { ServerModelValidationErrors } from 'src/app/models/server-model-validation-errors';
import { LoginUser } from 'src/app/models/user/login-user';
import { AuthService } from 'src/app/services/auth.service';
import { PasswordInputService } from 'src/app/services/password-input.service';
import { UserStoreService } from 'src/app/services/user-store.service';
import { ElementFlags } from 'typescript';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  public isLoggingIn: boolean = false;
  public fg!: FormGroup;

  constructor(private auth:AuthService, 
    private route: Router, 
    private fb: FormBuilder,
    public passwordInput: PasswordInputService,
    private toast: ToastrService,
    private userStore: UserStoreService){
    
  }

  ngOnInit() : void {

    // clear any tokens
    localStorage.clear();

    // Set up form
    this.fg = this.fb.group({
      email:['', Validators.required],
      password:['', Validators.required]
    });
  }

  onSubmit() {

    // Clear any server errors
    ValidateForm.clearServerValidationErrors(this.fg);

    if(this.fg.valid){
      this.isLoggingIn = true;
      this.auth.login(this.fg.value).subscribe({
        next: (res) => {

          this.fg.reset();

          // Set tokens
          this.auth.setToken(res.token);
          this.auth.setRefreshToken(res.refreshToken);

          const token = this.auth.decodedJwt();
          this.userStore.setLastFirstName(token.lastFirstName);
          this.userStore.setRole(token.roles);
          
          this.toast.success('Successful', 'Login');

          // Route to dashboard
          this.route.navigate(['dashboard'])
        },
        error: (err) => {
          if(err instanceof ServerModelValidationErrors){
            ValidateForm.applyServerValidationErrors(this.fg, err);
          } else {
            this.toast.error('Please check username / password');
          }
        }
      })
      .add(() => this.isLoggingIn = false);
    } else {
      ValidateForm.validateAllForm(this.fg);
    }
  }
}
