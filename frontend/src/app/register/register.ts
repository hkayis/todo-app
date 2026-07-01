import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class RegisterComponent {
  username: string = '';
  password: string = '';
  hataMesaji: string = '';
  basariMesaji: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  kayitOl(): void {
    this.hataMesaji = '';
    this.basariMesaji = '';

    this.authService.register(this.username, this.password).subscribe({
      next: () => {
        this.basariMesaji = 'Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1500);
      },
      error: (err) => {
        this.hataMesaji = err.error?.message || 'Kayıt başarısız şifre en az 6 karakter olmalıdır';   // ← düzeltildi
      }
    });
  }
}