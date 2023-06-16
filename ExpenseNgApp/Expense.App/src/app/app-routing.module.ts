import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { authGuard } from './guards/auth.guard';
import { RegisterComponent } from './components/register/register.component';
import { ExpenseComponent } from './components/expense/expense.component';
import { ReportComponent } from './components/report/report.component';
import { DashboardMenuComponent } from './components/dashboard-menu/dashboard-menu.component';
import { CurrenciesComponent } from './components/currencies/currencies.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { UsersComponent } from './components/users/users.component';
import { AddCategoryComponent } from './components/categories/add-category/add-category.component';
import { UpdateCategoryComponent } from './components/categories/update-category/update-category.component';

const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashBoardComponent, canActivate: [authGuard], canActivateChild: [authGuard], 
  children: [
    { path: '', component: DashboardMenuComponent, pathMatch: 'full'},
    { path: 'expense', component: ExpenseComponent},
    { path: 'reports', component: ReportComponent},
    { path: 'currencies', component: CurrenciesComponent},
    { path: 'categories', component: CategoriesComponent},
    { path: 'users', component: UsersComponent},
    { path: 'categories/add-category', component: AddCategoryComponent},
    { path: 'categories/update-category/:id', component: UpdateCategoryComponent}
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
