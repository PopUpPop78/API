import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { environment } from 'src/environments/environment.development';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { UrlInterceptor } from './Interceptors/url.interceptor';
import { ApiInterceptor } from './Interceptors/api.interceptor';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { RegisterComponent } from './components/register/register.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExpenseComponent } from './components/expense/expense.component';
import { ReportComponent } from './components/report/report.component';
import { DashboardMenuComponent } from './components/dashboard-menu/dashboard-menu.component';
import { CurrenciesComponent } from './components/currencies/currencies.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { UsersComponent } from './components/users/users.component';
import { AddCategoryComponent } from './components/categories/add-category/add-category.component';
import { UpdateCategoryComponent } from './components/categories/update-category/update-category.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashBoardComponent,
    RegisterComponent,
    ExpenseComponent,
    ReportComponent,
    DashboardMenuComponent,
    CurrenciesComponent,
    CategoriesComponent,
    UsersComponent,
    AddCategoryComponent,
    UpdateCategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2500,
      positionClass: 'toast-top-right',
      easeTime: 300,
      tapToDismiss: true,
      preventDuplicates: true,
      progressAnimation: 'decreasing',
      progressBar: true
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: UrlInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    { provide: "BASE_API_URL", useValue: environment.baseApiUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
