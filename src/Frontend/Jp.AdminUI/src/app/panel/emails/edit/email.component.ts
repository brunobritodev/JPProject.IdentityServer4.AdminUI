import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { EmailService } from '@app/emails/emails.service';
import { TranslatorService } from '@core/translator/translator.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Bcc, Email, Sender } from '@shared/viewModel/email.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import * as CodeMirror from 'codemirror';
import { Observable, Subject } from 'rxjs';


@Component({
    selector: "app-email",
    templateUrl: "./email.component.html",
    styleUrls: ["./email.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class EmailComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 60000
    });

    editorConfig: AngularEditorConfig = {
        editable: true,
        sanitize: false
    };

    emailTypes$: Observable<Array<any>>;
    showButtonLoading: boolean;


    @ViewChild('editor', { static: true }) editor: any;
    instance: any;
    linkForThemes: any = null;
    editorThemes = ['3024-day', '3024-night', 'ambiance-mobile', 'ambiance', 'base16-dark', 'base16-light', 'blackboard', 'cobalt', 'eclipse',
     'elegant', 'erlang-dark', 'lesser-dark', 'mbo', 'mdn-like', 'midnight', 'monokai', 'neat', 'neo', 'night', 'paraiso-dark', 'paraiso-light', 'pastel-on-dark', 'rubyblue', 'solarized', 'the-matrix', 'tomorrow-night-eighties', 'twilight', 'vibrant-ink', 'xq-dark', 'xq-light'];
    editorOpts = {
        mode: 'htmlmixed',
        lineNumbers: true,
        matchBrackets: true,
        theme: 'mbo',
        viewportMargin: Infinity
    };

    public errors: Array<string>;

    public selectedType: string;
    public model: Email;
    constructor(
        public translator: TranslatorService,
        private emailService: EmailService,
        public toasterService: ToasterService) { }

    ngOnInit() {
        this.errors = [];
        this.model = new Email();
        this.emailTypes$ = this.emailService.getEmailTypes();

        this.instance = CodeMirror.fromTextArea(this.editor.nativeElement, this.editorOpts);
        this.updateEditor();
        this.instance.on('change', () => {
            this.model.content = this.instance.getValue();
        });
        this.loadTheme(); // load default theme
    }

    updateEditor() {
        setTimeout(() => {
            this.instance.setValue(this.model.content);
        }, 500);
    }


    createCSS(path) {
        let link = document.createElement('link');
        link.href = path;
        link.type = 'text/css';
        link.rel = 'stylesheet';
        link.id = 'cm_theme';

        return document.getElementsByTagName('head')[0].appendChild(link);
    }

    public getEmailTemplate($event: any) {
        this.emailService.getEmail($event.value).subscribe(s => {
            if (s.bcc == null)
                s.bcc = new Bcc();

            if (s.sender == null)
                s.sender = new Sender();
            this.model = s;
            this.updateEditor();
            this.editorOpts.mode = "htmlmixed";
            this.errors = [];
        },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
            });
    }

    public update() {
        this.showButtonLoading = true;

        this.emailService.update(this.selectedType, this.model).subscribe(
            () => {
                this.showSuccessMessage();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    loadTheme() {
        let themesBase = 'assets/codemirror/theme/';

        if (!this.linkForThemes) {
            this.linkForThemes = this.createCSS(themesBase + this.editorOpts.theme + '.css');
        }
        else {
            this.linkForThemes.setAttribute('href', themesBase + this.editorOpts.theme + '.css');
        }
        this.instance.setOption('theme', this.editorOpts.theme);
    }

}
