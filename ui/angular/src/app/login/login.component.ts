import { Route, Router, RouterModule } from '@angular/router';
import { Component } from '@angular/core';
import { catchError } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  Username = '';
  Pass = '';

  constructor(private sharedService: SharedService, private router: Router) {}

  async login(): Promise<void> {
    this.sharedService
      .login(this.Username, this.Pass)
      .pipe(
        catchError((error) => {
          console.log('Thất bại');
          alert('Đăng nhập thất bại');
          throw error;
        })
      )
      .subscribe((response: any) => {
        console.log('Đăng nhập thành công');
        const role = response.role;
        localStorage.setItem('role', role);
        if (role === 'admin') {
          this.loginSuccess('/admin');
          this.router.navigate(['/department']);
        } else if (role === 'user') {
          this.loginSuccess('/user');
          this.router.navigate(['/login1']);
        } else {
          console.log('Không hợp lệ');
        }
      });
  }

  private loginSuccess(route: string): void {
    localStorage.setItem('success', '1');
    this.router.navigate([route]);
  }
}
