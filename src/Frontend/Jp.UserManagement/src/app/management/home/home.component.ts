import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    providers: [TranslatorService]
})
export class HomeComponent{
    constructor(public translator: TranslatorService) { }
}
