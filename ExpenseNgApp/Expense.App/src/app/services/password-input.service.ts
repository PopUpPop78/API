import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PasswordInputService {
 
  isPassword: boolean = true;
  eyeSlash: string = 'fa-eye-slash';
  eye: string = 'fa-eye';
  textType = 'text';
  passwordType = 'password';

  // exposed
  public inputType: string = this.passwordType;
  public eyeClass: string = this.eyeSlash;

  constructor() {

   }

   public togglePasswordEye(){
    this.isPassword = !this.isPassword;
    this.inputType = this.isPassword ? this.passwordType : this.textType;
    this.eyeClass = this.isPassword ? this.eyeSlash : this.eye;    
   }
}
