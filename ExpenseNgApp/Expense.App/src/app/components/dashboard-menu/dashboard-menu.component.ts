import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-dashboard-menu',
  templateUrl: './dashboard-menu.component.html',
  styleUrls: ['./dashboard-menu.component.css']
})
export class DashboardMenuComponent {

  constructor(private route:Router,
    private activeRoute: ActivatedRoute){

  }

  addExpense() {
    this.route.navigate(['expense'], {relativeTo: this.activeRoute});
  }

  reports() {
    this.route.navigate(['reports'], {relativeTo: this.activeRoute});
  }
}
