import {Component, OnInit, inject, input} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfMatriculaObterDto,
  MatriculaObterQuery,
  MatriculaObterDto,
  MatriculasClient,
  AvaliacoesClient,
  NotasClient,
  AvaliacaoObterDto,
  AvaliacaoObterQuery,
  NotaObterDto,
  NotaObterQuery,
  NotaSalvarCommand,
} from 'web-api-client';
import {AuthenticationService} from '../../../shared/services/authentication.service';
import {NzModalService} from 'ng-zorro-antd/modal';

@Component({
  templateUrl: './matriculas-listar.component.html',
  selector: 'matriculas-listar',
  standalone: false,
})
export class MatriculasListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);
  authService = inject(AuthenticationService);
  turmaId = input<number>(null);
  alunoId = input<number>(null);
  isTabelaRelacionada = input<boolean>(false);
  modalRealizarMatriculaVisivel = false;
  modalNotasVisivel = false;
  pageIndex = 1;
  pageSize = 10;
  matriculas = new PaginatedListOfMatriculaObterDto();
  carregandoMatriculas: boolean;
  filtro: MatriculaObterQuery = <MatriculaObterQuery>{};

  matriculaSelecionada: MatriculaObterDto | null = null;
  avaliacoes: AvaliacaoObterDto[] = [];
  notas: NotaObterDto[] = [];
  carregandoNotas = false;
  carregandoAvaliacoes = false;
  salvandoNotas = false;
  notasForm: FormGroup = this.fb.group({});

  constructor(
    private matriculasClient: MatriculasClient,
    private avaliacoesClient: AvaliacoesClient,
    private notasClient: NotasClient,
    private nzModalService: NzModalService
  ) {}

  ngOnInit(): void {
    this.pesquisar();
  }

  pesquisar(
    pageIndex: number = this.pageIndex,
    pageSize: number = this.pageSize,
    sortField: string | null = null,
    sortOrder: string | null = null
  ): void {
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;

    this.filtro.pageIndex = pageIndex;
    this.filtro.pageSize = pageSize;
    this.filtro.sortField = sortField;
    this.filtro.sortOrder = sortOrder;

    this.filtro.alunoId = this.alunoId() ?? +this.authService.obterUsuarioLogado.alunoId;
    this.filtro.turmaId = this.turmaId();

    Object.keys(this.filtro).forEach(k => (this.filtro[k] = this.filtro[k] === '' ? null : this.filtro[k]));

    this.carregandoMatriculas = true;
    this.carregarMatriculas();
  }

  private carregarMatriculas() {
    this.matriculasClient
      .obter(this.filtro)
      .subscribe(r => {
        this.matriculas = r;
      })
      .add(() => (this.carregandoMatriculas = false));
  }

  abrirModalRealizarMatricula() {
    this.modalRealizarMatriculaVisivel = true;
  }

  fecharModal() {
    this.modalRealizarMatriculaVisivel = false;
  }

  onMatriculaRealizada() {
    this.fecharModal();
    this.notification.success('Matrícula realizada com sucesso!', '');
    this.pesquisar();
  }

  cancelarMatricula(matricula: MatriculaObterDto) {
    this.matriculasClient.excluir(matricula.id!).subscribe(() => {
      this.notification.success('Matrícula cancelada com sucesso!', '');
      this.pesquisar();
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const {pageSize, pageIndex, sort} = params;
    let aux = sort.find(v => v.value != null);
    let sortField = aux?.key ?? null;
    let sortOrder = aux?.value ?? null;

    this.pesquisar(pageIndex, pageSize, sortField, sortOrder);
  }

  isAluno(): boolean {
    return this.authService.isAluno();
  }

  isProfessor(): boolean {
    return this.authService.isProfessor();
  }

  abrirModalNotas(matricula: MatriculaObterDto) {
    this.matriculaSelecionada = matricula;
    this.modalNotasVisivel = true;
    this.carregarAvaliacoesENotas();
  }

  fecharModalNotas() {
    this.modalNotasVisivel = false;
    this.matriculaSelecionada = null;
    this.avaliacoes = [];
    this.notas = [];
    this.notasForm = this.fb.group({});
  }

  private carregarAvaliacoesENotas() {
    if (!this.matriculaSelecionada?.turmaId) return;

    this.carregandoAvaliacoes = true;
    this.carregandoNotas = true;

    const avaliacaoQuery = new AvaliacaoObterQuery({
      turmaId: this.matriculaSelecionada.turmaId,
      pageSize: -1,
      pageIndex: 1,
    });

    this.avaliacoesClient
      .obter(avaliacaoQuery)
      .subscribe(result => {
        this.avaliacoes = result.items || [];
        this.carregarNotas();
      })
      .add(() => (this.carregandoAvaliacoes = false));
  }

  private carregarNotas() {
    if (!this.matriculaSelecionada?.id) return;

    const notaQuery = new NotaObterQuery({
      matriculaId: this.matriculaSelecionada.id,
      pageSize: -1,
      pageIndex: 1,
    });

    this.notasClient
      .obter(notaQuery)
      .subscribe(result => {
        this.notas = result.items || [];
        this.criarFormularioNotas();
      })
      .add(() => (this.carregandoNotas = false));
  }

  private criarFormularioNotas() {
    const formControls: {[key: string]: any} = {};

    this.avaliacoes.forEach(avaliacao => {
      const notaExistente = this.notas.find(n => n.avaliacaoId === avaliacao.id);
      const valorNota = notaExistente?.valor ?? null;

      formControls[`avaliacao_${avaliacao.id}`] = [valorNota, [Validators.min(0), Validators.max(10)]];
    });

    this.notasForm = this.fb.group(formControls);
  }

  salvarNotas() {
    if (!this.notasForm.valid || !this.matriculaSelecionada?.id) {
      this.nzModalService.error({
        nzTitle: 'Formulário Inválido',
        nzContent: 'Verifique o formulário e preencha corretamente os campos obrigatórios!'
      });
      return;
    }

    this.salvandoNotas = true;
    const promises: Promise<any>[] = [];

    this.avaliacoes.forEach(avaliacao => {
      const valorNota = this.notasForm.get(`avaliacao_${avaliacao.id}`)?.value;
      const notaExistente = this.notas.find(n => n.avaliacaoId === avaliacao.id);

      if (valorNota !== null && valorNota !== undefined && valorNota !== '') {
        const comando = new NotaSalvarCommand({
          id: notaExistente?.id,
          avaliacaoId: avaliacao.id!,
          matriculaId: this.matriculaSelecionada!.id!,
          valor: parseFloat(valorNota),
        });

        promises.push(this.notasClient.salvar(comando).toPromise());
      } else if (notaExistente?.id) {
        promises.push(this.notasClient.excluir(notaExistente.id).toPromise());
      }
    });

    Promise.all(promises)
      .then(() => {
        this.notification.success('Notas salvas com sucesso!', '');
        this.fecharModalNotas();
      })
      .catch(() => {
        this.notification.error('Erro ao salvar notas', '');
      })
      .finally(() => {
        this.salvandoNotas = false;
      });
  }

  calcularMediaFinal(): number {
    if (!this.avaliacoes.length) return 0;

    let somaNotas = 0;
    let somaPesos = 0;
    let temNotas = false;

    this.avaliacoes.forEach(avaliacao => {
      const valorNota = this.notasForm.get(`avaliacao_${avaliacao.id}`)?.value;

    if (valorNota !== null && valorNota !== undefined && valorNota !== '') {
        somaNotas += parseFloat(valorNota) * (avaliacao.peso || 1);
        somaPesos += avaliacao.peso || 1;
        temNotas = true;
      }
    });

    const media = temNotas && somaPesos > 0 ? somaNotas / somaPesos : 0;
    return Math.round(media * 100) / 100;
  }

  obterTituloMedia(): string {
    if (!this.avaliacoes.length) return 'Média Final';

    const totalAvaliacoes = this.avaliacoes.length;
    let avaliacoesComNota = 0;

    this.avaliacoes.forEach(avaliacao => {
      const valorNota = this.notasForm.get(`avaliacao_${avaliacao.id}`)?.value;
      if (valorNota !== null && valorNota !== undefined && valorNota !== '') {
        avaliacoesComNota++;
      }
    });

    return avaliacoesComNota === totalAvaliacoes ? 'Média Final' : 'Média Parcial';
  }

  obterStatus(): string {
    const titulo = this.obterTituloMedia();

    if (titulo === 'Média Parcial') {
      return 'Aguardando média final';
    }

    const media = this.calcularMediaFinal();
    if (media >= 7) return 'Aprovado';
    if (media >= 5) return 'Em Recuperação';
    return 'Reprovado';
  }

  obterCorStatus(): string {
    const titulo = this.obterTituloMedia();

    if (titulo === 'Média Parcial') {
      return 'default';
    }

    const media = this.calcularMediaFinal();
    if (media >= 7) return 'green';
    if (media >= 5) return 'gold';
    return 'red';
  }
}
