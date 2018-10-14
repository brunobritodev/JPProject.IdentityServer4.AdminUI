import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../core/translator/translator.service";
import { environment } from "@env/environment";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
    public env = environment;
    constructor(public translator: TranslatorService) { }

    ngOnInit() {
    }

}
