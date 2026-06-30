import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

// Backend'den dönen cevabın tipi
interface AuthResponse {
  token: string;
  username: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5132/api/auth';

  constructor(private http: HttpClient) { }

  // Kayıt ol — POST /api/auth/register
  register(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { username, password });
  }

  // Giriş yap — POST /api/auth/login
  login(username: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, { username, password })
      .pipe(
        // Cevap gelince token'ı sakla
        tap(response => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('username', response.username);
        })
      );
  }

  // Çıkış yap — saklanan token'ı sil
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
  }

  // Token'ı al (interceptor ve guard kullanacak)
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Kullanıcı giriş yapmış mı?
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  // Giriş yapan kullanıcının adı
  getUsername(): string | null {
    return localStorage.getItem('username');
  }
}