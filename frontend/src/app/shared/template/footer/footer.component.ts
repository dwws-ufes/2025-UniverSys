import { Component, Inject } from '@angular/core';
import { API_BASE_URL } from 'web-api-client';
import packageInfo from '../../../../../package.json';

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    standalone: false
})
export class FooterComponent {
    public version: string = packageInfo.version;

    constructor(@Inject(API_BASE_URL) baseUrl?: string) {
    }
}
