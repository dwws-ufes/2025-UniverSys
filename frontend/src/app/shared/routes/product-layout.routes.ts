import {Routes} from '@angular/router';
import { AdminGuard } from '../../authentication/admin-guard.service';
import { AlunoGuard } from '../../authentication/aluno-guard.service';
import { AdminOuAlunoGuard } from '../../authentication/admin-ou-aluno-guard.service';
import { AdminOuProfessorGuard } from 'src/app/authentication/admin-ou-professor-guard.service';

export const ProductLayout_ROUTES: Routes = [
  {
    path: 'usuarios',
    loadChildren: () => import('../../modules/usuarios/usuario.module').then(m => m.UsuarioModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Usuários'
    },
  },
  {
    path: 'cursos',
    loadChildren: () => import('../../modules/cursos/curso.module').then(m => m.CursoModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Cursos'
    },
  },
  {
    path: 'departamentos',
    loadChildren: () => import('../../modules/departamentos/departamento.module').then(m => m.DepartamentoModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Departamentos'
    },
  },
  {
    path: 'disciplinas',
    loadChildren: () => import('../../modules/disciplinas/disciplina.module').then(m => m.DisciplinaModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Disciplinas'
    },
  },
  {
    path: 'turmas',
    loadChildren: () => import('../../modules/turmas/turma.module').then(m => m.TurmaModule),
    canActivate: [AdminOuProfessorGuard],
    data: {
      title: 'Turmas'
    },
  },
  {
    path: 'professores',
    loadChildren: () => import('../../modules/professores/professor.module').then(m => m.ProfessorModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Professores'
    },
  },
  {
    path: 'alunos',
    loadChildren: () => import('../../modules/alunos/aluno.module').then(m => m.AlunoModule),
    canActivate: [AdminGuard],
    data: {
      title: 'Alunos'
    },
  },
  {
    path: 'boletim',
    loadChildren: () => import('../../modules/boletim/boletim.module').then(m => m.BoletimModule),
    canActivate: [AlunoGuard],
    data: {
      title: 'Boletim'
    },
  },
  {
    path: 'matriculas',
    loadChildren: () => import('../../modules/matriculas/matriculas.module').then(m => m.MatriculasModule),
    canActivate: [AlunoGuard],
    data: {
      title: 'Matrículas'
    },
  },
];
