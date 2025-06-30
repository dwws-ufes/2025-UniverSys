import {NgModule} from '@angular/core';
import {PreloadAllModules, RouterModule, Routes} from '@angular/router';
import {FullLayoutComponent} from './layouts/full-layout/full-layout.component';
import {ProductLayoutComponent} from './layouts/product-layout/product-layout.component';

import {FullLayout_ROUTES} from './shared/routes/full-layout.routes';
import {ProductLayout_ROUTES} from './shared/routes/product-layout.routes';

const appRoutes: Routes = [
  // LAYOUT SEM BARRA  LATERAL
  {
    path: '',
    component: FullLayoutComponent,
    children: FullLayout_ROUTES,
  },

  // LAYOUT PADRAO
  {
    path: '',
    component: ProductLayoutComponent,
    children: ProductLayout_ROUTES,
  }
];
@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {
      preloadingStrategy: PreloadAllModules,
      anchorScrolling: 'enabled',
      scrollPositionRestoration: 'enabled',
      useHash: false,
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
