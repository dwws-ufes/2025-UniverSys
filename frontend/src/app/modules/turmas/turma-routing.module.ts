import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TurmaFormComponent} from './turma-form/turma-form.component';
import {TurmaListarComponent} from './turma-listar/turma-listar.component';

const routes: Routes = [
  {
    path: '',
    component: TurmaListarComponent,
    data: {
      title: 'Turmas',
    },
  },
  {
    path: 'criar',
    component: TurmaFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: TurmaFormComponent,
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
export class TurmaRoutingModule {}
