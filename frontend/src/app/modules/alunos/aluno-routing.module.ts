import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AlunoFormComponent} from './aluno-form/aluno-form.component';
import {AlunoListarComponent} from './aluno-listar/aluno-listar.component';
import {AdminGuard} from '../../authentication/admin-guard.service';

const routes: Routes = [
  {
    path: '',
    component: AlunoListarComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'criar',
    component: AlunoFormComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'editar/:id',
    component: AlunoFormComponent,
    canActivate: [AdminGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlunoRoutingModule {
}
