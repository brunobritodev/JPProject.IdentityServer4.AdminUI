import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { EventsService } from './events.service';
import { EventsComponent } from './list/events.component';

const routes: Routes = [
    { path: "", component: EventsComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        NgxSelectModule,
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
