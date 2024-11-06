# HastaneYonetimSistemiApp
Hastane Yönetim Sistemi, hastane süreçlerini yönetmek için geliştirilmiş bir uygulamadır. Bu uygulama, hastanedeki hasta, doktor, randevu ve diğer yönetimsel bilgilerin merkezi bir sistemde toplanmasını ve yönetilmesini sağlar.

## 🗂️ Proje Yapısı
Bu proje üç ana katmandan oluşmaktadır:

- HastaneYonetimSistemiApp.Data: Veritabanı işlemlerinin yapıldığı katmandır. Entity Framework Core kullanılarak veri modelleri ve veritabanı bağlantısı bu katmanda yönetilir.
- HastaneYonetimSistemiApp.Business: İş mantığının tanımlandığı katmandır. İş kuralları ve veritabanı işlemlerinin koordinasyonu bu katmanda yapılır.
- HastaneYonetimSistemiApp.WebApi: API katmanıdır. Hastane yönetim sistemi için gerekli API servisleri bu katmanda sunulur.

## 🚀 Kurulum ve Çalıştırma
Projeyi çalıştırmak için aşağıdaki adımları takip edin:

1. Depoyu Klonlayın

git clone https://github.com/kullaniciadi/HastaneYonetimSistemiApp.git
cd HastaneYonetimSistemiApp

2. Gerekli Paketleri Yükleyin
Visual Studio veya .NET CLI kullanarak projenin gerektirdiği NuGet paketlerini yükleyin:

dotnet restore

3. Veritabanı Ayarlarını Yapılandırın
appsettings.json dosyasını açarak veritabanı bağlantı bilgilerini düzenleyin.

4. Veritabanını Güncelleyin
Entity Framework Migrations ile veritabanını oluşturun ve güncelleyin:

dotnet ef database update

5. Uygulamayı Çalıştırın

dotnet run --project HastaneYonetimSistemiApp.WebApi

## 💻 Kullanım
Projeyi çalıştırdıktan sonra Swagger UI veya Postman kullanarak API'ye erişebilirsiniz.

## 🛠️ Kullanılan Teknolojiler
- .NET 6
- Entity Framework Core
- SQL Server
- Swagger

## Özellikler
- 🏥 Hasta Yönetimi: Hasta kayıtlarının yapılması, güncellenmesi ve listelenmesi.
- 👨‍⚕️ Doktor Yönetimi: Doktorların eklenmesi, güncellenmesi ve listelenmesi.
- 📅 Randevu Yönetimi: Hasta ve doktorlar için randevu oluşturulması ve yönetilmesi.
- 🔒 Yetkilendirme ve Kimlik Doğrulama: Roller bazında yetkilendirme (örneğin, Admin rolü).
