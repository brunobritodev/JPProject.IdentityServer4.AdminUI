import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { TooltipModule } from 'ng2-tooltip-directive';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { DatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { RatingModule } from 'ngx-bootstrap/rating';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';

import { InputValidationComponent } from './components/input-validation/input-validation';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { SpinnersComponent } from './components/spinners/spinners.component';
import { CheckallDirective } from './directives/checkall/checkall.directive';
import { EasypiechartDirective } from './directives/easypiechart/easypiechart.directive';
import { FlotDirective } from './directives/flot/flot.directive';
import { JqcloudDirective } from './directives/jqcloud/jqcloud.directive';
import { NowDirective } from './directives/now/now.directive';
import { NumberDirective } from './directives/numberCheck/numbers-only.directive';
import { ScrollableDirective } from './directives/scrollable/scrollable.directive';
import { SparklineDirective } from './directives/sparkline/sparkline.directive';
import { VectormapDirective } from './directives/vectormap/vectormap.directive';
import { ColorsService } from './services/colors.service';


// https://angular.io/styleguide#!#04-10
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        AccordionModule.forRoot(),
        AlertModule.forRoot(),
        ButtonsModule.forRoot(),
        CarouselModule.forRoot(),
        CollapseModule.forRoot(),
        DatepickerModule.forRoot(),
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        ModalModule.forRoot(),
        PaginationModule.forRoot(),
        ProgressbarModule.forRoot(),
        RatingModule.forRoot(),
        TabsModule.forRoot(),
        TimepickerModule.forRoot(),
        TooltipModule,
        PopoverModule.forRoot(),
        TypeaheadModule.forRoot(),
        BsDatepickerModule.forRoot(),
        NgSelectModule
        
    ],
    providers: [
        ColorsService
    ],
    declarations: [
        FlotDirective,
        SparklineDirective,
        EasypiechartDirective,
        CheckallDirective,
        VectormapDirective,
        NowDirective,
        ScrollableDirective,
        JqcloudDirective,
        SpinnersComponent,
        LoadingSpinnerComponent,
        NumberDirective,
        InputValidationComponent
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        RouterModule,
        AccordionModule,
        AlertModule,
        ButtonsModule,
        CarouselModule,
        CollapseModule,
        DatepickerModule,
        BsDatepickerModule,
        BsDropdownModule,
        ModalModule,
        PaginationModule,
        ProgressbarModule,
        RatingModule,
        TabsModule,
        TimepickerModule,
        TooltipModule,
        PopoverModule,
        TypeaheadModule,
        FlotDirective,
        SparklineDirective,
        EasypiechartDirective,
        CheckallDirective,
        VectormapDirective,
        NowDirective,
        ScrollableDirective,
        JqcloudDirective,
        SpinnersComponent,
        LoadingSpinnerComponent,
        InputValidationComponent,
        NumberDirective,
        NgSelectModule
    ]
})

// https://github.com/ocombe/ng2-translate/issues/209
export class SharedModule {
    static forRoot(): ModuleWithProviders<SharedModule> {
        return {
            ngModule: SharedModule
        };
    }
}
