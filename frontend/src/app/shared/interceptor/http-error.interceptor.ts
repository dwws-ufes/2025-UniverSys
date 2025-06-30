import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {NzModalService} from 'ng-zorro-antd/modal';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {Observable, throwError, timer} from 'rxjs';
import {catchError, mergeMap, retryWhen} from 'rxjs/operators';
import {ApiErrorsTableModalContentComponent} from '../components/api-errors-table-modal-content/api-errors-table-modal-content.component';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private modal: NzModalService,
    private notification: NzNotificationService
  ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retryWhen(this.genericRetryStrategy()),
      catchError(response => {
        switch (response.status) {
          case 400:
            return this.Tratar400(response);
          case 401:
            return this.Tratar401(response);
          case 403:
            return this.Tratar403(response);
          case 500:
            return this.Tratar500(response);
          default:
            return this.Tratar500(response);
        }
      })
    );
  }

  private async Tratar400(response: any): Promise<any> {
    return new Promise(async (resolve, reject) => {
      const errors: string[] = [];

      if (
        response instanceof HttpErrorResponse &&
        response.error instanceof Blob &&
        response.error.type?.includes('application/problem+json')
      ) {
        try {
          const errorResponse: any = await new Promise((resolve, reject) => {
            let reader = new FileReader();
            reader.onload = (e: Event) => {
              resolve(JSON.parse((<any>e.target).result));
            };
            reader.onerror = reject;
            reader.readAsText(response.error);
          });

          for (let parentKey in errorResponse.errors) {
            for (let childKey in errorResponse.errors[parentKey]) {
              errors.push(errorResponse.errors[parentKey][childKey]);
            }
          }
        } catch (e) {}
      }

      if (errors.length) {
        if (errors.length == 1) {
          this.modal.error({
            nzTitle: 'Ooops...',
            nzContent: errors.join('<br/><br/>'),
            nzClassName: 'nz-modal-content-scrollable',
          });
        } else {
          const modal = this.modal.create({
            nzContent: ApiErrorsTableModalContentComponent,
            nzData: {errors},
            nzBodyStyle: {padding: '0'},
            nzClassName: 'nz-modal-content-scrollable',
            nzCentered: true,
            nzMaskClosable: false,
            nzFooter: [
              {
                label: 'OK',
                type: 'primary',
                onClick: () => modal.destroy(),
              },
            ],
          });
        }
      } else {
        this.notification.error(
          'Ooops...',
          'Ocorreu um erro na sua requisição. Atualize a página e tente novamente, caso o problema persista, tente novamente mais tarde ou entre em contato com o suporte.',
          {
            nzDuration: 10000,
          }
        );
      }

      reject(response);
    });
  }

  private async Tratar401(response: any): Promise<any> {
    this.router.navigate(['authentication/login'], {queryParams: {returnUrl: this.router.url}});
    return new Promise(async (resolve, reject) => {
      // this.notification.error(
      //   'Ooops...',
      //   'Você precisa acessar o sistema com seu login e senha para executar essa operação.',
      //   {
      //     nzDuration: 10000,
      //   }
      // );

      reject(response);
    });
  }

  private async Tratar403(response: any): Promise<any> {
    return new Promise(async (resolve, reject) => {
      this.notification.error('Ooops...', 'Você não possui permissão para executar essa operação.', {
        nzDuration: 10000,
      });

      reject(response);
    });
  }

  private async Tratar500(response: any): Promise<any> {
    return new Promise(async (resolve, reject) => {
      this.notification.error('Ooops...', 'Ocorreu um erro.', {
        nzDuration: 10000,
      });

      reject(response);
    });
  }

  genericRetryStrategy =
    ({
      maxRetryAttempts = 0,
      scalingDuration = 950,
      excludedStatusCodes = [400, 403, 404, 588],
    }: {
      maxRetryAttempts?: number;
      scalingDuration?: number;
      excludedStatusCodes?: number[];
    } = {}) =>
    (attempts: Observable<any>) => {
      return attempts.pipe(
        mergeMap((error, i) => {
          const retryAttempt = i + 1;
          if (retryAttempt > maxRetryAttempts || excludedStatusCodes.find(e => e === error.status)) {
            return throwError(error);
          }
          return timer(retryAttempt * scalingDuration);
        })
      );
    };
}
