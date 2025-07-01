import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  DisciplinaSalvarCommand,
  DisciplinasClient,
  DisciplinaObterPorIdDto,
  CursosClient,
  CursoObterQuery,
  CursoObterDto,
} from 'web-api-client';

@Component({
    templateUrl: './disciplina-form.component.html',
    styleUrls: ['./disciplina-form.component.scss'],
    standalone: false
})
export class DisciplinaFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
    codigo: new FormControl<string>(null, Validators.required),
    cargaHoraria: new FormControl<number>(null, [Validators.required, Validators.min(1)]),
    ementa: new FormControl<string>(null),
    cursoId: new FormControl<number>(null, Validators.required),
  });
  salvandoDisciplina: boolean;

  disciplinaId;
  disciplina: DisciplinaObterPorIdDto;
  carregandoDisciplina: boolean;

  cursos: CursoObterDto[] = [];
  carregandoCursos: boolean;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private disciplinasClient: DisciplinasClient,
    private cursosClient: CursosClient
  ) {}

  ngOnInit(): void {
    this.carregarCursos();

    this.disciplinaId = this.route.snapshot.paramMap.get('id');
    if (this.disciplinaId) {
      this.carregarDisciplina();
    }
  }

  private carregarCursos() {
    this.carregandoCursos = true;
    const query = new CursoObterQuery();
    query.pageSize = -1; // Carregar todos os cursos

    this.cursosClient.obter(query).subscribe(response => {
      this.cursos = response.items || [];
    }).add(() => {
      this.carregandoCursos = false;
    });
  }

  carregarDisciplina() {
    this.disciplinasClient.obterPorId(+this.disciplinaId).subscribe(res => {
      this.form.patchValue(res);
      this.disciplina = res;
    });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new DisciplinaSalvarCommand(fValue);

      this.salvandoDisciplina = true;
      this.disciplinasClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoDisciplina = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Disciplina salva com sucesso!');
          if (this.disciplinaId) {
            this.carregarDisciplina();
          } else {
            this.router.navigate(['disciplinas', 'editar', r]);
          }
        });
    } else {
      this.nzModalService.error({
        nzTitle: 'Formulário Inválido',
        nzContent: 'Verifique o formulário e preencha corretamente os campos obrigatórios!'
      });
    }
  }
}
