## 🛒 MultiShop - Microservice Based E-Commerce Backend

MultiShop, .NET teknolojileriyle geliştirilmiş mikro servis mimarisine sahip bir e-ticaret uygulamasıdır.  
Ocelot API Gateway ve Identity Server kullanılarak kimlik doğrulama ve servisler arası geçiş yönetilirken, her modül bağımsız olarak yapılandırılmıştır.  
Proje, servislerin **loosely coupled (gevşek bağlı)** olması prensibiyle geliştirilmiştir.

---

## 🚀 Temel Özellikler

- Microservice tabanlı yapı
- Ocelot ile API Gateway yönetimi
- Identity Server ile kimlik doğrulama ve yetkilendirme
- Entity Framework Core ile veritabanı işlemleri
- AutoMapper, FluentValidation, Swagger entegrasyonu
- SOLID prensipleriyle kodlama

---

## ⚙️ Kullanılan Teknolojiler

- ASP.NET Core Web API  
- Ocelot API Gateway  
- Identity Server 4  
- Entity Framework Core  
- SQL Server  
- AutoMapper  
- FluentValidation  
- Swagger  
- Postman  

## 📁 Proje Yapısı

```text
MultiShop
│
├── Services
│   ├── MultiShop.Catalog     → Ürün listeleme servisi
│   ├── MultiShop.Product     → Ürün işlemleri
│   ├── MultiShop.Category    → Kategori işlemleri
│   └── MultiShop.Detail      → Ürün detay servisi
│
├── Gateway
│   └── MultiShop.Gateway     → Ocelot API Gateway yapılandırması
│
├── Identity
    └── MultiShop.Identity    → Identity Server kimlik doğrulama ve yetkilendirme

---

## 👨‍💻 Geliştirici

**Cesur Alphan Ellik**  
[LinkedIn](https://www.linkedin.com/in/cesur-alphan-ellik-b0056a240/)  
[GitHub](https://github.com/Melanocetuss)

---

