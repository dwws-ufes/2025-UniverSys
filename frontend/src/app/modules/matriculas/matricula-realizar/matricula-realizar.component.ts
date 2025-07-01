import {Component, OnInit, Output, EventEmitter} from '@angular/core';
import {
  TurmasClient,
  TurmaObterDto,
  TurmaObterQuery,
  PaginatedListOfTurmaObterDto,
  DisciplinasClient,
  DisciplinaObterDto,
  DisciplinaObterQuery,
  PaginatedListOfDisciplinaObterDto,
  MatriculasClient,
  MatriculaRealizarCommand
} from 'web-api-client';
import { AuthenticationService } from '../../../shared/services/authentication.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-matricula-realizar',
  templateUrl: './matricula-realizar.component.html',
  styleUrls: ['./matricula-realizar.component.scss'],
  standalone: false
})
export class MatriculaRealizarComponent implements OnInit {
  @Output() matriculaRealizada = new EventEmitter<void>();
  @Output() fecharModal = new EventEmitter<void>();

  currentStep = 0;
  carregandoDisciplinas = false;
  carregandoTurmas = false;
  processandoMatricula = false;

  disciplinasDisponiveis: DisciplinaObterDto[] = [];
  disciplinasFiltradas: DisciplinaObterDto[] = [];
  disciplinaSelecionada: DisciplinaObterDto | null = null;
  termoBuscaDisciplina = '';

  turmasDisponiveis: TurmaObterDto[] = [];
  turmasFiltradas: TurmaObterDto[] = [];
  turmaSelecionada: TurmaObterDto | null = null;
  termoBuscaTurma = '';

  constructor(
    private disciplinasClient: DisciplinasClient,
    private turmasClient: TurmasClient,
    private matriculasClient: MatriculasClient,
    private authService: AuthenticationService,
    private notification: NzNotificationService
  ) {}

  ngOnInit(): void {
    this.carregarDisciplinasDisponiveis();
  }

  carregarDisciplinasDisponiveis(): void {
    this.carregandoDisciplinas = true;

    const user = this.authService.obterUsuarioLogado;
    const alunoCursoId = user?.alunoCursoId;
    const query = new DisciplinaObterQuery({
      pageSize: -1,
      cursoId: alunoCursoId,
      alunoId: user?.alunoId
    });

    this.disciplinasClient.obter(query).subscribe({
      next: (result: PaginatedListOfDisciplinaObterDto) => {
        this.disciplinasDisponiveis = result.items || [];
        this.disciplinasFiltradas = [...this.disciplinasDisponiveis];
      }
    }).add(() => {
      this.carregandoDisciplinas = false;
    });
  }

  carregarTurmasDaDisciplina(): void {
    if (!this.disciplinaSelecionada) return;

    this.carregandoTurmas = true;

    const query = new TurmaObterQuery({
      pageSize: -1,
      disciplinaId: this.disciplinaSelecionada.id
    });

    this.turmasClient.obter(query).subscribe({
      next: (result: PaginatedListOfTurmaObterDto) => {
        this.turmasDisponiveis = result.items || [];
        this.turmasFiltradas = [...this.turmasDisponiveis];
      }
    }).add(() => {
      this.carregandoTurmas = false;
    });
  }

  filtrarDisciplinas(): void {
    if (!this.termoBuscaDisciplina.trim()) {
      this.disciplinasFiltradas = [...this.disciplinasDisponiveis];
      return;
    }

    const termo = this.termoBuscaDisciplina.toLowerCase();
    this.disciplinasFiltradas = this.disciplinasDisponiveis.filter(disciplina =>
      disciplina.nome?.toLowerCase().includes(termo) ||
      disciplina.codigo?.toLowerCase().includes(termo) ||
      disciplina.ementa?.toLowerCase().includes(termo)
    );
  }

  filtrarTurmas(): void {
    if (!this.termoBuscaTurma.trim()) {
      this.turmasFiltradas = [...this.turmasDisponiveis];
      return;
    }

    const termo = this.termoBuscaTurma.toLowerCase();
    this.turmasFiltradas = this.turmasDisponiveis.filter(turma =>
      turma.nome?.toLowerCase().includes(termo) ||
      turma.semestreAno?.toString().includes(termo) ||
      turma.semestrePeriodo?.toString().includes(termo)
    );
  }

  selecionarDisciplina(disciplina: DisciplinaObterDto): void {
    this.disciplinaSelecionada = disciplina;
    this.turmaSelecionada = null;
    this.termoBuscaTurma = '';
  }

  selecionarTurma(turma: TurmaObterDto): void {
    this.turmaSelecionada = turma;
  }

  proximoStep(): void {
    if (this.currentStep === 0) {
      this.carregarTurmasDaDisciplina();
      this.currentStep++;
    } else if (this.currentStep < 2) {
      this.currentStep++;
    }
  }

  voltarStep(): void {
    if (this.currentStep > 0) {
      this.currentStep--;
    }
  }

  podeAvancar(): boolean {
    switch (this.currentStep) {
      case 0:
        return this.disciplinaSelecionada !== null;
      case 1:
        return this.turmaSelecionada !== null;
      case 2:
        return this.turmaSelecionada !== null;
      default:
        return false;
    }
  }

  realizarMatricula(): void {
    if (!this.turmaSelecionada) {
      this.notification.error('Erro', 'Nenhuma turma selecionada.');
      return;
    }

    const user = this.authService.obterUsuarioLogado;
    let alunoId = user?.alunoId

    this.processandoMatricula = true;

    const command = new MatriculaRealizarCommand({
      alunoId: alunoId,
      turmaId: this.turmaSelecionada.id
    });

        this.matriculasClient.realizarMatricula(command).subscribe({
      next: (result) => {
        this.matriculaRealizada.emit();
        this.fecharModal.emit();
      }
    }).add(() => {
      this.processandoMatricula = false;
    });
  }

  cancelar(): void {
    this.fecharModal.emit();
  }
}
