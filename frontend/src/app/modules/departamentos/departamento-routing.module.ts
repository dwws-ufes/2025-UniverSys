import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DepartamentoFormComponent} from './departamento-form/departamento-form.component';
import {DepartamentoListarComponent} from './departamento-listar/departamento-listar.component';

const routes: Routes = [
  {
    path: '',
    component: DepartamentoListarComponent,
    data: {
      title: 'Departamentos',
    },
  },
  {
    path: 'criar',
    component: DepartamentoFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: DepartamentoFormComponent,
    canActivate: [],
    data: {
      title: 'Editar'
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DepartamentoRoutingModule {}
