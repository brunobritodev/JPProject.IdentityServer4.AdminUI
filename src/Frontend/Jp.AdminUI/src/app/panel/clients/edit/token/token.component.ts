import { Component, OnInit, Input, ChangeDetectorRef } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Client } from "@shared/viewModel/client.model";
import { TranslatorService } from "@core/translator/translator.service";


@Component({
    selector: "app-client-token",
    templateUrl: "./token.component.html",
    styleUrls: ["./token.component.scss"],
})
export class ClientTokenComponent implements OnInit {


    @Input()
    public model: Client;
    accessTokenTypes: { id: number; text: string; }[];
    refreshTokenUsages: { id: number; text: string; }[];
    tokenExpirations: { id: number; text: string; }[];
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService) { }


    ngOnInit() {
        this.accessTokenTypes = [{ id: 0, text: "Jwt" }, { id: 1, text: "Reference" }];
        this.refreshTokenUsages = [{ id: 0, text: "ReUse" }, { id: 1, text: "One Time Only" }];
        this.tokenExpirations = [{ id: 0, text: "Sliding" }, { id: 1, text: "Absolute" }];
    }

}
