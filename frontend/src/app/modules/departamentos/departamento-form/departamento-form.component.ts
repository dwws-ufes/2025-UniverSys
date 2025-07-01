import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {finalize} from 'rxjs/operators';
import {
  DepartamentoSalvarCommand,
  DepartamentosClient,
  DepartamentoObterPorIdDto,
} from 'web-api-client';

@Component({
    templateUrl: './departamento-form.component.html',
    styleUrls: ['./departamento-form.component.scss'],
    standalone: false
})
export class DepartamentoFormComponent implements OnInit {
  nzModalService = inject(NzModalService);
  fb = inject(FormBuilder);
  form = this.fb.group({
    id: new FormControl<number>(null),
    nome: new FormControl<string>(null, Validators.required),
  });
  salvandoDepartamento: boolean;

  departamentoId;
  departamento: DepartamentoObterPorIdDto;
  carregandoDepartamento: boolean;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private nzNotificationService: NzNotificationService,
    private departamentosClient: DepartamentosClient
  ) {}

  ngOnInit(): void {
    this.departamentoId = this.route.snapshot.paramMap.get('id');
    if (this.departamentoId) {
      this.carregarDepartamento();
    }
  }

  carregarDepartamento() {
    this.departamentosClient.obterPorId(+this.departamentoId).subscribe(res => {
      this.form.patchValue(res);
      this.departamento = res;
    });
  }

  salvar() {
    if (this.form.valid) {
      const fValue = this.form.value;
      const req = new DepartamentoSalvarCommand(fValue);

      this.salvandoDepartamento = true;
      this.departamentosClient.salvar(req)
        .pipe(
          finalize(() => {
            this.salvandoDepartamento = false;
          })
        )
        .subscribe(r => {
          this.nzNotificationService.success('Sucesso!', 'Departamento salvo com sucesso!');
          if (this.departamentoId) {
            this.carregarDepartamento();
          } else {
            this.router.navigate(['departamentos', 'editar', r]);
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
