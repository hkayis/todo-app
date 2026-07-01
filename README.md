# Yapılacaklar Listesi (To-Do List) Uygulaması

Kullanıcıların kayıt olup giriş yapabildiği; görev ekleyebileceği, listeleyebileceği, düzenleyebileceği, tamamlandı olarak işaretleyebileceği ve silebileceği tam kapsamlı bir To-Do List uygulaması. Backend .NET Core ile Clean Architecture, CQRS ve Repository Pattern kullanılarak; frontend ise Angular ile geliştirilmiştir. Uygulama JWT tabanlı kimlik doğrulama ile korunmaktadır.

## İçindekiler

- [Özellikler](#özellikler)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
  - [Gereksinimler](#gereksinimler)
  - [Backend Kurulumu](#backend-kurulumu)
  - [Frontend Kurulumu](#frontend-kurulumu)
- [API Endpoint'leri](#api-endpointleri)
  - [Kimlik Doğrulama (Auth)](#kimlik-doğrulama-auth)
  - [Görevler (ToDo)](#görevler-todo)
- [Mimari Hakkında](#mimari-hakkında)

## Özellikler

- Kullanıcı kaydı ve girişi (register / login)
- JWT (JSON Web Token) tabanlı kimlik doğrulama
- Şifrelerin BCrypt ile hash'lenerek güvenli saklanması
- Görevler için tam CRUD işlemleri (oluştur, listele, güncelle, sil)
- Görevleri tamamlandı olarak işaretleme
- Giriş yapmamış kullanıcıların korumalı sayfalara erişiminin engellenmesi (route guard)
- Her isteğe token'ın otomatik eklenmesi (HTTP interceptor)

## Kullanılan Teknolojiler

### Backend
- **.NET Core** — Web API
- **Entity Framework Core** — ORM (Object-Relational Mapping)
- **MSSQL Server** — Veritabanı
- **CQRS** — Command Query Responsibility Segregation deseni
- **Repository Pattern** — Veri erişim katmanı soyutlaması
- **Clean Architecture** — Katmanlı mimari (Domain / Application / Infrastructure)
- **JWT (JSON Web Token)** — Kimlik doğrulama
- **BCrypt** — Şifre hash'leme

### Frontend
- **Angular** — SPA (Single Page Application) framework
- **TypeScript** — Programlama dili
- **HTML & SCSS** — Arayüz ve stillendirme
- **Angular Router** — Sayfa yönlendirme, route guard ve HTTP interceptor

## Proje Yapısı

```
todo-app/
├── backend/                    # .NET Core Web API
│   ├── Domain/
│   │   └── Entities/           # ToDo ve User entity'leri
│   ├── Application/
│   │   ├── CommandHandlers/    # Create, Update, Delete, Register, Login işlemleri
│   │   ├── QueryHandlers/      # GetAll, GetById işlemleri
│   │   ├── Controllers/        # ToDo ve Auth controller'ları
│   │   ├── Interfaces/         # Repository ve servis arayüzleri
│   │   └── DTOs/               # Veri transfer nesneleri
│   └── Infrastructure/
│       ├── Persistence/        # DbContext
│       ├── Repositories/       # IToDoRepository, IUserRepository ve implementasyonları
│       └── Services/           # TokenService (JWT üretimi)
│
└── frontend/                   # Angular uygulaması
    └── src/app/
        ├── login/              # Giriş sayfası
        ├── register/           # Kayıt sayfası
        ├── todos/              # Görev listesi sayfası
        ├── auth.ts             # Kimlik doğrulama servisi
        ├── auth-guard.ts       # Route guard
        └── auth-interceptor.ts # HTTP interceptor
```

## Kurulum ve Çalıştırma

### Gereksinimler

Projeyi çalıştırmadan önce aşağıdakilerin kurulu olması gerekir:

- [.NET SDK](https://dotnet.microsoft.com/download) (.NET 10 veya üzeri)
- [Node.js](https://nodejs.org/) (LTS sürümü) ve npm
- [Angular CLI](https://angular.dev/tools/cli) — `npm install -g @angular/cli`
- [MSSQL Server](https://www.microsoft.com/sql-server) (Express veya üzeri)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms) — (opsiyonel, veritabanını görüntülemek için)

### Backend Kurulumu

1. Backend klasörüne gidin:
   ```bash
   cd backend
   ```

2. `appsettings.json` dosyasındaki connection string'i kendi SQL Server'ınıza göre düzenleyin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=[SUNUCU-ADINIZ];Database=[VERITABANI-ADI];Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. `appsettings.json` dosyasındaki JWT ayarlarının tanımlı olduğundan emin olun. `Key` alanı en az 32 karakter olmalıdır:
   ```json
   "Jwt": {
     "Key": "[EN-AZ-32-KARAKTERLIK-GIZLI-ANAHTAR]",
     "Issuer": "TodoApi",
     "Audience": "TodoApiUsers"
   }
   ```

4. Gerekli paketleri yükleyin:
   ```bash
   dotnet restore
   ```

5. Veritabanını ve tabloları Entity Framework Migrations ile oluşturun:
   ```bash
   dotnet ef database update
   ```
   > Not: Eğer `dotnet ef` komutu tanınmıyorsa, önce şu komutu çalıştırın:
   > `dotnet tool install --global dotnet-ef`

6. Uygulamayı çalıştırın:
   ```bash
   dotnet run
   ```
   API çalışmaya başladığında, terminalde gösterilen adresi (örneğin
   "Now listening on: http://localhost:XXXX") not edin. Swagger arayüzüne
   `http://localhost:XXXX/swagger` adresinden erişebilirsiniz.

### Frontend Kurulumu

1. Frontend klasörüne gidin:
   ```bash
   cd frontend
   ```

2. Bağımlılıkları yükleyin:
   ```bash
   npm install
   ```

3. `src/app/todo.ts` ve `src/app/auth.ts` dosyalarındaki `apiUrl` değerlerinin, backend'in çalıştığı port ile eşleştiğinden emin olun:
   ```typescript
   // todo.ts
   private apiUrl = 'http://localhost:XXXX/api/todo';

   // auth.ts
   private apiUrl = 'http://localhost:XXXX/api/auth';
   ```

4. Uygulamayı çalıştırın:
   ```bash
   ng serve
   ```
   Uygulama `http://localhost:4200` adresinde açılacaktır.

> **Önemli:** Uygulamanın çalışması için hem backend (`dotnet run`) hem de frontend (`ng serve`) aynı anda çalışıyor olmalıdır.

## API Endpoint'leri

### Kimlik Doğrulama (Auth)

Kimlik doğrulama endpoint'leri `http://localhost:XXXX/api/auth` temel adresi üzerinden çalışır ve token gerektirmez.

#### Kayıt ol

```http
POST /api/auth/register
Content-Type: application/json
```

**Örnek İstek Gövdesi:**
```json
{
  "username": "hakan",
  "password": "123456"
}
```

**Yanıt:** Başarılıysa `200 OK`, kullanıcı adı zaten alınmışsa `400 Bad Request`.

#### Giriş yap

```http
POST /api/auth/login
Content-Type: application/json
```

**Örnek İstek Gövdesi:**
```json
{
  "username": "hakan",
  "password": "123456"
}
```

**Örnek Yanıt (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "hakan"
}
```

Kullanıcı adı veya şifre hatalıysa `401 Unauthorized` döner.

### Görevler (ToDo)

Görev endpoint'leri `http://localhost:XXXX/api/todo` temel adresi üzerinden çalışır ve **geçerli bir JWT token gerektirir.** Token, isteğin header'ında `Authorization: Bearer <token>` biçiminde gönderilmelidir.

#### Tüm görevleri getir

```http
GET /api/todo
Authorization: Bearer <token>
```

**Örnek Yanıt (200 OK):**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Alışveriş yap",
    "description": "Market alışverişi",
    "isCompleted": false,
    "createdAt": "2026-06-29T10:30:00"
  }
]
```

#### Belirli bir görevi getir

```http
GET /api/todo/{id}
Authorization: Bearer <token>
```

**Örnek Yanıt (200 OK):**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Alışveriş yap",
  "description": "Market alışverişi",
  "isCompleted": false,
  "createdAt": "2026-06-29T10:30:00"
}
```

Görev bulunamazsa `404 Not Found` döner.

#### Yeni görev oluştur

```http
POST /api/todo
Authorization: Bearer <token>
Content-Type: application/json
```

**Örnek İstek Gövdesi:**
```json
{
  "title": "Spor yap",
  "description": "Akşam koşusu",
  "isCompleted": false
}
```

**Örnek Yanıt (201 Created):**
```json
{
  "id": "a1b2c3d4-5717-4562-b3fc-2c963f66afa6",
  "title": "Spor yap",
  "description": "Akşam koşusu",
  "isCompleted": false,
  "createdAt": "2026-06-29T11:00:00"
}
```

#### Görevi güncelle

```http
PUT /api/todo/{id}
Authorization: Bearer <token>
Content-Type: application/json
```

**Örnek İstek Gövdesi:**
```json
{
  "title": "Spor yap (güncellendi)",
  "description": "Sabah koşusu",
  "isCompleted": true
}
```

**Yanıt:** `204 No Content` (başarılı), görev bulunamazsa `404 Not Found`.

#### Görevi sil

```http
DELETE /api/todo/{id}
Authorization: Bearer <token>
```

**Yanıt:** `204 No Content` (başarılı), görev bulunamazsa `404 Not Found`.

> Not: Token içermeyen ya da geçersiz token içeren görev istekleri `401 Unauthorized` döner.

## Mimari Hakkında

Proje, sorumlulukların ayrılması (separation of concerns) prensibine uygun olarak Clean Architecture yaklaşımıyla katmanlı bir şekilde tasarlanmıştır:

- **Domain** katmanı, uygulamanın temel entity'lerini (ToDo, User) içerir ve hiçbir dış bağımlılığı yoktur.
- **Application** katmanı, iş mantığını CQRS deseniyle yönetir. Command'ler (veri değiştiren işlemler) ve query'ler (veri okuyan işlemler) ayrı handler'larda ele alınır. Repository ve servis arayüzleri (interface) bu katmanda tanımlanır.
- **Infrastructure** katmanı, veritabanı erişimini Repository Pattern ile, JWT üretimini ise TokenService ile sağlar. Bu katman, Application katmanında tanımlanan arayüzleri uygular.

Bağımlılıklar dıştan içe doğru akacak şekilde tasarlanmıştır; yani Application katmanı Infrastructure'a değil, kendi tanımladığı arayüzlere bağımlıdır. Bu yapı sayesinde her katman bağımsız olarak test edilebilir ve değiştirilebilir. Örnek olarak veritabanı teknolojisi değiştirilse bile iş mantığı katmanı etkilenmez.

Frontend tarafında da benzer bir sorumluluk ayrımı vardır: component'ler görünümü yönetirken, service'ler API iletişimini üstlenir. Kimlik doğrulama için route guard (giriş yapmamış kullanıcının korumalı sayfalara erişimini engeller) ve HTTP interceptor (her isteğe token'ı otomatik ekler) mekanizmaları kullanılmıştır.

---

## Geliştirici

**Hakan Kayış**
İhsan Doğramacı Bilkent Üniversitesi - Bilişim Sistemleri ve Teknolojileri
https://www.linkedin.com/in/hakan-kayi%C5%9F-a1b772351/
