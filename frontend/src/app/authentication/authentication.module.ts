import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../shared/shared.module';
import {ReactiveFormsModule} from '@angular/forms';
import {AuthenticationRoutingModule} from './authentication-routing.module';

import {NzFormModule} from 'ng-zorro-antd/form';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzCardModule} from 'ng-zorro-antd/card';
import {NzCheckboxModule} from 'ng-zorro-antd/checkbox';
import {NzStepsModule} from 'ng-zorro-antd/steps';
import {NzSelectModule} from 'ng-zorro-antd/select';

import {LoginComponent} from './login/login.component';
import {NgxMaskDirective, provideNgxMask} from 'ngx-mask';
import {LoggedOutContainerComponent} from './components/logged-out-container/logged-out-container.component';

const antdModule = [
  NzFormModule,
  NzInputModule,
  NzButtonModule,
  NzCardModule,
  NzCheckboxModule,
  NzStepsModule,
  NzSelectModule,
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AuthenticationRoutingModule,
    NgxMaskDirective,
    ...antdModule,
  ],
  declarations: [
    LoginComponent,
    LoggedOutContainerComponent,
  ],
  providers: [provideNgxMask()],
  exports: [LoggedOutContainerComponent],
})
export class AuthenticationModule {}
