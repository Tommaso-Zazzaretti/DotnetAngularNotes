import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { Token } from './../Models/Token';
import { UserCredentials } from './../Models/UserCredentials';
import { UserProfile } from './../Models/UserProfile';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
    public isLogged:boolean=false;
    private LOGGED_USER:UserProfile|null = null;

    constructor(private HTTP: HttpClient, private router: Router) {
    }

    public Login(Credentials:UserCredentials):void {
        const URL  = 'http://localhost:57493/api/login';
        const OPTS = {
                         headers: new HttpHeaders({'Content-Type':  'application/json'}),
                         observe:      'body' as const,
                         responseType: 'json' as const
                     };
        this.HTTP.post<Token>(URL,Credentials,OPTS).subscribe({
              //Ogni result è un oggetto { "token": "...." }, guardare la classe Token
              next: (result) => {
                    const tokenString:string = (result.token!=null) ? result.token : "";
                    if(tokenString!=""){
                        localStorage.setItem("TOKEN",tokenString); //Salvo il Jwt Token in memoria
                        this.GetLoggedUser().subscribe({
                            next: (result) => {
                                this.LOGGED_USER = result;
                                this.isLogged = this.CheckLogin();
                                window.alert(this.isLogged);
                                this.router.navigate(["ReservedArea"]);
                            },
                            error: (e:HttpErrorResponse) => {
                                  let errorString = e.status.toString()+": "+e.error
                                  window.alert(errorString);
                            }
                        })
                    }
              },
              error: (e:HttpErrorResponse) => {
                    let errorString = e.status.toString()+": "+e.error
                    window.alert(errorString);
              }
        });
    }

    private GetLoggedUser():Observable<UserProfile>{
        const URL  = 'http://localhost:57493/api/user';
        const OPTS = {
                       headers: new HttpHeaders({'Content-Type':  'application/json'}),
                       observe:      'body' as const,
                       responseType: 'json' as const
                     };
        return this.HTTP.get<UserProfile>(URL,OPTS);
    }

    public getToken():string|null{
        return localStorage.getItem("TOKEN");
    }

    private CheckLogin():boolean{
        return (localStorage.getItem("TOKEN")!=null && this.LOGGED_USER!=null)
    }

    public Logout():void{
        localStorage.removeItem("TOKEN");
        this.LOGGED_USER=null;
        this.isLogged = this.CheckLogin();
        this.router.navigate(["Login"]);
    }
}
