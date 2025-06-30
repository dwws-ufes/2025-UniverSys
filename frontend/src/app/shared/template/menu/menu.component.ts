import {Component, computed, effect, inject, input} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {NgxPermissionsService} from 'ngx-permissions';
import {Observable} from 'rxjs';
import {AuthGuard} from 'src/app/authentication/auth-guard.service';
import {environment} from 'src/environments/environment';
import {SideNavInterface} from '../../interfaces/side-nav.type';
import {AuthenticationService} from '../../services/authentication.service';
import {ROUTES} from './../side-nav/side-nav-routes.config';


@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    standalone: false
})
export class MenuComponent {
  route = inject(ActivatedRoute);
  // commonLayout = input<boolean>();
  authGuard = inject(AuthGuard);
  jwtHelper = inject(JwtHelperService);
  permissionsService = inject(NgxPermissionsService);
  router = inject(Router);
  authenticationService = inject(AuthenticationService);
  menuItems: any[];
  nomeUsuario = '';
  login = '';
  usuarioId: 0;
  isLoggedIn$: Observable<boolean>;
  alterandoStatus: boolean;
  avatarUrl: string;
  manualUrl: string;
  mensagemCabecalho = '';

  clienteId: number | undefined;
  // UI REACT
  mobileMenuShow = false;

  constructor() {
    effect(() => {
      this.setarItensMenu();
    });
  }

  ngOnInit() {
    try {
      const token = localStorage.getItem('jwt');
      const tokenDecodificado = this.jwtHelper.decodeToken(token);
      if (tokenDecodificado) {
        this.nomeUsuario = tokenDecodificado?.nome ?? tokenDecodificado?.login;
        this.login = tokenDecodificado.login;
        this.usuarioId = tokenDecodificado.usuarioId;
        this.clienteId = tokenDecodificado.clienteId;
      }

      this.isLoggedIn$ = this.authGuard.isLoggedIn;

      if (environment.mensagemCabecalho) this.mensagemCabecalho = environment.mensagemCabecalho;
    } catch (error) {
      console.log('Erro ao decodificar o token: ' + error);
      this.logOut();
    }

  }

  private setarItensMenu() {
    this.menuItems = ROUTES.map(menuItem => this.mapMenuItens(menuItem)).filter(Boolean);
  }

  mapMenuItens(menuItem) {
    if (this.permitirExibicaoItemMenu(menuItem)) {
      if (menuItem?.submenu?.length > 0) {
        menuItem.submenu = menuItem?.submenu
          .map(submenu => {
            if (this.permitirExibicaoItemMenu(submenu)) {
              if (submenu?.submenu?.length > 0) {
                submenu.submenu = submenu?.submenu.filter(submenu => this.permitirExibicaoItemMenu(submenu));
              }
              return submenu;
            }
          })
          .filter(Boolean);
      }
      return menuItem;
    }
  }

  adicionarUsuario(id: number) {
    this.router.navigate(['usuarios', 'cadastrar']);
  }


  irParaMinhaConta() {
    this.router.navigate(['minha-conta']);
  }

  openMobileMenu() {
    this.mobileMenuShow = !this.mobileMenuShow;
  }

  logOut() {
    this.authenticationService.logout();
  }

  private permitirExibicaoItemMenu(item: SideNavInterface): boolean {
    if (!item.permissions?.length) {
      return true;
    }

    //TODO
    return true
  }


}
