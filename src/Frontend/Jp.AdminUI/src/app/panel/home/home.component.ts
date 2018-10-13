import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../core/translator/translator.service";


@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {

    

    constructor(public translator: TranslatorService) { }

    ngOnInit() {
    }

}
