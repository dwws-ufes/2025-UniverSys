import {Routes} from '@angular/router';

export const FullLayout_ROUTES: Routes = [
  {
    path: '',
    loadChildren: () => import('../../default/default.module').then(m => m.DefaultModule),
  },
  {
    path: 'authentication',
    loadChildren: () => import('../../authentication/authentication.module').then(m => m.AuthenticationModule),
  },
];
