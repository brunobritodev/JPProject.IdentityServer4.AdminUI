import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { SettingsService } from '@core/settings/settings.service';
import { Observable } from 'rxjs';
import { filter, map, tap } from 'rxjs/operators';

import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardOnlyAdmin implements CanActivate {
  private isAuthenticated: boolean;

  constructor(
    private authService: AuthService,
    private settings: SettingsService,
    private router: Router
  ) {
    this.authService.isAuthenticated$.subscribe(i => this.isAuthenticated = i);
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean> {
    return this.authService.isDoneLoading$
      .pipe(filter(isDone => isDone))
      .pipe(tap(_ => this.isAuthenticated || this.authService.login(state.url)))
      .pipe(map(_ => {
        if (this.isAuthenticated && this.settings.getUserClaims()["role"] === "Administrator") {
          return true;
        }
        this.router.navigate(['/not-found']);
        return false;
      }));
  }
}
