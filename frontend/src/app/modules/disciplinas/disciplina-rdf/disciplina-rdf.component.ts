import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { DisciplinasClient } from 'web-api-client';

@Component({
  selector: 'disciplina-rdf',
  templateUrl: './disciplina-rdf.component.html',
  styleUrls: ['./disciplina-rdf.component.scss'],
  standalone: false
})
export class DisciplinaRdfComponent implements OnInit, OnDestroy {
  private readonly disciplinasClient = inject(DisciplinasClient);
  private readonly sanitizer = inject(DomSanitizer);

  loading = false;
  contentText: string | null = null;
  private objectUrl: string | null = null;
  error: string | null = null;

  ngOnInit(): void {
    this.loading = true;
    this.disciplinasClient.obterTodasRdfXml().subscribe(res => {
      const blob = res.data as Blob;
      blob.text().then(text => {
        this.contentText = text;
        this.loading = false;
      });
    });
  }

  ngOnDestroy(): void {
    if (this.objectUrl) URL.revokeObjectURL(this.objectUrl);
  }
}


