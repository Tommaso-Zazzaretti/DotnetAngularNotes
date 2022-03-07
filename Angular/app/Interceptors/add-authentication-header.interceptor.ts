import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthenticationService } from './../Services/authentication.service';
@Injectable()
export class AddAuthenticationHeaderInterceptor implements HttpInterceptor {

    constructor(private AuthService: AuthenticationService) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const TOKEN:string|null = this.AuthService.getToken();
        if(TOKEN!=null){
            request = request.clone({
                setHeaders: { Authorization: "Bearer " + TOKEN }
            });
        }
        return next.handle(request);
    }
}
