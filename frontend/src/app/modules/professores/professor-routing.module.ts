import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ProfessorFormComponent} from './professor-form/professor-form.component';
import {ProfessorListarComponent} from './professor-listar/professor-listar.component';

const routes: Routes = [
  {
    path: '',
    component: ProfessorListarComponent,
    data: {
      title: 'Professores',
    },
  },
  {
    path: 'criar',
    component: ProfessorFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: ProfessorFormComponent,
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
export class ProfessorRoutingModule {}
