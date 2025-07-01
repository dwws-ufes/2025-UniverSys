import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NgxPermissionsService} from 'ngx-permissions';
import {AuthenticationService} from 'src/app/shared/services/authentication.service';
import {AuthClient, LoginModel} from '../../../../web-api-client';
import {AuthGuard} from '../auth-guard.service';

@Component({
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    standalone: false
})
export class LoginComponent implements OnInit, AfterViewInit {
  @ViewChild('login') loginField: ElementRef;
  loginForm: UntypedFormGroup;
  autenticando: boolean = false;
  returnUrl: string;
  passwordVisible = false;
  urlManual: string;
  // clientes: ClienteDto[] = [];

  constructor(
    private fb: UntypedFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authGuard: AuthGuard,
    private authClient: AuthClient,
    private notification: NzNotificationService,
    private authenticationService: AuthenticationService
  ) {

  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.loginField.nativeElement.focus();
    }, 250);
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      userName: new FormControl<string>(null, Validators.required),
      password: new FormControl<string>(null, Validators.required),
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  submitForm(): void {
      this.autenticando = true;

      let req = new LoginModel({
        login: this.loginForm.controls['userName'].value,
        senha: this.loginForm.controls['password'].value,
      });

      this.authClient.login(req).subscribe({
        next: async response => {
          const token = response.accessToken;
          localStorage.setItem('jwt', token);
          const refreshToken = response.refreshToken;
          localStorage.setItem('refreshToken', refreshToken);
          this.authGuard.setLoggedIn(true);


          this.router.navigate(['/']);
        },
        error: err => {
          this.authGuard.setLoggedIn(false);
          if (err.status === 401 || err.status === 403) {
            this.notification.error('Erro', 'Usuário não encontrado ou senha inválida');
          } else {
            this.notification.error('Erro', 'Ocorreu um erro ao acessar o sistema. Entre em contato com o suporte');
          }
          this.autenticando = false;
        },
        complete: () => (this.autenticando = false),
      });
  }
}
