import {Injectable, inject} from '@angular/core';
import {Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import { AuthenticationService } from '../shared/services/authentication.service';

@Injectable()
export class DefaultGuard {
  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(): boolean {
    const token = localStorage.getItem('jwt');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.redirecionarPaginaInicialBaseadoPerfil();
    }
    return true;
  }

  private redirecionarPaginaInicialBaseadoPerfil() {
    if (this.authenticationService.isAdmin()) {
      this.router.navigateByUrl('/usuarios');
      return;
    }
    if (this.authenticationService.isAluno()) {
      this.router.navigateByUrl('/matriculas');
      return;
    }
    this.router.navigateByUrl('/turmas');
  }
}
