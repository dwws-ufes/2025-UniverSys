import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { BoletimDashboardComponent } from './boletim-dashboard/boletim-dashboard.component';
import { BoletimRoutingModule } from './boletim-routing.module';

// ng-zorro modules
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzProgressModule } from 'ng-zorro-antd/progress';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzListModule } from 'ng-zorro-antd/list';

@NgModule({
  declarations: [
    BoletimDashboardComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BoletimRoutingModule,
    NzCardModule,
    NzPageHeaderModule,
    NzSelectModule,
    NzSpinModule,
    NzStatisticModule,
    NzGridModule,
    NzEmptyModule,
    NzTagModule,
    NzIconModule,
    NzDescriptionsModule,
    NzDividerModule,
    NzProgressModule,
    NzCollapseModule,
    NzListModule
  ]
})
export class BoletimModule { }
