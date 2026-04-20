NAMA: TIA RAISYA AKNI

NIM : 242410102015 

KELAS : PAA (A)

-----------------------------------------------------------------------------------------------------------------------------
1. Deskripsi Project dan Domain
   
  MovieApp adalah aplikasi berbasis API yang digunakan untuk mengelola data film, genre, dan review. Domain yang dipilih adalah film karena mudah dipahami, memiliki relasi antar tabel yang jelas, serta cocok untuk  implementasi CRUD.

3. Teknologi yang Digunakan
   
Bahasa: C#
Framework: ASP.NET Core Web API
Database: PostgreSQL
Tools: Visual Studio, pgAdmin, Postman

5. Langkah Instalasi dan Cara Menjalankan Project
   
Clone repository:
```bash
git clone (https://github.com/tiaraisyaaa/MovieAppAPI.git)
```

Buka project di Visual Studio.

Pastikan connection string di appsettings.json sesuai dengan konfigurasi PostgreSQL lokal.

Jalankan project

API akan berjalan di http://localhost:5000 (atau port sesuai konfigurasi).

7. Cara Import Database

Buka pgAdmin atau terminal PostgreSQL.
Jalankan script database.sql yang ada di repository.
Pastikan tabel genres, movies, dan reviews sudah terbentuk dengan data sample.

5. Daftar endpoint lengkap dalam format tabel (method, URL, keterangan)

| Method | URL              | Keterangan              |
|--------|------------------|-------------------------|
| GET    | /api/movies      | Ambil semua film        |
| GET    | /api/movies/{id} | Ambil  film brdasarkan id      |
| POST   | /api/movies      | Tambah film baru        |
| PUT    | /api/movies/{id} | Update data film        |
| DELETE | /api/movies/{id} | Hapus film              |

| Method | URL              | Keterangan              |
|--------|------------------|-------------------------|
| GET    | /api/genres      | Ambil semua genre       |
| GET    | /api/genres/{id} | Ambil  genre  berdasarkan id    |
| POST   | /api/genres      | Tambah genre baru       |
| PUT    | /api/genres | Update data genre       |
| DELETE | /api/genres/{id}| Hapus genres              |

| Method | URL               | Keterangan              |
|--------|-------------------|-------------------------|
| GET    | /api/reviews      | Ambil semua review      |
| GET    | /api/reviews/{id} | Ambil  review berdasarkan id    |
| POST   | /api/reviews      | Tambah review baru      |
| PUT    | /api/reviews | Update data review      |
| DELETE | /api/reviews/{id} | Hapus review        

6. Link Youtube
  ```bash
https://www.youtube.com/watch?v=h13u4UZ_IJY
```

   
   
