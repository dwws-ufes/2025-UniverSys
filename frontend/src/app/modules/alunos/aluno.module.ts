import {NgModule, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {AlunoFormComponent} from './aluno-form/aluno-form.component';
import {AlunoListarComponent} from './aluno-listar/aluno-listar.component';
import {AlunoRoutingModule} from './aluno-routing.module';

import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MagmaGridComponent, MagmaSearchActionsComponent, MagmaSharedTitleComponent} from '@magmadigital/components';
import {NzAffixModule} from 'ng-zorro-antd/affix';
import {NzAvatarModule} from 'ng-zorro-antd/avatar';
import {NzBadgeModule} from 'ng-zorro-antd/badge';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzCardModule} from 'ng-zorro-antd/card';
import {NzCollapseModule} from 'ng-zorro-antd/collapse';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {NzDividerModule} from 'ng-zorro-antd/divider';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzInputNumberModule} from 'ng-zorro-antd/input-number';
import {NzListModule} from 'ng-zorro-antd/list';
import {NzEmptyModule} from 'ng-zorro-antd/empty';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {NzPageHeaderModule} from 'ng-zorro-antd/page-header';
import {NzPopconfirmModule} from 'ng-zorro-antd/popconfirm';
import {NzProgressModule} from 'ng-zorro-antd/progress';
import {NzRadioModule} from 'ng-zorro-antd/radio';
import {NzResultModule} from 'ng-zorro-antd/result';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzSkeletonModule} from 'ng-zorro-antd/skeleton';
import {NzSpaceModule} from 'ng-zorro-antd/space';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {NzStepsModule} from 'ng-zorro-antd/steps';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzTabsModule} from 'ng-zorro-antd/tabs';
import {NzTagModule} from 'ng-zorro-antd/tag';
import {NzTimePickerModule} from 'ng-zorro-antd/time-picker';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NzUploadModule} from 'ng-zorro-antd/upload';
import {provideNgxMask} from 'ngx-mask';
import {NgxPermissionsDirective, NgxPermissionsModule} from 'ngx-permissions';
import { MatriculasModule } from '../matriculas/matriculas.module';

const antdModule = [
  NzButtonModule,
  NzCardModule,
  NzListModule,
  NzTagModule,
  NzBadgeModule,
  NzProgressModule,
  NzDropDownModule,
  NzDatePickerModule,
  NzSelectModule,
  NzSpinModule,
  NzToolTipModule,
  NzAvatarModule,
  NzFormModule,
  NzTimePickerModule,
  NzStepsModule,
  NzCollapseModule,
  NzInputModule,
  NzInputNumberModule,
  NzRadioModule,
  NzResultModule,
  NzIconModule,
  NzDividerModule,
  NzSpaceModule,
  NzPopconfirmModule,
  NzAffixModule,
  NzTableModule,
  NzSkeletonModule,
  NzTabsModule,
  NzUploadModule,
  NzModalModule,
  NzPageHeaderModule,
  NzEmptyModule,
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AlunoRoutingModule,
    MagmaSharedTitleComponent,
    MagmaSearchActionsComponent,
    MagmaGridComponent,
    ...antdModule,
    NgxPermissionsModule,
    MatriculasModule
  ],
  exports: [AlunoListarComponent],
  declarations: [AlunoFormComponent, AlunoListarComponent],
  providers: [provideNgxMask(), NgxPermissionsDirective],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AlunoModule {}
