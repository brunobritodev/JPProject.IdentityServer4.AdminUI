import { AbstractControl, ValidationErrors } from '@angular/forms';

export class PasswordValidator {

  static validator(control: AbstractControl): ValidationErrors {
    const passwordErrors: { [key: string]: any } = {};

    const numbersCharsRegex = new RegExp(/[\d]/g);
    const upperLettersRegex = new RegExp(/[A-Z]/g);
    const lowerLettersRegex = new RegExp(/[a-z]/g);
    const specialCharsRegex = new RegExp(/[!@#$%^&*()_\-\[\]{};'~`:"\\|,.<>\/?]/g);
    const forbiddenCharsRegex = new RegExp(/[^0-9A-Za-z!@#$%^&*()_\-\[\]{};'~`:"\\|,.<>\/?]/g);

    if (control.value != null && control.value.length < 8) { passwordErrors['minlength'] = true; }
    //if (!numbersCharsRegex.test(control.value)) { passwordErrors['pwNotHaveNumber'] = true; }
    //if (!upperLettersRegex.test(control.value)) { passwordErrors['pwNotHaveCapitalCase'] = true; }
    //if (!lowerLettersRegex.test(control.value)) { passwordErrors['pwNotHaveSmallCase'] = true; }
    //if (!specialCharsRegex.test(control.value)) { passwordErrors['pwNotHaveSpecialCharacters'] = true; }
    //if (forbiddenCharsRegex.test(control.value)) { passwordErrors['pwHaveForbiddenCharacters'] = true; }


    if (Object.keys(passwordErrors).length) {
      passwordErrors['password'] = true;
      return passwordErrors;
    }

    return null;
  }
}
