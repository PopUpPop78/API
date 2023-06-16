import { AbstractControl, FormControl, FormGroup } from "@angular/forms";
import { ServerModelValidationErrors } from "../models/server-model-validation-errors";

export default class ValidateForm{
  
  public static validateAllForm(form: FormGroup){
    Object.keys(form.controls).forEach(field => {
      const ctrl = form.get(field);
      if(ctrl instanceof FormControl){
        ctrl.markAsDirty({onlySelf:true});
      } else if(ctrl instanceof FormGroup){
        this.validateAllForm(ctrl);
      }
    })
  }

  public static applyServerValidationErrors(form: FormGroup, serverErrors: ServerModelValidationErrors) {
    Object.keys(form.controls).forEach(field => {

      const errors = serverErrors.serverValidationErrors.find(x=> x.key.includes(field.toLowerCase()))?.error;
      errors?.forEach(console.log);

      form.get(field)?.setErrors({'serverErrors': errors});
    });
  }

  public static clearServerValidationErrors(form: FormGroup) {
    Object.keys(form.controls).forEach(field => {

      let ctrl = form.get(field)!;
      ctrl.setErrors(null);
      ctrl.updateValueAndValidity();

    })

    form.updateValueAndValidity();
  }
}