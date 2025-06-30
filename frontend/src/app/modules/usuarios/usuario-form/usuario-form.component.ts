import {Component, inject, OnInit} from '@angular/core';
import {FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  UsuarioAlterarCommand,
  UsuarioCriarCommand,
  UsuariosClient,
  UsuarioDto,
} from 'web-api-client';

@Component({
    templateUrl: './usuario-form.component.html',
    styleUrls: ['./usuario-form.component.scss'],
    standalone: false
})
export class UsuarioFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  form: UntypedFormGroup;
  salvandoUsuario: boolean;

  usuarioId;
  usuario: UsuarioDto;
  carregandousuario: boolean;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private usuariosClient: UsuariosClient
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.usuarioId = this.route.snapshot.paramMap.get('id');
    if (this.usuarioId) {
      this.usuariosClient.obterPorId(this.usuarioId).subscribe(res => {
        this.form.patchValue(res);
        this.usuario = res;
      });
    }
  }

  private initForm() {
    this.form = new UntypedFormBuilder().group({
      id: [null],
      login: new FormControl<string>(null, Validators.required),
      nome: new FormControl<string>(null, Validators.required),
      email: new FormControl<string>(null, [Validators.required, Validators.email]),
    });
  }

  salvar() {
    if (this.form.valid) {
      this.usuarioId ? this.atualizarUsuario() : this.cadastrarUsuario();
    }
  }

  private cadastrarUsuario() {
    const fValue = this.form.value;
    const req = UsuarioCriarCommand.fromJS(fValue);

    req.termoAceito = true;

    this.salvandoUsuario = true;
    this.usuariosClient
      .criar(req)
      .pipe(
        finalize(() => {
          this.salvandoUsuario = false;
        })
      )
      .subscribe(r => {
        this.nzNotificationService.success('Usuário cadastrado com sucesso!', '');
        this.router.navigate(['/usuarios', 'editar', r.id]);
      });
  }

  private atualizarUsuario() {
    const fValue = this.form.value;
    const req = UsuarioAlterarCommand.fromJS(fValue);

    this.salvandoUsuario = true;
    this.usuariosClient
      .alterar(req)
      .pipe(
        finalize(() => {
          this.salvandoUsuario = false;
        })
      )
      .subscribe(r => {
        this.nzNotificationService.success('Usuário atualizado com sucesso!', '');
        this.router.navigate(['/usuarios']);
      });
  }
}
