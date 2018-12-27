import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable({
    providedIn: 'root',
})
export class TranslatorService {

    private defaultLanguage = "en";

    private availablelangs = [
        { code: "en", text: "English" },
        { code: "pt", text: "Portuguese" },
    ];

    constructor(public translate: TranslateService) {

        if (!translate.getDefaultLang())
            translate.setDefaultLang(this.defaultLanguage);

        this.useLanguage(translate.currentLang || this.defaultLanguage);

    }

    useLanguage(lang: string = null) {
        this.translate.use(lang || this.translate.getDefaultLang());
    }

    getAvailableLanguages() {
        return this.availablelangs;
    }

}
