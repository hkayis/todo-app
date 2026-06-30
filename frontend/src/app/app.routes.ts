import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { RegisterComponent } from './register/register';
import { TodosComponent } from './todos/todos';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'todos', component: TodosComponent },

  // Boş yol (ana sayfa) → login'e yönlendir
  { path: '', redirectTo: '/login', pathMatch: 'full' },

  // Tanımsız yol → login'e yönlendir
  { path: '**', redirectTo: '/login' }
];