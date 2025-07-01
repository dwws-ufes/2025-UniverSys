import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoletimDashboardComponent } from './boletim-dashboard/boletim-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: BoletimDashboardComponent,
    data: {
      title: 'Boletim',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BoletimRoutingModule {}
