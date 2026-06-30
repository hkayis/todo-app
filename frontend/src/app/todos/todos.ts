import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TodoService } from '../todo';
import { AuthService } from '../auth';
import { Todo } from '../todo.model';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todos.html',
  styleUrl: './todos.scss'
})
export class TodosComponent implements OnInit {
  todos: Todo[] = [];
  yeniTitle: string = '';
  yeniDescription: string = '';
  duzenlenenId: string | null = null;
  duzenTitle: string = '';
  duzenDescription: string = '';

  constructor(
    private todoService: TodoService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.todolariYukle();
  }

  todolariYukle(): void {
    this.todoService.getTodos().subscribe(data => {
      this.todos = data;
    });
  }

  todoEkle(): void {
    if (!this.yeniTitle.trim()) return;
    const yeniTodo = {
      title: this.yeniTitle,
      description: this.yeniDescription,
      isCompleted: false
    };
    this.todoService.createTodo(yeniTodo).subscribe(() => {
      this.yeniTitle = '';
      this.yeniDescription = '';
      this.todolariYukle();
    });
  }

  durumDegistir(todo: Todo): void {
    const guncel = {
      title: todo.title,
      description: todo.description,
      isCompleted: !todo.isCompleted
    };
    this.todoService.updateTodo(todo.id, guncel).subscribe(() => {
      this.todolariYukle();
    });
  }

  todoSil(id: string): void {
    this.todoService.deleteTodo(id).subscribe(() => {
      this.todolariYukle();
    });
  }

  duzenlemeBaslat(todo: Todo): void {
    this.duzenlenenId = todo.id;
    this.duzenTitle = todo.title;
    this.duzenDescription = todo.description || '';
  }

  duzenlemeKaydet(todo: Todo): void {
    if (!this.duzenTitle.trim()) return;
    const guncel = {
      title: this.duzenTitle,
      description: this.duzenDescription,
      isCompleted: todo.isCompleted
    };
    this.todoService.updateTodo(todo.id, guncel).subscribe(() => {
      this.duzenlenenId = null;
      this.todolariYukle();
    });
  }

  duzenlemeIptal(): void {
    this.duzenlenenId = null;
  }

  // YENİ: çıkış yap
  cikisYap(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}