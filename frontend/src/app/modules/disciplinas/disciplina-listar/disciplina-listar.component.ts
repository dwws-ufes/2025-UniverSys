import {Component, OnInit, inject, input} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfDisciplinaObterDto,
  DisciplinaObterQuery,
  DisciplinaObterDto,
  DisciplinasClient,
} from 'web-api-client';

@Component({
    templateUrl: './disciplina-listar.component.html',
    selector: 'disciplina-listar',
    standalone: false
})
export class DisciplinaListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);
  cursoId = input<number>(null);
  isTabelaRelacionada = input<boolean>(false);
  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  disciplinas = new PaginatedListOfDisciplinaObterDto();
  carregandoDisciplinas: boolean;
  filtro: DisciplinaObterQuery = <DisciplinaObterQuery>{};

  disciplinaEditada: DisciplinaObterDto | null = null;

  constructor(
    private disciplinasClient: DisciplinasClient,
    private router: Router,
    private route: ActivatedRoute
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

    this.filtro.cursoId = this.cursoId();

    Object.keys(this.filtro).forEach(k => (this.filtro[k] = this.filtro[k] === '' ? null : this.filtro[k]));

    this.carregandoDisciplinas = true;
    this.carregarDisciplinas();
  }

  private carregarDisciplinas() {
    this.disciplinasClient
      .obter(this.filtro)
      .subscribe(r => {
        this.disciplinas = r;
      })
      .add(() => (this.carregandoDisciplinas = false));
  }

  editar(disciplina: DisciplinaObterDto) {
    this.router.navigate(['disciplinas', 'editar', disciplina.id]);
  }

  excluir(disciplina: DisciplinaObterDto) {
    this.disciplinasClient.excluir(disciplina.id!).subscribe(() => {
      this.notification.success('Disciplina excluída com sucesso!', '');
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
}
