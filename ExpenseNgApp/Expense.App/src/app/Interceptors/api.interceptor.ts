import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, switchMap, throwError, throwIfEmpty } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { AuthUser } from '../models/user/auth-user';
import { ServerModelValidationErrors } from '../models/server-model-validation-errors';
import { Router } from '@angular/router';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService, private route: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.auth.getToken()!;

    if(token){
      request = request.clone({
        setHeaders: {Authorization: `Bearer ${token}`}
      });
    }

    return next.handle(request).pipe(
      catchError((err) => {
        if(err instanceof HttpErrorResponse){
          if(err.status === 401) {

            // Unauthorized error
            return this.handleUnauthorized(request, next);
          } 
          else if (err.status == 422 || err.status) {
            
            // Create a server model validation error and throw
            const validationErrors = new ServerModelValidationErrors(err);
            return throwError(() => validationErrors);
          }
        }
        return throwError(()=> new Error('Unknow error occurred'));
      })
    );
  }

  handleUnauthorized(request: HttpRequest<any>, next: HttpHandler){

    let authUser = new AuthUser();
    authUser.refreshToken = this.auth.getRefreshToken();
    authUser.token = this.auth.getToken();

    if(!authUser.token || !authUser.refreshToken) {
      return next.handle(request);
    }

    return this.auth.renewToken(authUser)
    .pipe(
      switchMap((data:AuthUser) => {

        this.auth.setRefreshToken(data.refreshToken);
        this.auth.setToken(data.token);

        request = request.clone({
          setHeaders: {Authorization: `Bearer ${data.token}`}
        });

        return next.handle(request);
      }),
      catchError((err) => {
        this.route.navigate(['login']);
        return throwError(()=> new Error('Unknow error occurred'));
      })
    );
  }
}
