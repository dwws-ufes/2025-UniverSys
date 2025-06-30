import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {NzProgressModule} from 'ng-zorro-antd/progress';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzDividerModule} from 'ng-zorro-antd/divider';
import {NzCollapseModule} from 'ng-zorro-antd/collapse';
import {NzEmptyModule} from 'ng-zorro-antd/empty';
import {NzPopconfirmModule} from 'ng-zorro-antd/popconfirm';
import {NzTabsModule} from 'ng-zorro-antd/tabs';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {NzUploadModule} from 'ng-zorro-antd/upload';
import {NzAlertModule} from 'ng-zorro-antd/alert';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzSkeletonModule} from 'ng-zorro-antd/skeleton';
import {ApiErrorsTableModalContentComponent} from './components/api-errors-table-modal-content/api-errors-table-modal-content.component';
import {NzAvatarModule} from 'ng-zorro-antd/avatar';
import {NzTagModule} from 'ng-zorro-antd/tag';
import {NzSwitchModule} from 'ng-zorro-antd/switch';
import {NzSpaceModule} from 'ng-zorro-antd/space';
import {NzPopoverModule} from 'ng-zorro-antd/popover';
import {provideNgxMask} from 'ngx-mask';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';

@NgModule({
  exports: [CommonModule, FormsModule, NzIconModule, HttpClientModule, HttpClientJsonpModule],
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzIconModule,
    NzToolTipModule,
    NzModalModule,
    NzProgressModule,
    NzGridModule,
    NzInputModule,
    NzFormModule,
    NzButtonModule,
    NzSelectModule,
    NzDividerModule,
    NzCollapseModule,
    NzEmptyModule,
    NzPopconfirmModule,
    NzTabsModule,
    NzSpinModule,
    NzUploadModule,
    NzAlertModule,
    NzDatePickerModule,
    NzTableModule,
    NzSkeletonModule,
    NzAvatarModule,
    NzTagModule,
    NzSwitchModule,
    NzSpaceModule,
    NzPopoverModule,
  ],
  declarations: [ApiErrorsTableModalContentComponent],
  providers: [provideNgxMask()],
})
export class SharedModule {}
