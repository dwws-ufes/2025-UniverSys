import {Component, OnInit, inject, input} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfProfessorObterDto,
  ProfessorObterQuery,
  ProfessorObterDto,
  ProfessoresClient,
} from 'web-api-client';

@Component({
    templateUrl: './professor-listar.component.html',
    selector: 'professor-listar',
    standalone: false
})
export class ProfessorListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);
  departamentoId = input<number>(null);
  isTabelaRelacionada = input<boolean>(false);
  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  professores = new PaginatedListOfProfessorObterDto();
  carregandoProfessores: boolean;
  filtro: ProfessorObterQuery = <ProfessorObterQuery>{};

  professorEditado: ProfessorObterDto | null = null;

  constructor(
    private professoresClient: ProfessoresClient,
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

    this.filtro.departamentoId = this.departamentoId();

    Object.keys(this.filtro).forEach(k => (this.filtro[k] = this.filtro[k] === '' ? null : this.filtro[k]));

    this.carregandoProfessores = true;
    this.carregarProfessores();
  }

  private carregarProfessores() {
    this.professoresClient
      .obter(this.filtro)
      .subscribe(r => {
        this.professores = r;
      })
      .add(() => (this.carregandoProfessores = false));
  }

  editar(professor: ProfessorObterDto) {
    this.router.navigate(['professores', 'editar', professor.id]);
  }

  excluir(professor: ProfessorObterDto) {
    this.professoresClient.excluir(professor.id!).subscribe(() => {
      this.notification.success('Professor excluído com sucesso!', '');
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

  getEspecializacaoText(especializacao: number): string {
    switch (especializacao) {
      case 0: return 'Graduação';
      case 1: return 'Mestrado';
      case 2: return 'Doutorado';
      default: return '';
    }
  }
}
