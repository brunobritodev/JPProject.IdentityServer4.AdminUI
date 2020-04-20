import { APP_BASE_HREF } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, TestBed } from '@angular/core/testing';
import { TranslateModule } from '@ngx-translate/core';
import { ToasterModule } from 'angular2-toaster';

import { AppComponent } from './app.component';
import { RoutesModule } from './app.routing.module';
import { CoreModule } from './core/core.module';
import { LayoutModule } from './shared/layout/layout.module';
import { SharedModule } from './shared/shared.module';

/* tslint:disable:no-unused-variable */

describe("App: JpProject", () => {
    beforeEach(() => {

        jasmine.DEFAULT_TIMEOUT_INTERVAL = 60000;

        TestBed.configureTestingModule({
            declarations: [
                AppComponent
            ],
            imports: [
                HttpClientTestingModule,
                TranslateModule.forRoot(),
                CoreModule,
                LayoutModule,
                SharedModule,
                ToasterModule.forRoot(),
                RoutesModule
            ],
            providers: [
                { provide: APP_BASE_HREF, useValue: "/" }
            ]
        });
    });

    it("should create the app", async(() => {
        let fixture = TestBed.createComponent(AppComponent);
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

});
