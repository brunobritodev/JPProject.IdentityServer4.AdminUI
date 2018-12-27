import { Component } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';

@Component({
  templateUrl: '500.component.html',
  providers: [TranslatorService]
})
export class P500Component {

  constructor(public translator: TranslatorService) { }

}
