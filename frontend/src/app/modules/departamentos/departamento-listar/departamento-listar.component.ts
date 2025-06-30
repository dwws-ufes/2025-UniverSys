import {Component, OnInit, inject} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfDepartamentoObterDto,
  DepartamentoObterQuery,
  DepartamentoObterDto,
  DepartamentosClient,
} from 'web-api-client';

@Component({
    templateUrl: './departamento-listar.component.html',
    standalone: false
})
export class DepartamentoListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);

  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  departamentos = new PaginatedListOfDepartamentoObterDto();
  carregandoDepartamentos: boolean;
  filtro: DepartamentoObterQuery = <DepartamentoObterQuery>{};

  departamentoEditado: DepartamentoObterDto | null = null;

  constructor(
    private departamentosClient: DepartamentosClient,
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

    this.carregandoDepartamentos = true;
    this.carregarDepartamentos();
  }

  private carregarDepartamentos() {
    this.departamentosClient
      .obter(this.filtro)
      .subscribe(r => {
        this.departamentos = r;
      })
      .add(() => (this.carregandoDepartamentos = false));
  }

  editar(departamento: DepartamentoObterDto) {
    this.router.navigate(['departamentos', 'editar', departamento.id]);
  }

  excluir(departamento: DepartamentoObterDto) {
    this.departamentosClient.excluir(departamento.id!).subscribe(() => {
      this.notification.success('Departamento excluído com sucesso!', '');
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
