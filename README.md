# Fikir Havuzu Uygulaması

Çalışanların fikir ve önerilerini paylaşabildiği, yetkili kullanıcıların bu önerileri değerlendirdiği ve başarılı bulunan fikirlerin sahiplerinin ödüllendirildiği web tabanlı bir öneri yönetim sistemidir.

**Proje Kodu:** PRJIC20200901
**Kurum:** TRtek Yazılım

---

## Kullanılan Teknolojiler

- ASP.NET Core MVC (Razor)
- Entity Framework Core (Code First)
- Microsoft SQL Server
- Bootstrap 5
- N-katmanlı mimari

---

## Mimari

Proje, sorumlulukların ayrılması ilkesine uygun olarak dört katmandan oluşur:

```
FikirHavuzu.Web           → Sunum katmanı (Controller, View)
    ↓
FikirHavuzu.Services      → İş mantığı katmanı
    ↓
FikirHavuzu.Repositories  → Veri erişim katmanı (DbContext, Repository)
    ↓
FikirHavuzu.Entities      → Varlık modelleri
```

Katmanlar arası bağımlılıklar interface'ler üzerinden kurulmuş, bağımlılık enjeksiyonu (Dependency Injection) ile yönetilmiştir.

---

## Modüller

### 1. Kullanıcı Yönetimi
- Kullanıcı ekleme (Ad, Soyad, Mail, Telefon, Sicil No, TC Kimlik No)
- Kullanıcı bilgilerini güncelleme
- Kullanıcıyı pasife alma / aktife alma
- Kullanıcı listeleme ve arama (isim veya mail ile filtreleme)

Pasife alınan kullanıcı sisteme giriş yapamaz; giriş denemesinde bilgilendirme mesajı gösterilir.

### 2. Fikir / Öneri Yönetimi
- Fikir ekleme (öneri sahibi, giriş yapan kullanıcıdan otomatik atanır)
- Fikir listeleme
- Duruma göre filtreleme (Beklemede / Kabul / Red)
- Fikir değerlendirme (puan ve açıklama)

### 3. Yetki Yönetimi
- Sistemde tanımlı roller: **Admin**, **Degerlendirici**, **Kullanici**
- Kullanıcılara rol atama ve kaldırma
- Yeni eklenen kullanıcıya otomatik olarak "Kullanici" rolü verilir

### 4. Ödül ve Liderlik Sistemi
- Değerlendirmede **70 ve üzeri** puan alan fikir otomatik olarak **kabul** edilir
- Kabul edilen fikrin sahibine **100 ödül puanı** eklenir
- Aynı fikir tekrar değerlendirilse dahi ödül yalnızca bir kez verilir
- Liderlik tablosu, kullanıcıları toplam ödül puanına göre sıralar

---

## Yetkilendirme

Kimlik doğrulama, hazır bir kütüphane kullanılmadan sıfırdan geliştirilmiştir.

- **Şifre güvenliği:** Her kullanıcı için `HMACSHA512` ile ayrı bir salt üretilir, şifre bu salt kullanılarak hash'lenir. Şifreler veritabanında düz metin olarak tutulmaz.
- **Oturum yönetimi:** ASP.NET Core'un yerleşik cookie authentication altyapısı kullanılır. Kullanıcının kimlik ve rol bilgileri giriş anında claim olarak yazılır.
- **Erişim kontrolü:** Controller ve action seviyesinde rol bazlı kısıtlama uygulanır.

### Rol Yetki Matrisi

| İşlem | Admin | Degerlendirici | Kullanici |
|---|:---:|:---:|:---:|
| Kullanıcı yönetimi (ekle/güncelle/pasife al/listele) | ✓ | — | — |
| Yetki yönetimi (rol atama/kaldırma) | ✓ | — | — |
| Fikir ekleme ve listeleme | ✓ | ✓ | ✓ |
| Fikir değerlendirme | ✓ | ✓ | — |
| Liderlik tablosu görüntüleme | ✓ | ✓ | ✓ |

Yetkisi olmayan bir sayfaya erişim denendiğinde kullanıcı bilgilendirme sayfasına yönlendirilir.

---

## Veritabanı Şeması

| Tablo | Açıklama |
|---|---|
| `Kullanicilar` | Kullanıcı bilgileri, şifre hash/salt, aktiflik durumu, ödül puanı |
| `Fikirler` | Fikir başlığı, açıklaması, sahibi, durumu, puanı, değerlendirme notu |
| `Yetkiler` | Sistemde tanımlı roller |
| `KullaniciYetkileri` | Kullanıcı–rol ilişkisi (çoktan çoğa ara tablo) |

**İlişkiler:**
- `Kullanicilar` → `Fikirler` : bire-çok (bir kullanıcının birden fazla fikri olabilir)
- `Kullanicilar` ↔ `Yetkiler` : çoktan-çoğa (`KullaniciYetkileri` ara tablosu üzerinden)

---

## Kurulum

### Gereksinimler
- .NET SDK
- Microsoft SQL Server
- Entity Framework Core CLI (`dotnet tool install --global dotnet-ef`)

### Adımlar

**1. Projeyi klonlayın**
```bash
git clone <repo-adresi>
cd FikirHavuzu
```

**2. Bağlantı ayarını yapın**

`FikirHavuzu.Web/appsettings.json` dosyasındaki bağlantı dizesini kendi SQL Server örneğinize göre düzenleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FikirHavuzuDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Veritabanını oluşturun**
```bash
cd FikirHavuzu.Web
dotnet ef database update --project ../FikirHavuzu.Repositories --startup-project .
```

Migration çalıştırıldığında `Admin`, `Degerlendirici` ve `Kullanici` rolleri otomatik olarak eklenir.

**4. Uygulamayı çalıştırın**
```bash
dotnet run
```

Uygulama varsayılan olarak `http://localhost:5292` adresinde çalışır.

---

## Demo Giriş Bilgileri

| Rol | Mail | Şifre |
|---|---|---|
| Admin | `serhat@fikirhavuzu.com` |'Sifre123' |
| Degerlendirici | `degerlendirici@fikirhavuzu.com` | 'Sifre123' |
| Kullanici | `kullanici@fikirhavuzu.com` |'Sifre123'  |

> Bu hesaplar yalnızca test amaçlıdır.

---

## Demo Veri

`FikirHavuzu_DemoVeri.sql` dosyası, sistemi dolu bir şekilde görüntülemek için hazırlanmıştır. Script şunları ekler:

- 47 kullanıcı (4'ü Degerlendirici rolünde)
- 40 fikir (kabul edilmiş, reddedilmiş ve değerlendirilmeyi bekleyen)
- Kabul edilen fikirlere göre hesaplanmış ödül puanları

Script SSMS üzerinden veya `sqlcmd` ile çalıştırılabilir.

> Not: Script ile eklenen kullanıcılar şifre hash'i içermediği için sisteme giriş yapamaz. Bu kayıtlar listeleme, fikir sahipliği ve liderlik tablosu gösterimi içindir.

---

## Geliştirici

**Serhat Çelik**
Kocaeli Üniversitesi — Elektronik ve Haberleşme Mühendisliği
