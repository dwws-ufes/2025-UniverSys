import {Component, OnInit, inject} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfCursoObterDto,
  CursoObterQuery,
  CursoObterDto,
  CursosClient,
} from 'web-api-client';

@Component({
    templateUrl: './curso-listar.component.html',
    standalone: false
})
export class CursoListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);

  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  cursos = new PaginatedListOfCursoObterDto();
  carregandoCursos: boolean;
  filtro: CursoObterQuery = <CursoObterQuery>{};

  cursoEditado: CursoObterDto | null = null;

  constructor(
    private cursosClient: CursosClient,
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

    Object.keys(this.filtro).forEach(k => (this.filtro[k] = this.filtro[k] === '' ? null : this.filtro[k]));

    this.carregandoCursos = true;
    this.carregarCursos();
  }

  private carregarCursos() {
    this.cursosClient
      .obter(this.filtro)
      .subscribe(r => {
        this.cursos = r;
      })
      .add(() => (this.carregandoCursos = false));
  }

  editar(curso: CursoObterDto) {
    this.router.navigate(['cursos', 'editar', curso.id]);
  }

  excluir(curso: CursoObterDto) {
    this.cursosClient.excluir(curso.id!).subscribe(() => {
      this.notification.success('Curso excluído com sucesso!', '');
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
