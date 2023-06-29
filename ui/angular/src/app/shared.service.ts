import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  [x: string]: any;
  readonly APIUrl = 'https://localhost:44382/api';
  readonly PhotoUrl = 'http://localhost:44382/Photos';
  constructor(private http: HttpClient, private router: Router) {}

  getDepList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDepartment(val: any) {
    return this.http.post(this.APIUrl + '/Department', val);
  }

  updateDepartment(val: any) {
    return this.http.put(this.APIUrl + '/Department', val);
  }

  deleteDepartment(val: any) {
    return this.http.delete(this.APIUrl + '/Department/' + val);
  }

  getEmpList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/Employee');
  }

  addEmployee(val: any) {
    return this.http.post(this.APIUrl + '/Employee', val);
  }

  updateEmployee(val: any) {
    return this.http.put(this.APIUrl + '/Employee', val);
  }

  deleteEmployee(val: any) {
    return this.http.delete(this.APIUrl + '/Employee/' + val);
  }

  UploatPhoto(val: any) {
    return this.http.post(this.APIUrl + 'Employee/SaveFile', val);
  }

  getAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(
      this.APIUrl + '/Employee/GetAllDepartmentNames'
    );
  }

  loggedIn() {
    console.log(!!localStorage.getItem('success'));
    return !!localStorage.getItem('success');
  }

  isAdmin(): boolean {
    const role = localStorage.getItem('role');
    return role === 'admin';
  }

  login(Username: string, Pass: string): Observable<any> {
    const loginData = {
      Username: Username,
      Pass: Pass,
    };

    return this.http.post<any>(this.APIUrl + '/Account/Login', loginData);
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
