import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Client } from "@shared/viewModel/client.model";
import { TranslatorService } from "@core/translator/translator.service";
import { Observable } from "rxjs";
import { ScopeService } from "@shared/services/scope.service";
import { map, debounceTime } from "rxjs/operators";



@Component({
    selector: "app-client-settings",
    templateUrl: "./settings.component.html",
    styleUrls: ["./settings.component.scss"],
    providers: [ScopeService]
})
export class ClientSettingsComponent implements OnInit {

    @Input()
    public model: Client;

    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService,
        private scopeService: ScopeService
    ) { }

    public requestScopeItems = (text: string): Observable<string[]> => {
        return this.scopeService.getScopes(text).pipe(debounceTime(500)).pipe(map(a => a.data));
    }

    public addGrantType(type: string) {
        if (this.model.allowedGrantTypes.find(a => a == type) == null)
            this.model.allowedGrantTypes.push(type);
    }

    ngOnInit() {

    }

}
