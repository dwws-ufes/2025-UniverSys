import {NgModule, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {CursoFormComponent} from './curso-form/curso-form.component';
import {CursoListarComponent} from './curso-listar/curso-listar.component';
import {CursoRoutingModule} from './curso-routing.module';

/** Import any ng-zorro components as the module required except icon module */
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
import { DisciplinaListarComponent } from '../disciplinas/disciplina-listar/disciplina-listar.component';
import { AlunoListarComponent } from '../alunos/aluno-listar/aluno-listar.component';
import { DisciplinaModule } from '../disciplinas/disciplina.module';
import { AlunoModule } from '../alunos/aluno.module';

/** Assign all ng-zorro modules to this array*/
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
    CursoRoutingModule,
    MagmaSharedTitleComponent,
    MagmaSearchActionsComponent,
    MagmaGridComponent,
    ...antdModule,
    NgxPermissionsModule,
    DisciplinaModule,
    AlunoModule
  ],
  exports: [],
  declarations: [CursoFormComponent, CursoListarComponent],
  providers: [provideNgxMask(), NgxPermissionsDirective],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CursoModule {}
