import { Component } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
@Component({
  templateUrl: '404.component.html',
  providers: [TranslatorService]
})
export class P404Component {

  constructor(public translator: TranslatorService) { }

}
