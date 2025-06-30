import {Component, OnInit, inject, input} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfAlunoObterDto,
  AlunoObterQuery,
  AlunoObterDto,
  AlunosClient,
  CursosClient,
  CursoObterQuery,
  CursoObterDto,
} from 'web-api-client';

@Component({
    templateUrl: './aluno-listar.component.html',
    selector: 'aluno-listar',
    standalone: false
})
export class AlunoListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);
  cursoId = input<number>(null);
  isTabelaRelacionada = input<boolean>(false);
  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  alunos = new PaginatedListOfAlunoObterDto();
  carregandoAlunos: boolean;
  filtro: AlunoObterQuery = <AlunoObterQuery>{};

  alunoEditado: AlunoObterDto | null = null;
  cursos: CursoObterDto[] = [];

  constructor(
    private alunosClient: AlunosClient,
    private cursosClient: CursosClient,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.carregarCursos();
    this.pesquisar();
  }

  carregarCursos() {
    const query = new CursoObterQuery();
    query.pageSize = -1;

    this.cursosClient.obter(query)
      .subscribe(res => {
        this.cursos = res.items || [];
      });
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

    this.carregandoAlunos = true;
    this.carregarAlunos();
  }

  private carregarAlunos() {
    this.alunosClient
      .obter(this.filtro)
      .subscribe(r => {
        this.alunos = r;
      })
      .add(() => (this.carregandoAlunos = false));
  }

  editar(aluno: AlunoObterDto) {
    this.router.navigate(['alunos', 'editar', aluno.id]);
  }

  excluir(aluno: AlunoObterDto) {
    this.alunosClient.excluir(aluno.id!).subscribe(() => {
      this.notification.success('Aluno excluído com sucesso!', '');
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

  getCursoNome(cursoId: number): string {
    const curso = this.cursos.find(c => c.id === cursoId);
    return curso ? curso.nome : '';
  }
}
