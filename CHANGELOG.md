## v1.1.0

* Now you can run through a docker! ❤️
    * Unfortunately you need to change hosts for it. Because Authority URL. I can't do anything to face it. It's security feature from OAuth2 to keep same Authority name for each Token.

* Pagination for Users and Persisted Grants

* Update of main DbContext, JpProject
* Removed previous components from MySql.IdentityServer and Sql.IdentityServer. Unifying them at same project.

* Client Page:
    * Changed Claims button to be under Token - Following Docs from [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/reference/client.html#token)
    * Included Device Flow options

Updates:
* @angular/cli to 7.3.6
* @angular/core to 7.2.10 (ng update @angular/core, then updated other dep's by itself)|
    * @angular/language-service @ "7.2.10" (was "6.0.0")...
    * @angular/compiler-cli @ "7.2.10" (was "6.0.0")...
    * @angular/animations @ "7.2.10" (was "6.0.0")...
    * @angular/compiler @ "7.2.10" (was "6.0.0")...
    * @angular/common @ "7.2.10" (was "6.0.0")...
    * @angular/core @ "7.2.10" (was "6.0.0")...
    * @angular/http @ "7.2.10" (was "6.0.0")...
    * @angular/forms @ "7.2.10" (was "6.0.0")...
    * @angular/platform-browser-dynamic @ "7.2.10" (was "6.0.0")...
    * @angular/platform-browser @ "7.2.10" (was "6.0.0")...
    * @angular/router @ "7.2.10" (was "6.0.0")...
    * zone.js @ "0.8.29" (was "0.8.26")...
    * rxjs @ "6.4.0" (was "6.0.0")...
    * typescript @ "3.2.4" (was "2.7.2")

Updated components from UI:

|dependency|old version| new version|
|----------|-----------|------------|
|@agm/core| 1.0.0-beta.2|  →   1.0.0-beta.5|
|@ng-bootstrap/ng-bootstrap |^3.2.2| → ^4.1.0|
|@ngx-translate/core| 10.0.1| → 11.0.1|
|@ngx-translate/http-loader| 3.0.1| → 4.0.0|
|@swimlane/ngx-datatable| 12.0.0| → 14.0.0|
|ag-grid| 17.1.1| → 18.1.2|
|ag-grid-angular| 17.1.0| → 20.2.0|
|angular-oauth2-oidc |^4.0.2| → ^5.0.2|
|angular-tree-component| 7.0.1| → 8.3.0|
|bootstrap| 4.1.1| → 4.3.1|
|chart.js| 2.7.2| → 2.8.0|
|codemirror| 5.37.0| → 5.45.0|
|enhanced-resolve| 3.3.0| → 4.1.0|
|fullcalendar| 3.9.0| → 3.10.0|
|lodash| 4.17.10| → 4.17.11|
|modernizr| 3.6.0| → 3.7.1|
|moment| 2.22.1| → 2.24.0|
|ng2-charts| 1.6.0| → 2.0.4|
|ng2-material-dropdown| 0.9.5| → 0.10.1|
|ngx-chips |^1.9.8| → ^2.0.0-beta.0|
|ngx-color-picker| 6.0.0| → 7.4.0|
|ngx-infinite-scroll| 0.8.4| → 7.1.0|
|popper.js| 1.14.3| → 1.14.7|
|screenfull| 3.3.2| → 4.1.0|
|summernote| 0.8.10| → 0.8.11|
|sweetalert| 1.1.3| → 2.1.2|
|ts-helpers| 1.1.1| **removed**|
|web-animations-js| 2.2.1| → 2.3.1|
|zone.js| 0.8.29| → 0.9.0|
|@angular-devkit/build-angular| 0.6.1| → 0.13.6|
|@types/lodash| 4.14.108| → 4.14.123|
|codelyzer| 4.2.1| → 5.0.0-beta.2|
|ts-node| 5.0.1| → 8.0.3|
|tslint| 5.9.1| → 5.14.0|

## v1.0.0

- First release
