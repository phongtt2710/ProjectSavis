import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Route, Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  constructor(private service: SharedService, private router: Router) {}

  Username: string = '';
  Pass: string = '';
  Rol: string = 'user';

  ngOnInit(): void {}

  addAccount(): void {
    const newUser = {
      Username: this.Username,
      Pass: this.Pass,
      Rol: this.Rol,
    };

    this.service.addAccount(newUser).subscribe(
      (response: any) => {
        console.log('Đăng ký thành công');
        alert('Đăng ký thành công');
        this.router.navigate(['/login']);
      },
      (error: any) => {
        console.log('Đăng ký thất bại');
        alert('Đăng ký thất bại');
      }
    );
  }
}
