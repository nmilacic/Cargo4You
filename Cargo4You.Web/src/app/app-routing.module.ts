import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { PackageDimensionComponent } from './pages/package-dimension/package-dimension.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { ResultComponent } from './pages/result/result.component';

const routes: Routes = [
 { path: 'package-dimension',
 component: PackageDimensionComponent,},
 { path: 'login',
 component: LoginComponent,},
 {
   path: 'registration',
   component: RegistrationComponent
 },
 {
  path: 'result',
  component: ResultComponent
}

 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
