import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DisciplinaFormComponent} from './disciplina-form/disciplina-form.component';
import {DisciplinaListarComponent} from './disciplina-listar/disciplina-listar.component';

const routes: Routes = [
  {
    path: '',
    component: DisciplinaListarComponent,
    data: {
      title: 'Disciplinas',
    },
  },
  {
    path: 'criar',
    component: DisciplinaFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: DisciplinaFormComponent,
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
export class DisciplinaRoutingModule {}
