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
        // Kayıt başarılı → mesaj göster, login'e yönlendir
        this.basariMesaji = 'Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1500);
      },
      error: (err) => {
        // Backend'den gelen hata mesajını göster
        this.hataMesaji = err.error || 'Kayıt başarısız';
      }
    });
  }
}