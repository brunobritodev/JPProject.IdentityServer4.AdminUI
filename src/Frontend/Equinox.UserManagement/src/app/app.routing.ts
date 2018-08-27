import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// Import Containers
import { DefaultLayoutComponent } from "./core";

import { P404Component } from "./views/error/404.component";
import { P500Component } from "./views/error/500.component";
import { PagesModule } from "./pages/pages.module";
import { AuthGuard } from "./core/auth/auth.guard";


export const routes: Routes = [
  {
    path: "404",
    component: P404Component,
    data: {
      title: "Page 404"
    }
  },
  {
    path: "500",
    component: P500Component,
    data: {
      title: "Page 500"
    }
  },
  {
    path: "",
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard],
    data: {
      title: "Home"
    },
    children: [
      {
        path: "base",
        loadChildren: "./views/base/base.module#BaseModule"
      },
      {
        path: "buttons",
        loadChildren: "./views/buttons/buttons.module#ButtonsModule"
      },
      {
        path: "charts",
        loadChildren: "./views/chartjs/chartjs.module#ChartJSModule"
      },
      {
        path: "dashboard",
        loadChildren: "./views/dashboard/dashboard.module#DashboardModule"
      },
      {
        path: "icons",
        loadChildren: "./views/icons/icons.module#IconsModule"
      },
      {
        path: "notifications",
        loadChildren: "./views/notifications/notifications.module#NotificationsModule"
      },
      {
        path: "theme",
        loadChildren: "./views/theme/theme.module#ThemeModule"
      },
      {
        path: "widgets",
        loadChildren: "./views/widgets/widgets.module#WidgetsModule"
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    PagesModule
  ],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
