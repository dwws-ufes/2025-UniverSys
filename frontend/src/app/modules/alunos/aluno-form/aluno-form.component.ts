import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  AlunoSalvarCommand,
  AlunosClient,
  AlunoObterPorIdDto,
  CursosClient,
  CursoObterQuery,
  CursoObterDto,
} from 'web-api-client';

@Component({
    templateUrl: './aluno-form.component.html',
    styleUrls: ['./aluno-form.component.scss'],
    standalone: false
})
export class AlunoFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    matricula: new FormControl<string>(null, Validators.required),
    nome: new FormControl<string>(null, Validators.required),
    cpf: new FormControl<string>(null, Validators.required),
    cursoId: new FormControl<number>(null, Validators.required),
  });
  salvandoAluno: boolean;

  alunoId;
  aluno: AlunoObterPorIdDto;
  carregandoAluno: boolean;

  cursos: CursoObterDto[] = [];
  carregandoCursos: boolean;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private alunosClient: AlunosClient,
    private cursosClient: CursosClient
  ) {}

  ngOnInit(): void {
    this.alunoId = this.route.snapshot.paramMap.get('id');
    this.carregarCursos();

    if (this.alunoId) {
      this.carregarAluno();
    }
  }

  carregarCursos() {
    this.carregandoCursos = true;
    const query = new CursoObterQuery();
    query.pageSize = -1;

    this.cursosClient.obter(query)
      .pipe(finalize(() => this.carregandoCursos = false))
      .subscribe(res => {
        this.cursos = res.items || [];
      });
  }

  carregarAluno() {
    this.carregandoAluno = true;
    this.alunosClient.obterPorId(+this.alunoId)
      .pipe(finalize(() => this.carregandoAluno = false))
      .subscribe(res => {
        this.form.patchValue(res);
        this.aluno = res;
      });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new AlunoSalvarCommand(fValue);

      this.salvandoAluno = true;
      this.alunosClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoAluno = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Aluno salvo com sucesso!');
          if (this.alunoId) {
            this.carregarAluno();
          } else {
            this.router.navigate(['alunos', 'editar', r]);
          }
        });
    }
  }
}
