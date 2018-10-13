import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";

@Component({
    selector: "app-loading",
    templateUrl: "./spinners.component.html"
})
export class SpinnersComponent implements OnInit {

    public spinner: number;
    constructor(public translator: TranslatorService ) { }

    ngOnInit() {
        this.spinner = Math.floor(Math.random() * 38);
    }
}
