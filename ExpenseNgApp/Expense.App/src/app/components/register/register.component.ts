import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import ValidateForm from 'src/app/helpers/validate-form';
import { ServerModelValidationErrors } from 'src/app/models/server-model-validation-errors';
import { AuthService } from 'src/app/services/auth.service';
import { PasswordInputService } from 'src/app/services/password-input.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  public fg!: FormGroup;
  public isRegistering: boolean = false;

  constructor(private auth: AuthService, 
    private route: Router,
    private fb: FormBuilder,
    public passwordInput: PasswordInputService,
    private toast: ToastrService){

  }

  ngOnInit(){
    this.fg = this.fb.group({
      firstName:['', Validators.required],
      lastName:['', Validators.required],
      email:['', Validators.required],
      password:['', Validators.required]
    })
  }

  register(){
    
    // Clear any server errors
    ValidateForm.clearServerValidationErrors(this.fg);
    
    if(this.fg.valid){
      this.isRegistering = true;
      this.auth.register(this.fg.value).subscribe({
        next: (res) => {
          this.toast.success('Successful', 'Registation');

          // Route to login
          this.route.navigate(['login']);
        },
        error: (err) => {
          if(err instanceof ServerModelValidationErrors){
            ValidateForm.applyServerValidationErrors(this.fg, err);
          } else {
            this.toast.error(JSON.stringify(err));
          }
        }
      })
      .add(() => this.isRegistering = false);
      
    } else {
      ValidateForm.validateAllForm(this.fg);
    }
  }

}
