import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginFormComponent } from './Components/login-form/login-form.component';
import { RegisterFormComponent } from './Components/register-form/register-form.component';
import { ReservedPageComponent } from './Components/reserved-page/reserved-page.component';

import { AuthenticationGuard } from './Guards/authentication.guard';

const routes: Routes = [
  { path: '', redirectTo: '/Login', pathMatch: 'full' },
  { path: 'Login'   , component: LoginFormComponent },
  { path: 'Register', component: RegisterFormComponent },
  { path: 'ReservedArea', component: ReservedPageComponent, canActivate: [AuthenticationGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
