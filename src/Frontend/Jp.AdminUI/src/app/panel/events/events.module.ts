import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@shared/shared.module';
import { ToasterModule } from 'angular2-toaster/src/toaster.module';
import { TagInputModule } from 'ngx-chips';

import { EventsService } from './events.service';
import { EventsComponent } from './list/events.component';

const routes: Routes = [
    { path: "", component: EventsComponent },
];

@NgModule({
    imports: [
        SharedModule,
        ToasterModule.forRoot(),
        RouterModule.forChild(routes),
        TagInputModule,
        NgbPaginationModule
    ],
    declarations: [
        EventsComponent,
    ],
    providers: [
        EventsService
    ],
    exports: [
        RouterModule
    ]
})
export class EventsModule { }
