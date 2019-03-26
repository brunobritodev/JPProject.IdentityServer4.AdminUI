import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Client } from "@shared/viewModel/client.model";
import { TranslatorService } from "@core/translator/translator.service";


@Component({
    selector: "app-client-device-flow",
    templateUrl: "./device-flow.component.html",
    styleUrls: ["./device-flow.component.scss"],
})
export class ClientDeviceFlowComponent implements OnInit {


    @Input()
    public model: Client;
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService) { }


    ngOnInit() {

    }

}
