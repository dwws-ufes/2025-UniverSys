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
  TipoUsuario,
  AlunosClient,
  AlunoObterQuery,
  AlunoObterDto,
  ProfessoresClient,
  ProfessorObterQuery,
  ProfessorObterDto,
  SelectItemEnum
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

  TipoUsuario = TipoUsuario;
  tiposUsuario: SelectItemEnum[] = [];
  alunos: AlunoObterDto[] = [];
  professores: ProfessorObterDto[] = [];
  carregandoAlunos: boolean = false;
  carregandoProfessores: boolean = false;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private usuariosClient: UsuariosClient,
    private alunosClient: AlunosClient,
    private professoresClient: ProfessoresClient
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.carregarTiposUsuario();
    this.carregarAlunos();
    this.carregarProfessores();

    this.usuarioId = this.route.snapshot.paramMap.get('id');

    this.configurarValidacaoSenha();

    if (this.usuarioId) {
      this.usuariosClient.obterPorId(this.usuarioId).subscribe(res => {
        const dadosUsuario = { ...res };
        delete dadosUsuario.senha;

        this.form.patchValue(dadosUsuario);
        this.usuario = res;
      });
    }
  }

  private initForm() {
    this.form = new UntypedFormBuilder().group({
      id: [null],
      login: new FormControl<string>(null, Validators.required),
      senha: new FormControl<string>(null),
      nome: new FormControl<string>(null, Validators.required),
      email: new FormControl<string>(null, [Validators.required, Validators.email]),
      tipo: new FormControl<TipoUsuario>(null, Validators.required),
      alunoId: new FormControl<number>(null),
      professorId: new FormControl<number>(null)
    });

    this.form.get('tipo')?.valueChanges.subscribe(tipo => {
      const alunoIdControl = this.form.get('alunoId');
      const professorIdControl = this.form.get('professorId');

      if (tipo === TipoUsuario.Aluno) {
        professorIdControl?.setValue(null);
      } else if (tipo === TipoUsuario.Professor) {
        alunoIdControl?.setValue(null);
      } else {
        alunoIdControl?.setValue(null);
        professorIdControl?.setValue(null);
      }
    });

    this.configurarValidacaoSenha();
  }

  private configurarValidacaoSenha() {
    const senhaControl = this.form.get('senha');

    if (!this.usuarioId) {
      senhaControl?.setValidators([Validators.required]);
    } else {
      senhaControl?.clearValidators();
    }

    senhaControl?.updateValueAndValidity();
  }

  private carregarTiposUsuario() {
    this.usuariosClient.obterTipos().subscribe(tipos => {
      this.tiposUsuario = tipos;
    });
  }

  private carregarAlunos() {
    this.carregandoAlunos = true;
    const query = new AlunoObterQuery();
    query.pageSize = -1

    this.alunosClient.obter(query)
      .pipe(finalize(() => this.carregandoAlunos = false))
      .subscribe(response => {
        this.alunos = response.items || [];
      });
  }

  private carregarProfessores() {
    this.carregandoProfessores = true;
    const query = new ProfessorObterQuery();
    query.pageSize = -1;

    this.professoresClient.obter(query)
      .pipe(finalize(() => this.carregandoProfessores = false))
      .subscribe(response => {
        this.professores = response.items || [];
      });
  }

  get isTipoAluno(): boolean {
    return this.form.get('tipo')?.value === TipoUsuario.Aluno;
  }

  get isTipoProfessor(): boolean {
    return this.form.get('tipo')?.value === TipoUsuario.Professor;
  }

  salvar() {
    if (this.form.valid) {
      this.usuarioId ? this.atualizarUsuario() : this.cadastrarUsuario();
    }
  }

  private cadastrarUsuario() {
    const fValue = this.form.value;
    const req = UsuarioCriarCommand.fromJS(fValue);

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

    if (!fValue.senha || fValue.senha.trim() === '') {
      delete fValue.senha;
    }

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
