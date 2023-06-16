import { Injectable } from '@angular/core';
import { Category } from '../models/category/category';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  route: string = 'category';

  constructor(private http: HttpClient) { }

  add(cat: Category) : Observable<Category> {
    return this.http.post<Category>(this.route, cat);
  }

  getAll() : Observable<Category[]> {
    return this.http.get<Category[]>(this.route);
  }

  get(id: number) : Observable<Category> {
    return this.http.get<Category>(`${this.route}/${id}`);
  }

  update(cat: Category) : Observable<Category> {
    return this.http.put<Category>(`${this.route}/${cat.id}`, cat);
  }

  delete(cat: Category) : Observable<any> {
    return this.http.delete<Category>(`${this.route}/${cat.id}`);
  }
}
