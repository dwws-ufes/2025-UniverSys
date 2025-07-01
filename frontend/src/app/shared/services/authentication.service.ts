import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../interfaces/user.type';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxPermissionsService } from 'ngx-permissions';
import { Router } from '@angular/router';
import { TipoUsuario } from 'web-api-client';

const USER_AUTH_API_URL = '/api-url';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  router = inject(Router)
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private permissionsService: NgxPermissionsService
  ) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  public get obterUsuarioLogado(): any {
    const token: string = localStorage.getItem('jwt');

    try {
      return this.jwtHelper.decodeToken(token);
    } catch (error) {}
  }

  public get obterTipoUsuario(): TipoUsuario {
    var tipoUsuario = this.obterUsuarioLogado?.tipo;
    var enumValue: TipoUsuario = (<any>TipoUsuario)[tipoUsuario];
    return enumValue;
  }

  login(username: string, password: string) {
    return this.http.post<any>(USER_AUTH_API_URL, { username, password }).pipe(
      map((user) => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    localStorage.removeItem('jwt');
    localStorage.removeItem('refreshToken');
    // this.authGuard.setLoggedIn(false);
    this.router.navigate(['authentication/login']);
  }

  //TODO
  isAdmin(): boolean {
    return this.obterTipoUsuario === TipoUsuario.Administrador;
  }

  isProfessor(): boolean {
    return this.obterTipoUsuario === TipoUsuario.Professor;
  }

  isAluno(): boolean {
    return this.obterTipoUsuario === TipoUsuario.Aluno;
  }

  getAlunoId(): number | null {
    const usuario = this.obterUsuarioLogado;
    return usuario?.alunoId || null;
  }

  getProfessorId(): number | null {
    const usuario = this.obterUsuarioLogado;
    return usuario?.professorId || null;
  }

}
