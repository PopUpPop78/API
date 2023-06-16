import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'dashboard',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent {

  lastFirstName: string = '';
  isAdmin: boolean = false;

  constructor(private route: Router, 
    private auth: AuthService,
    private userStore: UserStoreService,
    private activeRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.userStore.getLastFirstName().subscribe({
      next: res => {
        let lastFirstName = this.auth.getFirstLastNameFromJwt();
        this.lastFirstName = res || lastFirstName;
      }
    });

    this.userStore.getRoles().subscribe({
      next: res => {
        let roles = this.auth.getRoles() as string[];
        this.isAdmin = (res as string[])?.includes('Admin') || roles?.includes('Admin');
      }
    })
  }

  signOut() {
    this.auth.signOut();
    this.userStore.clearRoles();
    this.route.navigate(['login']);
  }

  manageCurrencies() {
    this.route.navigate(['currencies'], {relativeTo: this.activeRoute});
  }

  manageCategories() {
    this.route.navigate(['categories'], {relativeTo: this.activeRoute});  
  }

  manageUsers() {
    this.route.navigate(['users'], {relativeTo: this.activeRoute}); 
  }

}
