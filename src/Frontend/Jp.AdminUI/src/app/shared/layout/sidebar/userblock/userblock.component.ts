import { Component, OnInit } from "@angular/core";

import { UserblockService } from "./userblock.service";
import { SettingsService } from "../../../../core/settings/settings.service";

@Component({
    selector: "app-userblock",
    templateUrl: "./userblock.component.html",
    styleUrls: ["./userblock.component.scss"]
})
export class UserblockComponent implements OnInit {

    public user: any;

    constructor(public userblockService: UserblockService,
        public settings: SettingsService) {
    }

    ngOnInit() {
        this.settings.getUserProfile().subscribe(a => this.user = a);
    }

    userBlockIsVisible() {
        return this.userblockService.getVisibility();
    }

}
