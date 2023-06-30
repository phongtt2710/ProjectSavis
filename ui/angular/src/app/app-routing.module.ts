import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeeComponent } from './employee/employee.component';
import { DepartmentComponent } from './department/department.component';
import { LoginComponent } from './login/login.component';
import { Login1Component } from './login/login1/login1.component';
import { SignupComponent } from './signup/signup.component';
const routes: Routes = [
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'login1', component: Login1Component },
  { path: 'signup', component: SignupComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
