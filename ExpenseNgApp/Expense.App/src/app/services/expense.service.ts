import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Expense } from '../models/expense/expense';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  route: string = 'expense';

  constructor(private http: HttpClient) { }

  getAll() : Observable<Expense[]> {
    return this.http.get<Expense[]>(this.route);
  }
}
