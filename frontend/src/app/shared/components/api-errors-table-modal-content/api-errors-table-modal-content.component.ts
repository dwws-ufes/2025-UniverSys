import { Component, Inject, Input, OnInit } from '@angular/core';
import { NZ_MODAL_DATA } from 'ng-zorro-antd/modal';

@Component({
    templateUrl: './api-errors-table-modal-content.component.html',
    standalone: false
})
export class ApiErrorsTableModalContentComponent implements OnInit {
  errors: string[];
  @Input() modalTitle: string = 'Ooops...';

  constructor(@Inject(NZ_MODAL_DATA) public data: ModalErrorData) {}

  ngOnInit(): void {
    this.errors = this.data.errors;
  }
}

interface ModalErrorData
{
  errors: string[];
}
