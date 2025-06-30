import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UsuarioFormComponent} from './usuario-form/usuario-form.component';
import {UsuarioListarComponent} from './usuario-listar/usuario-listar.component';

const routes: Routes = [
  {
    path: '',
    component: UsuarioListarComponent,
    data: {
      title: 'Usuários',
    },
  },
  {
    path: 'criar',
    component: UsuarioFormComponent,
    canActivate: [],
    data: {
      title: 'Criar',
    },
  },
  {
    path: 'editar/:id',
    component: UsuarioFormComponent,
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
export class UsuarioRoutingModule {}
