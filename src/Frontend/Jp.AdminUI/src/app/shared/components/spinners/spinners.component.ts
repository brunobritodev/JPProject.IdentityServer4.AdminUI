import { Component, OnInit, ViewEncapsulation } from "@angular/core";

@Component({
    selector: "app-loading",
    templateUrl: "./spinners.component.html"
})
export class SpinnersComponent implements OnInit {

    public spinner: number;
    constructor() { }

    ngOnInit() {
        this.spinner = Math.floor(Math.random() * 38);
    }
}
