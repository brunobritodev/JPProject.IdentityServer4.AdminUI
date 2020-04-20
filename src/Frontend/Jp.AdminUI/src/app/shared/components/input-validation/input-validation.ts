import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@ng-stack/forms';
import { FormUtil } from '@shared/validators/form.utils';


@Component({
  selector: 'app-input-validation',
  templateUrl: "./input-validation.html",
})
export class InputValidationComponent {
    @Input() control: FormControl | FormGroup;
    @Input() errorMsgs = {};
    @Input() errorValidation = FormUtil.errorValidation;
  
    ObjectKeys = Object.keys;
}
