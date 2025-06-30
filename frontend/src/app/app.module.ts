import { LOCALE_ID, NgModule, inject, provideAppInitializer } from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NzBreadCrumbModule} from 'ng-zorro-antd/breadcrumb';
import {NZ_DATE_LOCALE, NZ_I18N, pt_BR} from 'ng-zorro-antd/i18n';

import {HashLocationStrategy, LocationStrategy, registerLocaleData} from '@angular/common';
import pt from '@angular/common/locales/pt';
import {ptBR} from 'date-fns/locale';

import {AppRoutingModule} from './app-routing.module';
import {SharedModule} from './shared/shared.module';
import {TemplateModule} from './shared/template/template.module';

import {AppComponent} from './app.component';
import {FullLayoutComponent} from './layouts/full-layout/full-layout.component';
import {ProductLayoutComponent} from './layouts/product-layout/product-layout.component';

import {NzAvatarModule} from 'ng-zorro-antd/avatar';
import {NzBadgeModule} from 'ng-zorro-antd/badge';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzCardModule} from 'ng-zorro-antd/card';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzTableModule} from 'ng-zorro-antd/table';
import {ThemeConstantService} from './shared/services/theme-constant.service';

import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {JwtModule} from '@auth0/angular-jwt';
import {NzNotificationModule} from 'ng-zorro-antd/notification';
import {NzPopoverModule} from 'ng-zorro-antd/popover';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {NzSwitchModule} from 'ng-zorro-antd/switch';
import {NgxPermissionsModule, NgxPermissionsService} from 'ngx-permissions';
import {firstValueFrom} from 'rxjs';
import {API_BASE_URL} from 'web-api-client';
import {environment} from '../environments/environment';
import {AuthGuard} from './authentication/auth-guard.service';
import {HttpErrorInterceptor} from './shared/interceptor/http-error.interceptor';
import {AuthenticationService} from './shared/services/authentication.service';

import {FullCalendarModule} from '@fullcalendar/angular';
import {BASE_URL} from '@magmadigital/components';
import {CURRENCY_MASK_CONFIG} from 'ng2-currency-mask';


registerLocaleData(pt);

// tslint:disable-next-line:typedef
export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [AppComponent, FullLayoutComponent, ProductLayoutComponent],
  imports: [
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5001'],
      },
    }),
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NzBreadCrumbModule,
    TemplateModule,
    SharedModule,
    NzCardModule,
    NzInputModule,
    NzTableModule,
    NzSelectModule,
    NzBadgeModule,
    NzAvatarModule,
    NzButtonModule,
    NzIconModule,
    NzNotificationModule,
    NzSpinModule,
    NzModalModule,
    NzSwitchModule,
    NzPopoverModule,
    FullCalendarModule,
    NgxPermissionsModule.forRoot(),
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiBaseUrl,
    },
    {
      provide: BASE_URL,
      useValue: environment.apiBaseUrl,
    },
    {
      provide: NZ_I18N,
      useValue: pt_BR,
    },
    {provide: NZ_DATE_LOCALE, useValue: ptBR},
    {provide: LOCALE_ID, useValue: 'pt-BR'},
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
    provideAppInitializer(() => {
      const initializerFn = ((
      authenticationService: AuthenticationService
    ) => {
      return async (): Promise<void> => {

        if(!authenticationService.obterUsuarioLogado) {
          authenticationService.logout();
        }
      };
    })(inject(AuthenticationService))
      return initializerFn();
    }),
    {
      provide: CURRENCY_MASK_CONFIG,
      useValue: {
        align: 'left',
        allowNegative: true,
        decimal: ',',
        precision: 2,
        prefix: 'R$ ',
        suffix: '',
        thousands: '.',
      },
    },
    ThemeConstantService,
    AuthGuard
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
