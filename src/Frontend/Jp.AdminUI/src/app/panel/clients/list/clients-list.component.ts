import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";


@Component({
    selector: "app-clients-list",
    templateUrl: "./clients-list.component.html",
    styleUrls: ["./clients-list.component.scss"]
})
export class ClientListComponent implements OnInit {
    constructor(public translator: TranslatorService) { }

    ngOnInit() {
    }

}
