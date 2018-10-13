import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { TranslatorService } from "@core/translator/translator.service";
import { Client } from "@shared/viewModel/client.model";

@Component({
    selector: "app-client-auth",
    templateUrl: "./auth.component.html",
    styleUrls: ["./auth.component.scss"],
})
export class ClientAuthComponent implements OnInit {


    @Input()
    public model: Client;
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService) { }


    ngOnInit() {

    }

}
