import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  CursoSalvarCommand,
  CursosClient,
  CursoObterPorIdDto,
  CursoObterResumoDto,
} from 'web-api-client';

@Component({
    templateUrl: './curso-form.component.html',
    styleUrls: ['./curso-form.component.scss'],
    standalone: false
})
export class CursoFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
    duracaoSemestres: new FormControl<number>(null, [Validators.required, Validators.min(1)]),
  });
  salvandoCurso: boolean;

  cursoId;
  curso: CursoObterPorIdDto;
  carregandoCurso: boolean;

  resumo: CursoObterResumoDto;
  carregandoResumo: boolean = false;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private cursosClient: CursosClient
  ) {}

  ngOnInit(): void {
    this.cursoId = this.route.snapshot.paramMap.get('id');
    if (this.cursoId) {
      this.carregarCurso();
    }
  }

  carregarCurso() {
    this.carregandoCurso = true;
    this.cursosClient.obterPorId(+this.cursoId).subscribe(res => {
      this.form.patchValue(res);
      this.curso = res;
      this.carregandoCurso = false;

      if (res.nome) {
        this.carregarResumo(res.nome);
      }
    });
  }

  carregarResumo(nomeCurso: string) {
    if (!nomeCurso) return;

    this.carregandoResumo = true;
    this.cursosClient.obterResumo(nomeCurso).subscribe({
      next: (resumo) => {
        this.resumo = resumo;
        this.carregandoResumo = false;
      }
    })
    .add(() => {
      this.carregandoResumo = false;
    });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new CursoSalvarCommand(fValue);

      this.salvandoCurso = true;
      this.cursosClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoCurso = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Curso salvo com sucesso!');
          if (this.cursoId) {
            this.carregarCurso();
          } else {
            this.router.navigate(['cursos', 'editar', r]);
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
