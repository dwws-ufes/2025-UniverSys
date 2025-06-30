import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Router, RouterStateSnapshot} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {BehaviorSubject} from 'rxjs';
import {AuthClient} from 'web-api-client';
import { AuthenticationService } from '../shared/services/authentication.service';

@Injectable()
export class AuthGuard {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private authClient: AuthClient,
    private authService: AuthenticationService
  ) {}

  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('jwt');
  }

  setLoggedIn(authenticated: boolean) {
    this.loggedIn.next(authenticated);
  }

  getLogin() {
    const token: string = localStorage.getItem('jwt');
    let tokenDecodificado = this.jwtHelper.decodeToken(token);

    if (tokenDecodificado) {
      return tokenDecodificado.login;
    }

    return null;
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    try {
      const token = localStorage.getItem('jwt');
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        return true;
      }

      this.setLoggedIn(false);
      this.router.navigate(['authentication/login'], {
        queryParams: {returnUrl: state.url},
      });
      // }
      return false;
      // return true;
    } catch (error) {
      return false;
    }
  }


  getUserId() {
    const token: string = localStorage.getItem('jwt');
    let tokenDecodificado = this.jwtHelper.decodeToken(token);

    if (tokenDecodificado) {
      const userId = tokenDecodificado.usuarioId;
      return userId;
    }

    return null;
  }

}
