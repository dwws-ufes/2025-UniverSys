import {Component, OnInit, inject} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {NzTableQueryParams} from 'ng-zorro-antd/table';
import {
  PaginatedListOfUsuarioDto,
  UsuarioObterQuery,
  UsuarioDto,
  UsuariosClient,
} from 'web-api-client';

@Component({
    templateUrl: './usuario-listar.component.html',
    standalone: false
})
export class UsuarioListarComponent implements OnInit {
  notification = inject(NzNotificationService);
  fb = inject(FormBuilder);

  showFilters = false;
  pageIndex = 1;
  pageSize = 10;
  usuarios = new PaginatedListOfUsuarioDto();
  carregandoUsuarios: boolean;
  filtro: UsuarioObterQuery = <UsuarioObterQuery>{};

  usuarioEditado: UsuarioDto | null = null;

  constructor(
    private usuariosClient: UsuariosClient,
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

    this.carregandoUsuarios = true;
    this.carregarUsuarios();
  }

  private carregarUsuarios() {
    this.usuariosClient
      .obter(this.filtro)
      .subscribe(r => {
        this.usuarios = r;
      })
      .add(() => (this.carregandoUsuarios = false));
  }

  editar(u: UsuarioDto) {
    this.router.navigate(['usuarios', 'editar', u.id]);
  }

  excluir(usuario: UsuarioDto) {
    this.usuariosClient.excluir(usuario.id!).subscribe(() => {
      this.notification.success('Usuário excluído com sucesso!', '');
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
