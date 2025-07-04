import { TipoUsuario } from 'web-api-client';
import { SideNavInterface } from '../../interfaces/side-nav.type';
export const ROUTES: SideNavInterface[] = [
  {
    path: 'usuarios',
    title: 'Usuários',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'user',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'cursos',
    title: 'Cursos',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'book',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'departamentos',
    title: 'Departamentos',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'apartment',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'disciplinas',
    title: 'Disciplinas',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'read',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'turmas',
    title: 'Turmas',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'team',
    submenu: [],
    permissions: [TipoUsuario.Administrador, TipoUsuario.Professor],
  },
  {
    path: 'professores',
    title: 'Professores',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'user-delete',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'alunos',
    title: 'Alunos',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'usergroup-add',
    submenu: [],
    permissions: [TipoUsuario.Administrador],
  },
  {
    path: 'boletim',
    title: 'Boletim',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'bar-chart',
    submenu: [],
    permissions: [TipoUsuario.Aluno],
  },
  {
    path: 'matriculas',
    title: 'Matrículas',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'form',
    submenu: [],
    permissions: [TipoUsuario.Aluno],
  }
];
