import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MatriculasListarComponent} from './matriculas-listar/matriculas-listar.component';

const routes: Routes = [
  {
    path: '',
    component: MatriculasListarComponent,
    data: {
      title: 'Matrículas',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MatriculasRoutingModule {}
