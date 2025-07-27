## 🛒 MultiShop - Microservice Based E-Commerce Backend

MultiShop, .NET teknolojileriyle geliştirilmiş mikro servis mimarisine sahip bir e-ticaret uygulamasıdır.  
Ocelot API Gateway ve Identity Server kullanılarak kimlik doğrulama ve servisler arası geçiş yönetilirken, her modül bağımsız olarak yapılandırılmıştır.  
Proje, servislerin **loosely coupled (gevşek bağlı)** olması prensibiyle geliştirilmiştir.

---

## 🚀 Temel Özellikler

- Microservice tabanlı yapı
- Ocelot ile API Gateway yönetimi
- Identity Server ile kimlik doğrulama ve yetkilendirme
- Her servis bağımsız katmanlı mimariye sahip (Controller, Business, DataAccess)
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

---

## 📁 Proje Yapısı
ChatGPT:
MultiShop - Microservice Based E-Commerce Backend

🛒 MultiShop - Microservice Based E-Commerce Backend
MultiShop, .NET teknolojileriyle geliştirilmiş mikro servis mimarisine sahip bir e-ticaret uygulamasıdır. Ocelot API Gateway ve Identity Server kullanılarak kimlik doğrulama ve servisler arası geçiş yönetilirken, her modül bağımsız olarak yapılandırılmıştır. Proje, servislerin loosely coupled olması prensibiyle geliştirilmiştir.

🚀 Temel Özellikler
Microservice tabanlı yapı

Ocelot ile API Gateway yönetimi

Identity Server ile kimlik doğrulama ve yetkilendirme

Her servis bağımsız katmanlı mimariye sahip (Controller, Business, DataAccess)

Entity Framework Core ile veritabanı işlemleri

AutoMapper, FluentValidation, Swagger entegrasyonu

SOLID prensipleriyle kodlama

⚙️ Kullanılan Teknolojiler
ASP.NET Core Web API

Ocelot API Gateway

Identity Server 4

Entity Framework Core

SQL Server

AutoMapper

FluentValidation

Swagger

Postman

📁 Proje Yapısı
MultiShop
│
├── Services
│   ├── MultiShop.Catalog      → Ürün listeleme servisi
│   ├── MultiShop.Product      → Ürün işlemleri
│   ├── MultiShop.Category     → Kategori işlemleri
│   └── MultiShop.Detail       → Ürün detay servisi
│
├── Gateway
│   └── MultiShop.Gateway      → Ocelot API Gateway yapılandırması
│
├── Identity
│   └── MultiShop.Identity     → Identity Server kimlik doğrulama ve yetkilendirme
│
└── MultiShop.WebAPI           → Ortak API endpoint sunumu
🛠️ Kurulum
Bu repoyu klonlayın:

git clone https://github.com/Melanocetuss/MultiShop.git
Visual Studio ile açın.

Gerekli appsettings.json, ocelot.json ve Identity Server ayarlarını yapılandırın.

Servislerin her biri için veritabanı bağlantılarını ayarlayın.

Paket Yöneticisi Konsolu üzerinden veritabanlarını oluşturun:

Update-Database
Identity Server ve Gateway projelerini başlatarak kimlik doğrulama ve API yönlendirmeyi etkinleştirin:

https://localhost:{identityPort}/connect/token

https://localhost:{gatewayPort}/catalog

https://localhost:{gatewayPort}/product

🧪 Test & Swagger
Tüm servisler Swagger arayüzü ile test edilebilir. Identity Server üzerinden token alındıktan sonra gateway üzerinden yetkili istekler yapılabilir.

📘 Loosely Coupled Nedir?
Loosely Coupled (Türkçesi: Gevşek Bağlılık), yazılımda bileşenlerin birbirine olabildiğince az bağımlı olmasıdır. Bu yaklaşım sistemin daha esnek, test edilebilir ve sürdürülebilir olmasını sağlar.

🎯 Avantajları:
Modüller birbirinden bağımsız geliştirilebilir

Hatalar izole edilip kolayca çözülebilir

Test edilebilirlik artar

Kod tekrar kullanılabilir hale gelir

Daha sürdürülebilir sistemler kurulabilir

💡 Örnek (C#)
// Sıkı bağlı (strongly coupled)
var urunService = new UrunService();
var kategori = urunService.GetKategori(5);

// Gevşek bağlı (loosely coupled)
public interface IKategoriService
{
    KategoriDto GetKategori(int id);
}

public class UrunManager
{
    private readonly IKategoriService _kategoriService;

    public UrunManager(IKategoriService kategoriService)
    {
        _kategoriService = kategoriService;
    }
}
Bu projede servisler arası geçişler, Ocelot API Gateway ve arayüzler (interface) üzerinden yapıldığı için sistem loosely coupled bir yapıya sahiptir.

📸 Ekran Görüntüleri
(Swagger sayfaları, gateway istek yönlendirme örnekleri ve Postman çıktıları burada yer alabilir.)

👨‍💻 Geliştirici
Cesur Alphan Ellik
LinkedIn | GitHub

📌 Not
Bu proje öğrenim ve portföy amaçlı geliştirilmiştir. Mikroservis mimarisi, API Gateway ve kimlik doğrulama mekanizmalarının bir arada uygulanmasına örnek teşkil etmektedir.



Düzenlendi
İstediğin gibi başlık ve bölümler artık GitHub README formatına uygun şekilde ##, - ve kod bloklarıyla yapılandırıldı ✅

Artık profesyonel görünüyor ve GitHub'da çok daha okunabilir. İstersen şimdi:

ocelot.json yapılandırmasını örnekle ekleyebilirim

veya ekran görüntüsü yerleştirmek için Markdown formatı gösterebilirim

Hangisiyle devam edelim?





Şunu dedin:
## su formate githubda redme olucak sekilde


ChatGPT:
Harika Cesur! Aşağıda, doğrudan GitHub’da kullanabileceğin şekilde, senin verdiğin içeriği tam anlamıyla README formatında ve ## başlıklarla uyumlu olarak düzenledim. Bunu kopyalayıp README.md dosyana yapıştırabilirsin:

markdown
Kopyala
Düzenle
## 🛒 MultiShop - Microservice Based E-Commerce Backend

MultiShop, .NET teknolojileriyle geliştirilmiş mikro servis mimarisine sahip bir e-ticaret uygulamasıdır.  
Ocelot API Gateway ve Identity Server kullanılarak kimlik doğrulama ve servisler arası geçiş yönetilirken, her modül bağımsız olarak yapılandırılmıştır.  
Proje, servislerin **loosely coupled (gevşek bağlı)** olması prensibiyle geliştirilmiştir.

---

## 🚀 Temel Özellikler

- Microservice tabanlı yapı
- Ocelot ile API Gateway yönetimi
- Identity Server ile kimlik doğrulama ve yetkilendirme
- Her servis bağımsız katmanlı mimariye sahip (Controller, Business, DataAccess)
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

---

## 📁 Proje Yapısı

MultiShop
│
├── Services
│ ├── MultiShop.Catalog → Ürün listeleme servisi
│ ├── MultiShop.Product → Ürün işlemleri
│ ├── MultiShop.Category → Kategori işlemleri
│ └── MultiShop.Detail → Ürün detay servisi
│
├── Gateway
│ └── MultiShop.Gateway → Ocelot API Gateway yapılandırması
│
├── Identity
│ └── MultiShop.Identity → Identity Server kimlik doğrulama ve yetkilendirme
│
└── MultiShop.WebAPI → Ortak API endpoint sunumu

---

## 👨‍💻 Geliştirici

**Cesur Alphan Ellik**  
[LinkedIn](https://www.linkedin.com/in/cesur-alphan-ellik-b0056a240/)  
[GitHub](https://github.com/Melanocetuss)

---

