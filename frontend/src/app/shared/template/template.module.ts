import {CommonModule, DecimalPipe} from '@angular/common';
import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';

import {NzAvatarModule} from 'ng-zorro-antd/avatar';
import {NzBadgeModule} from 'ng-zorro-antd/badge';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzDividerModule} from 'ng-zorro-antd/divider';
import {NzDrawerModule} from 'ng-zorro-antd/drawer';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzListModule} from 'ng-zorro-antd/list';
import {NzRadioModule} from 'ng-zorro-antd/radio';
import {NzSwitchModule} from 'ng-zorro-antd/switch';

import {FooterComponent} from './footer/footer.component';
import {MenuComponent} from './menu/menu.component';

import {
  ArrowRightLeft,
  FilePen,
  FileSpreadsheet,
  Handshake,
  Landmark,
  LucideAngularModule,
  PackageSearch,
  Percent,
  ReceiptText,
  ShoppingCart,
  Signature,
  SquareMenu,
  Store,
  UserRoundCheck,
} from 'lucide-angular';
import {NzBreadCrumbModule} from 'ng-zorro-antd/breadcrumb';
import {NzCardModule} from 'ng-zorro-antd/card';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzPopconfirmModule} from 'ng-zorro-antd/popconfirm';
import {NzTagModule} from 'ng-zorro-antd/tag';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NgxPermissionsDirective, NgxPermissionsModule} from 'ngx-permissions';
import {SideNavDirective} from '../directives/side-nav.directive';
import {ThemeConstantService} from '../services/theme-constant.service';

const antdModule = [
  NzAvatarModule,
  NzBadgeModule,
  NzBreadCrumbModule,
  NzRadioModule,
  NzDropDownModule,
  NzListModule,
  NzDrawerModule,
  NzDividerModule,
  NzSwitchModule,
  NzInputModule,
  NzButtonModule,
  NzTagModule,
  NzCardModule,
  NzGridModule,
  NzToolTipModule,
  NzPopconfirmModule,
];

@NgModule({
  exports: [
    CommonModule,
    SideNavDirective,
    FooterComponent,
    MenuComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    ...antdModule,
    LucideAngularModule.pick({
      ArrowRightLeft,
      PackageSearch,
      ShoppingCart,
      UserRoundCheck,
      FilePen,
      SquareMenu,
      Signature,
      Handshake,
      Percent,
      Landmark,
      Store,
      ReceiptText,
      FileSpreadsheet
    }),
    NgxPermissionsModule,
  ],
  declarations: [
    SideNavDirective,
    FooterComponent,
    MenuComponent,
  ],
  providers: [DecimalPipe, ThemeConstantService, NgxPermissionsDirective],
})
export class TemplateModule {}
