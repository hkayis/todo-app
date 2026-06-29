import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService } from './todo';
import { Todo } from './todo.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class AppComponent implements OnInit {
  todos: Todo[] = [];

  yeniTitle: string = '';
  yeniDescription: string = '';

  // EDIT için: hangi görev düzenleniyor (id) ve düzenlenen değerler
  duzenlenenId: string | null = null;
  duzenTitle: string = '';
  duzenDescription: string = '';

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.todolariYukle();
  }

  todolariYukle(): void {
    this.todoService.getTodos().subscribe(data => {
      this.todos = data;
    });
  }

  todoEkle(): void {
    if (!this.yeniTitle.trim()) {
      return;
    }

    const yeniTodo = {
      title: this.yeniTitle,
      description: this.yeniDescription,
      isCompleted: false
    };

    this.todoService.createTodo(yeniTodo).subscribe((sonuc) => {
      this.yeniTitle = '';
      this.yeniDescription = '';
      this.todolariYukle();
      console.log(sonuc.id);
      console.log(sonuc.title);
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

  // ---- EDIT METOTLARI ----

  // Düzenleme moduna gir: mevcut değerleri düzen alanlarına kopyala
  duzenlemeBaslat(todo: Todo): void {
    this.duzenlenenId = todo.id;
    this.duzenTitle = todo.title;
    this.duzenDescription = todo.description || '';
  }

  // Düzenlemeyi kaydet
  duzenlemeKaydet(todo: Todo): void {
    if (!this.duzenTitle.trim()) {
      return;
    }

    const guncel = {
      title: this.duzenTitle,
      description: this.duzenDescription,
      isCompleted: todo.isCompleted   // tamamlanma durumu aynı kalsın
    };

    this.todoService.updateTodo(todo.id, guncel).subscribe(() => {
      this.duzenlenenId = null;   // düzenleme modundan çık
      this.todolariYukle();
    });
  }

  // Düzenlemeden vazgeç
  duzenlemeIptal(): void {
    this.duzenlenenId = null;
  }
}