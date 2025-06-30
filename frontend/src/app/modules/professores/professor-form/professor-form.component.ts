import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  ProfessorSalvarCommand,
  ProfessoresClient,
  ProfessorObterPorIdDto,
  DepartamentosClient,
  DepartamentoObterQuery,
  DepartamentoObterDto,
  SelectItemEnum,
} from 'web-api-client';

@Component({
    templateUrl: './professor-form.component.html',
    styleUrls: ['./professor-form.component.scss'],
    standalone: false
})
export class ProfessorFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
    cpf: new FormControl<string>(null, Validators.required),
    especializacao: new FormControl<number>(null, Validators.required),
    departamentoId: new FormControl<number>(null, Validators.required),
  });
  salvandoProfessor: boolean;

  professorId;
  professor: ProfessorObterPorIdDto;
  carregandoProfessor: boolean;

  departamentos: DepartamentoObterDto[] = [];
  carregandoDepartamentos: boolean;

  especializacoes: SelectItemEnum[] = [];
  carregandoEspecializacoes: boolean;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private professoresClient: ProfessoresClient,
    private departamentosClient: DepartamentosClient
  ) {}

  ngOnInit(): void {
    this.professorId = this.route.snapshot.paramMap.get('id');
    this.carregarDepartamentos();
    this.carregarEspecializacoes();

    if (this.professorId) {
      this.carregarProfessor();
    }
  }

  carregarDepartamentos() {
    this.carregandoDepartamentos = true;
    const query = new DepartamentoObterQuery();
    query.pageSize = -1;

    this.departamentosClient.obter(query)
      .pipe(finalize(() => this.carregandoDepartamentos = false))
      .subscribe(res => {
        this.departamentos = res.items || [];
      });
  }

  carregarEspecializacoes() {
    this.carregandoEspecializacoes = true;

    this.professoresClient.obterEspecializacoes()
      .pipe(finalize(() => this.carregandoEspecializacoes = false))
      .subscribe(res => {
        this.especializacoes = res || [];
      });
  }

  carregarProfessor() {
    this.carregandoProfessor = true;
    this.professoresClient.obterPorId(+this.professorId)
      .pipe(finalize(() => this.carregandoProfessor = false))
      .subscribe(res => {
        this.form.patchValue(res);
        this.professor = res;
      });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new ProfessorSalvarCommand(fValue);

      this.salvandoProfessor = true;
      this.professoresClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoProfessor = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Professor salvo com sucesso!');
          if (this.professorId) {
            this.carregarProfessor();
          } else {
            this.router.navigate(['professores', 'editar', r]);
          }
        });
    }
  }
}
