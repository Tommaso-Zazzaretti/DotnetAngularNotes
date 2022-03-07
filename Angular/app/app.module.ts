//Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
//Components
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { LoginFormComponent } from './Components/login-form/login-form.component';
import { RegisterFormComponent } from './Components/register-form/register-form.component';
import { ReservedPageComponent } from './Components/reserved-page/reserved-page.component';
//HTTP_INTERCEPTORS
import { AddAuthenticationHeaderInterceptor } from './Interceptors/add-authentication-header.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginFormComponent,
    RegisterFormComponent,
    ReservedPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [ {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAuthenticationHeaderInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
