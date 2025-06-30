import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CursoFormComponent} from './curso-form/curso-form.component';
import {CursoListarComponent} from './curso-listar/curso-listar.component';

const routes: Routes = [
  {
    path: '',
    component: CursoListarComponent,
    data: {
      title: 'Cursos',
    },
  },
  {
    path: 'criar',
    component: CursoFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: CursoFormComponent,
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
export class CursoRoutingModule {}
