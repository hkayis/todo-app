import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  hataMesaji: string = '';
  sifreGoster: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  girisYap(): void {
    this.hataMesaji = '';

    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        this.router.navigate(['/todos']);
      },
      error: () => {
        this.hataMesaji = 'Kullanıcı adı veya şifre hatalı';
      }
    });
  }
}