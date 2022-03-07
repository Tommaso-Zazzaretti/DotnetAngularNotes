import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree,Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './../Services/authentication.service';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
    constructor(private AuthService: AuthenticationService, private router: Router){

    }

    canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (!this.AuthService.isLogged) {
            window.alert("Access not allowed!");
            this.router.navigate(['Login'])
        }
        return true;
    }
}
