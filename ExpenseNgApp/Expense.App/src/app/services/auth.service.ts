import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUser, } from '../models/user/create-user';
import { AuthUser } from '../models/user/auth-user';
import { LoginUser } from '../models/user/login-user';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  controllerRoute: string = 'auth';
  token: string = `token`;
  refreshToken: string = `refreshToken`;
  roles: string = `roles`;

  private jwt: any;

  constructor(private http: HttpClient, private route: Router) {
    this.jwt = this.decodedJwt();
   }

   register(user: CreateUser) : Observable<any> {
    return this.http.post(`${this.controllerRoute}/register`, user);
   }

   login(user: LoginUser) : Observable<AuthUser>{
    return this.http.post<AuthUser>(`${this.controllerRoute}/login`, user);
   }

   renewToken(authUser: AuthUser) : Observable<AuthUser>{
    return this.http.post<AuthUser>(`${this.controllerRoute}/refreshtoken`, authUser);
   }

   isLoggedIn(): boolean {
    return !!localStorage.getItem(this.token);
   }

   signOut(){
    localStorage.clear();
   }
   
   getRefreshToken() : string{
    return localStorage.getItem(this.refreshToken)!;
   }

   setRefreshToken(token: string){
    localStorage.setItem(this.refreshToken, token);
   }

   getToken(): string {
    return localStorage.getItem(this.token)!;
   }

   setToken(token: string){
    localStorage.setItem(this.token, token);
   }

   decodedJwt() : any {
    const jwt = new JwtHelperService();
    const token = this.getToken();

    return jwt.decodeToken(token);
   }

   getFirstLastNameFromJwt() : any {
    if(this.jwt){
      return this.jwt.lastFirstName;
    }
   }

   getRoles() : any {
    if(this.jwt) {
      return this.jwt.roles;
    }
   }
}
