import { ValidationErrors, AbstractControl } from '@angular/forms';

export class EqualToValidator {

  static validator(equalToControl: AbstractControl | string) {
    const controlName = equalToControl;

    return (control: AbstractControl): ValidationErrors => {
      if (typeof equalToControl === 'string') {
        // 'Parent' de 'control' vai ser nulo durante a inicialização da página
        if (!control.parent) { return null; }

        equalToControl = control.parent.get(equalToControl);
      }

      if ((controlName === 'userEmail1') && control.value !== equalToControl.value) { return { equalToEmail: true }; }
      if ((controlName === 'password') && control.value !== equalToControl.value) { return { equalToPassword: true }; }
      return null;
    };
  }
}
