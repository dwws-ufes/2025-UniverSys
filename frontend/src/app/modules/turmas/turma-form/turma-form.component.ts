import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {finalize} from 'rxjs/operators';
import {
  TurmaSalvarCommand,
  TurmasClient,
  TurmaObterPorIdDto,
  DisciplinasClient,
  DisciplinaObterQuery,
  DisciplinaObterDto,
  ProfessoresClient,
  ProfessorObterQuery,
  ProfessorObterDto,
  AvaliacoesClient,
  AvaliacaoObterQuery,
  AvaliacaoObterDto,
  PaginatedListOfAvaliacaoObterDto,
  AvaliacaoSalvarCommand,
  AvaliacaoObterPorIdDto,
} from 'web-api-client';
import { AuthenticationService } from '../../../shared/services/authentication.service';

@Component({
    templateUrl: './turma-form.component.html',
    styleUrls: ['./turma-form.component.scss'],
    standalone: false
})
export class TurmaFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
    disciplinaId: new FormControl<number>(null, Validators.required),
    professorId: new FormControl<number>(null, Validators.required),
    semestreAno: new FormControl<number>(null, [Validators.required, Validators.min(2020)]),
    semestrePeriodo: new FormControl<number>(null, [Validators.required, Validators.min(1), Validators.max(2)]),
  });
  salvandoTurma: boolean;

  turmaId;
  turma: TurmaObterPorIdDto;
  carregandoTurma: boolean;

  disciplinas: DisciplinaObterDto[] = [];
  carregandoDisciplinas: boolean;

  professores: ProfessorObterDto[] = [];
  carregandoProfessores: boolean;

  avaliacoes = new PaginatedListOfAvaliacaoObterDto();
  carregandoAvaliacoes: boolean;
  pageIndexAvaliacoes = 1;
  pageSizeAvaliacoes = 10;
  avaliacaoModalVisible = false;
  avaliacaoForm = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
    peso: new FormControl<number>(null, [Validators.required, Validators.min(0.1)]),
    turmaId: new FormControl<number>(null),
  });
  salvandoAvaliacao: boolean;
  avaliacaoEditando: AvaliacaoObterDto | null = null;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private turmasClient: TurmasClient,
    private disciplinasClient: DisciplinasClient,
    private professoresClient: ProfessoresClient,
    private avaliacoesClient: AvaliacoesClient,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.turmaId = this.route.snapshot.paramMap.get('id');

    this.carregarDisciplinas();
    this.carregarProfessores();

    if (this.turmaId) {
      this.carregarTurma();
      this.carregarAvaliacoes();
    }

    if (this.isProfessor()) {
      this.form.disable();
    }
  }

  isProfessor(): boolean {
    return this.authService.isProfessor();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  podeEditar(): boolean {
    return this.isAdmin();
  }

  carregarDisciplinas() {
    this.carregandoDisciplinas = true;
    const query = new DisciplinaObterQuery();
    query.pageSize = -1;

    this.disciplinasClient.obter(query)
      .pipe(finalize(() => this.carregandoDisciplinas = false))
      .subscribe(res => {
        this.disciplinas = res.items || [];
      });
  }

  carregarProfessores() {
    this.carregandoProfessores = true;
    const query = new ProfessorObterQuery();
    query.pageSize = -1;

    this.professoresClient.obter(query)
      .pipe(finalize(() => this.carregandoProfessores = false))
      .subscribe(res => {
        this.professores = res.items || [];
      });
  }

  carregarTurma() {
    this.carregandoTurma = true;
    this.turmasClient.obterPorId(+this.turmaId)
      .pipe(finalize(() => this.carregandoTurma = false))
      .subscribe(res => {
        this.form.patchValue(res);
        this.turma = res;
      });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new TurmaSalvarCommand(fValue);

      this.salvandoTurma = true;
      this.turmasClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoTurma = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Turma salva com sucesso!');
          if (this.turmaId) {
            this.carregarTurma();
          } else {
            this.router.navigate(['turmas', 'editar', r]);
          }
        });
    }
  }

  carregarAvaliacoes(
    pageIndex: number = this.pageIndexAvaliacoes,
    pageSize: number = this.pageSizeAvaliacoes
  ): void {
    if (!this.turmaId) return;

    this.pageIndexAvaliacoes = pageIndex;
    this.pageSizeAvaliacoes = pageSize;

    const query = new AvaliacaoObterQuery();
    query.turmaId = +this.turmaId;
    query.pageIndex = pageIndex;
    query.pageSize = pageSize;

    this.carregandoAvaliacoes = true;
    this.avaliacoesClient.obter(query)
      .pipe(finalize(() => this.carregandoAvaliacoes = false))
      .subscribe(res => {
        this.avaliacoes = res || new PaginatedListOfAvaliacaoObterDto();
      });
  }

  onQueryParamsChangeAvaliacoes(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.carregarAvaliacoes(pageIndex, pageSize);
  }

  abrirModalAvaliacao(avaliacao?: AvaliacaoObterDto): void {
    this.avaliacaoEditando = avaliacao || null;
    this.avaliacaoForm.reset();

    if (avaliacao) {
      this.avaliacaoForm.patchValue({
        id: avaliacao.id,
        nome: avaliacao.nome,
        peso: avaliacao.peso,
        turmaId: avaliacao.turmaId
      });
    } else {
      this.avaliacaoForm.patchValue({
        turmaId: +this.turmaId
      });
    }

    this.avaliacaoModalVisible = true;
  }

  editarAvaliacao(avaliacao: AvaliacaoObterDto): void {
    this.abrirModalAvaliacao(avaliacao);
  }

  salvarAvaliacao(): void {
    if (this.avaliacaoForm.valid) {
      const fValue = this.avaliacaoForm.value;
      const req = new AvaliacaoSalvarCommand(fValue);

      this.salvandoAvaliacao = true;
      this.avaliacoesClient.salvar(req)
        .pipe(finalize(() => this.salvandoAvaliacao = false))
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Avaliação salva com sucesso!');
          this.avaliacaoModalVisible = false;
          this.carregarAvaliacoes();
        });
    }
  }

  cancelarModalAvaliacao(): void {
    this.avaliacaoModalVisible = false;
    this.avaliacaoEditando = null;
    this.avaliacaoForm.reset();
  }

  excluirAvaliacao(avaliacao: AvaliacaoObterDto): void {
    this.nzModalService.confirm({
      nzTitle: 'Confirmar exclusão',
      nzContent: `Deseja realmente excluir a avaliação "${avaliacao.nome}"?`,
      nzOkText: 'Sim',
      nzCancelText: 'Não',
      nzOkDanger: true,
      nzOnOk: () => {
        this.avaliacoesClient.excluir(avaliacao.id!)
          .subscribe(() => {
            this.nzNotificationService.success('Sucesso!', 'Avaliação excluída com sucesso!');
            this.carregarAvaliacoes();
          });
      }
    });
  }
}
