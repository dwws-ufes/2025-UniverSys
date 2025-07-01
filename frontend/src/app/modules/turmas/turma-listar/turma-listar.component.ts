import {Component, OnInit, inject, input} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfTurmaObterDto,
  TurmaObterQuery,
  TurmaObterDto,
  TurmasClient,
} from 'web-api-client';
import { AuthenticationService } from '../../../shared/services/authentication.service';

@Component({
    templateUrl: './turma-listar.component.html',
    selector: 'turma-listar',
    standalone: false
})
export class TurmaListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);
  disciplinaId = input<number>(null);
  professorId = input<number>(null);
  isTabelaRelacionada = input<boolean>(false);
  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  turmas = new PaginatedListOfTurmaObterDto();
  carregandoTurmas: boolean;
  filtro: TurmaObterQuery = <TurmaObterQuery>{};

  turmaEditada: TurmaObterDto | null = null;

  constructor(
    private turmasClient: TurmasClient,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthenticationService
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

    this.filtro.disciplinaId = this.disciplinaId();
    this.filtro.professorId = this.professorId();

    Object.keys(this.filtro).forEach(k => (this.filtro[k] = this.filtro[k] === '' ? null : this.filtro[k]));

    this.carregandoTurmas = true;
    this.carregarTurmas();
  }

  private carregarTurmas() {
    this.turmasClient
      .obter(this.filtro)
      .subscribe(r => {
        this.turmas = r;
      })
      .add(() => (this.carregandoTurmas = false));
  }

  editar(turma: TurmaObterDto) {
    this.router.navigate(['turmas', 'editar', turma.id]);
  }

  excluir(turma: TurmaObterDto) {
    this.turmasClient.excluir(turma.id!).subscribe(() => {
      this.notification.success('Turma excluída com sucesso!', '');
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

  isProfessor(): boolean {
    return this.authService.isProfessor();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  visualizar(turma: TurmaObterDto) {
    this.router.navigate(['turmas', 'editar', turma.id]);
  }
}
