# Yapılacaklar Listesi (To-Do List) Uygulaması

Kullanıcıların görev ekleyebileceği, listeleyebileceği, düzenleyebileceği, tamamlandı olarak işaretleyebileceği ve silebileceği tam kapsamlı bir To-Do List uygulaması. Backend .NET Core ile CQRS ve Repository Pattern kullanılarak, frontend ise Angular ile geliştirilmiştir.

## İçindekiler

- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
  - [Gereksinimler](#gereksinimler)
  - [Backend Kurulumu](#backend-kurulumu)
  - [Frontend Kurulumu](#frontend-kurulumu)
- [API Endpoint'leri](#api-endpointleri)
- [Mimari Hakkında](#mimari-hakkında)

## Kullanılan Teknolojiler

### Backend
- **.NET Core** — Web API
- **Entity Framework Core** — ORM (Object-Relational Mapping)
- **MSSQL Server** — Veritabanı
- **CQRS** — Command Query Responsibility Segregation deseni
- **Repository Pattern** — Veri erişim katmanı soyutlaması

### Frontend
- **Angular** — SPA (Single Page Application) framework
- **TypeScript** — Programlama dili
- **HTML & SCSS** — Arayüz ve stillendirme

## Proje Yapısı

```
todo-app/
├── backend/                    # .NET Core Web API
│   ├── Domain/
│   │   └── Entities/           # ToDo entity'si
│   ├── Application/
│   │   ├── CommandHandlers/    # Create, Update, Delete işlemleri
│   │   ├── QueryHandlers/      # GetAll, GetById işlemleri
│   │   ├── Controllers/        # API controller'ı
│   │   └── DTOs/               # Veri transfer nesneleri
│   └── Infrastructure/
│       ├── Persistence/        # DbContext
│       └── Repositories/       # IToDoRepository ve implementasyonu
│
└── frontend/                   # Angular uygulaması
    └── src/app/                # Component, service, model
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

3. Gerekli paketleri yükleyin:
   ```bash
   dotnet restore
   ```

4. Veritabanını ve tabloları Entity Framework Migrations ile oluşturun:
   ```bash
   dotnet ef database update
   ```
   > Not: Eğer `dotnet ef` komutu tanınmıyorsa, önce şu komutu çalıştırın:
   > `dotnet tool install --global dotnet-ef`

5. Uygulamayı çalıştırın:
   ```bash
   dotnet run
   ```
   API varsayılan olarak `http://localhost:5132` adresinde çalışacaktır.
   Swagger arayüzüne `http://localhost:5132/swagger` adresinden erişebilirsiniz.

### Frontend Kurulumu

1. Frontend klasörüne gidin:
   ```bash
   cd frontend
   ```

2. Bağımlılıkları yükleyin:
   ```bash
   npm install
   ```

3. `src/app/todo.ts` dosyasındaki `apiUrl` değerinin backend portunuzla eşleştiğinden emin olun:
   ```typescript
   private apiUrl = 'http://localhost:5132/api/todo';
   ```

4. Uygulamayı çalıştırın:
   ```bash
   ng serve
   ```
   Uygulama `http://localhost:4200` adresinde açılacaktır.

> **Önemli:** Uygulamanın çalışması için hem backend (`dotnet run`) hem de frontend (`ng serve`) aynı anda çalışıyor olmalıdır.

## API Endpoint'leri

Tüm endpoint'ler `http://localhost:5132/api/todo` temel adresi üzerinden çalışır.

### Tüm görevleri getir

```http
GET /api/todo
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

### Belirli bir görevi getir

```http
GET /api/todo/{id}
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

### Yeni görev oluştur

```http
POST /api/todo
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

### Görevi güncelle

```http
PUT /api/todo/{id}
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

### Görevi sil

```http
DELETE /api/todo/{id}
```

**Yanıt:** `204 No Content` (başarılı), görev bulunamazsa `404 Not Found`.

## Mimari Hakkında

Proje, sorumlulukların ayrılması prensibine uygun olarak katmanlı bir mimariyle tasarlanmıştır:

- **Domain** katmanı, uygulamanın temel entitylerini içerir ve hiçbir dış bağımlılığı yoktur.
- **Application** katmanı, iş mantığını CQRS deseniyle yönetir. Commandler (veri değiştiren işlemler) ve queryler (veri okuyan işlemler) ayrı handler'larda ele alınır.
- **Infrastructure** katmanı, veritabanı erişimini Repository Pattern ile soyutlar. `IToDoRepository` arayüzü, veri erişim detaylarını iş mantığından gizler.

Bu yapı sayesinde her katman bağımsız olarak test edilebilir ve değiştirilebilir. Örnek olarak veritabanı teknolojisi değiştirilse bile iş mantığı katmanı etkilenmez.

---

## Geliştirici

**Hakan Kayiş** -
İhsan Doğramacı Bilkent Üniversitesi - Bilişim Sistemleri ve Teknolojileri
https://www.linkedin.com/in/hakan-kayi%C5%9F-a1b772351/
