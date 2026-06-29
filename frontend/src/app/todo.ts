import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from './todo.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  // route artık /api/todo (tekil) — PORTU KENDİNE GÖRE AYARLA
  private apiUrl = 'http://localhost:5132/api/todo';

  constructor(private http: HttpClient) { }

  // GET /api/todo — hepsini getir
  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  }

  // GET /api/todo/{id} — tek görev
  getTodoById(id: string): Observable<Todo> {
    return this.http.get<Todo>(`${this.apiUrl}/${id}`);
  }

  // POST /api/todo — yeni görev ekle
  createTodo(todo: Partial<Todo>): Observable<Todo> {
    return this.http.post<Todo>(this.apiUrl, todo);
  }

  // PUT /api/todo/{id} — güncelle
  updateTodo(id: string, todo: Partial<Todo>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, todo);
  }

  // DELETE /api/todo/{id} — sil
  deleteTodo(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}