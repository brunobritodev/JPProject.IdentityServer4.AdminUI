import { AbstractControl } from '@angular/forms';
import { FormGroup, Validators } from '@ng-stack/forms';


export class FormUtil {

  static errorValidation(formControl: AbstractControl) {
    return formControl.invalid && formControl.touched;
  }

  static errorValidationDirty(formControl: AbstractControl) {
    return formControl.invalid && formControl.touched && formControl.dirty;
  }

  static emptyOrNull(formControl: AbstractControl) {
    return formControl.value === null || formControl.value === '';
  }

  static touchForm(form: FormGroup) {
    form.markAsTouched();
    Object.keys(form.controls).forEach(control => form.controls[control].markAsTouched());
  }

  static dirtyForm(form: FormGroup) {
    form.markAsDirty();
    Object.keys(form.controls).forEach(control => form.controls[control].markAsDirty());
  }

  static updateValidationInput(input: AbstractControl) {
    input.updateValueAndValidity();
    input.markAsDirty();
    input.markAsTouched();
  }

  static requiredEnabled(control: AbstractControl) {
    control.setValidators([ Validators.required ]);
    control.enable();
  }

  static notRequiredEnabled(control: AbstractControl) {
    control.clearValidators();
    control.enable();
  }

  static notRequiredDisabled(control: AbstractControl) {
    control.clearValidators();
    control.disable();
  }

  static isoDateStringToBrDateString(isoDate: string) {
    if (!isoDate || isoDate === '') {
      return '';
    }

    const dateMs = Date.parse(isoDate);
    const date = new Date(dateMs);

    return (
      date.getUTCDate().toString().padStart(2, '0') + '/' +
      (date.getUTCMonth() + 1).toString().padStart(2, '0') + '/' +
      date.getUTCFullYear().toString()
    );
  }

  static brDateStringToIsoDateString(brDate: string) {
    const date = brDate.split('/');

    return date[2] + '-' + date[1] + '-' + date[0];
  }

  static validateForm(form: FormGroup) {
    FormUtil.touchForm(form);
    FormUtil.dirtyForm(form);

    return form.valid;
  }

  static resetValueAndValidators(control: AbstractControl) {
    control.reset();
    control.clearValidators();
    control.updateValueAndValidity();
  }
}
