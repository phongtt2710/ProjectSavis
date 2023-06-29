import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-login1',
  templateUrl: './login1.component.html',
  styleUrls: ['./login1.component.css'],
})
export class Login1Component implements OnInit {
  constructor(private service: SharedService) {}
  EmployeeList: any = [];

  ModalTitle: string = '';
  ActivateAddEditEmpComp: boolean = false;
  emp: any;
  ngOnInit(): void {
    this.refreshEmpList();
  }

  refreshEmpList(): void {
    this.service.getEmpList().subscribe((data) => {
      this.EmployeeList = data;
    });
  }
}
