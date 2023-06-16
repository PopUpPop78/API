import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {

  private lastFirstName$ = new BehaviorSubject<string>('');
  private role$ = new BehaviorSubject<string[]>([]);

  constructor() { }

  public getRoles() : Observable<string[]> {
    return this.role$.asObservable();
  }  

  public getLastFirstName() : Observable<string> {
    return this.lastFirstName$.asObservable();
  }

  public setRole(roles: string[]) : void {
    this.role$.next(roles);
  }

  public setLastFirstName(lastFirstName: string) : void {
    this.lastFirstName$.next(lastFirstName);
  }

  public clearRoles() {
    this.role$ = new BehaviorSubject<string[]>([]);
  }

}
