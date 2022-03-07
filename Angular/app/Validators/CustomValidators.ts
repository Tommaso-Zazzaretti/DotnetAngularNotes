import { AbstractControl,ValidationErrors } from '@angular/forms';


export function RegexMatchValidator(controlName: string, regex: RegExp){
  	return (formGroup:AbstractControl): ValidationErrors|null => {
    			const control = formGroup.get(controlName);
    			if (control === null) { return null; }
    			if (control.errors && !control.errors['regexMismatch']) { return null; }
    			//Setto un errore di mismatch solo nel form control del campo che DEVE matchare
    			const error = (!(control.value.match(regex)) ? {regexMismatch: true} : null);
    			control.setErrors(error);
  			  return error;
  		};
}
