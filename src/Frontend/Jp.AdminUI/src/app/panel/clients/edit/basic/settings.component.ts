import { Component, OnInit, Input, ChangeDetectorRef } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Client } from "../../../../shared/viewModel/client.model";
import { TranslatorService } from "../../../../core/translator/translator.service";


@Component({
    selector: "app-client-settings",
    templateUrl: "./settings.component.html",
    styleUrls: ["./settings.component.scss"],
})
export class ClientSettingsComponent implements OnInit {

public items = ['Pizza', 'Pasta', 'Parmesan'];

    @Input()
    public model: Client;
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService) { }


    ngOnInit() {

    }

}
