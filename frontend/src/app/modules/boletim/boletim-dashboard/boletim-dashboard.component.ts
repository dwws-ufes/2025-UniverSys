import { Component, OnInit } from '@angular/core';
import {
  MatriculasClient,
  NotasClient,
  AvaliacoesClient,
  MatriculaObterQuery,
  MatriculaObterDto,
  NotaObterQuery,
  NotaObterDto,
  AvaliacaoObterQuery,
  AvaliacaoObterDto
} from 'web-api-client';
import { AuthenticationService } from '../../../shared/services/authentication.service';
import { forkJoin, map, switchMap } from 'rxjs';

interface BoletimItem {
  matricula: MatriculaObterDto;
  avaliacoes: AvaliacaoObterDto[];
  notas: NotaObterDto[];
  mediaFinal: number | null;
  situacao: 'Aprovado' | 'Reprovado' | 'Em Andamento';
}

@Component({
  selector: 'app-boletim-dashboard',
  templateUrl: './boletim-dashboard.component.html',
  styleUrls: ['./boletim-dashboard.component.scss'],
  standalone: false
})
export class BoletimDashboardComponent implements OnInit {
  loading = false;
  boletimItens: BoletimItem[] = [];
  alunoId: number | null = null;

  constructor(
    private matriculasClient: MatriculasClient,
    private notasClient: NotasClient,
    private avaliacoesClient: AvaliacoesClient,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.alunoId = this.authService.getAlunoId();
    if (this.alunoId) {
      this.carregarBoletim();
    }
  }

  carregarBoletim(): void {
    if (!this.alunoId) return;

    this.loading = true;
    const query = new MatriculaObterQuery();
    query.alunoId = this.alunoId;
    query.pageSize = -1;

    this.matriculasClient.obter(query).pipe(
      switchMap(matriculas => {
        const boletimObservables = matriculas.items?.map(matricula =>
          this.processarMatricula(matricula)
        ) || [];
        return forkJoin(boletimObservables);
      })
    ).subscribe({
      next: (itens) => {
        this.boletimItens = itens
      }
    })
    .add(() => {
      this.loading = false;
    });
  }

  private processarMatricula(matricula: MatriculaObterDto) {
    const avaliacoesQuery = new AvaliacaoObterQuery();
    avaliacoesQuery.turmaId = matricula.turmaId;
    avaliacoesQuery.pageSize = -1;

    return this.avaliacoesClient.obter(avaliacoesQuery).pipe(
      switchMap(avaliacoes => {
        const notasQuery = new NotaObterQuery();
        notasQuery.matriculaId = matricula.id;
        notasQuery.pageSize = -1;

        return this.notasClient.obter(notasQuery).pipe(
          map(notas => {
            const mediaFinal = this.calcularMedia(avaliacoes.items || [], notas.items || []);
            const situacao = this.determinarSituacao(mediaFinal);
            return {
              matricula,
              avaliacoes: avaliacoes.items || [],
              notas: notas.items || [],
              mediaFinal,
              situacao
            } as BoletimItem;
          })
        );
      })
    );
  }

  private calcularMedia(avaliacoes: AvaliacaoObterDto[], notas: NotaObterDto[]): number | null {
    if (avaliacoes.length === 0) return null;

    let somaNotas = 0;
    let somaPesos = 0;
    let temNota = false;

    avaliacoes.forEach(avaliacao => {
      const nota = notas.find(n => n.avaliacaoId === avaliacao.id);
      if (nota && nota.valor !== undefined && nota.valor !== null) {
        somaNotas += nota.valor * (avaliacao.peso || 1);
        somaPesos += avaliacao.peso || 1;
        temNota = true;
      }
    });

    return temNota && somaPesos > 0 ? somaNotas / somaPesos : null;
  }

  private determinarSituacao(media: number | null): 'Aprovado' | 'Reprovado' | 'Em Andamento' {
    if (media === null) return 'Em Andamento';
    return media >= 7.0 ? 'Aprovado' : media >= 5.0 ? 'Em Andamento' : 'Reprovado';
  }

  getCorSituacao(situacao: string): string {
    switch (situacao) {
      case 'Aprovado': return '#52c41a';
      case 'Reprovado': return '#ff4d4f';
      case 'Em Andamento': return '#1890ff';
      default: return '#d9d9d9';
    }
  }

  calcularMediaGeral(): number | null {
    const itensComMedia = this.boletimItens.filter(item => item.mediaFinal !== null);
    if (itensComMedia.length === 0) return null;
    const soma = itensComMedia.reduce((acc, item) => acc + (item.mediaFinal || 0), 0);
    return soma / itensComMedia.length;
  }

  temNota(avaliacaoId: number | undefined, notas: NotaObterDto[]): boolean {
    return notas.some(n => n.avaliacaoId === avaliacaoId);
  }

  obterNota(avaliacaoId: number | undefined, notas: NotaObterDto[]): NotaObterDto | undefined {
    return notas.find(n => n.avaliacaoId === avaliacaoId);
  }
}
