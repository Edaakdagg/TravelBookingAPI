TravelBooking API

Bu proje, .NET 9 teknolojisi kullanılarak  Clean Architecture  (Temiz Mimari) prensiplerine uygun olarak geliştirilmiş bir seyahat rezervasyon sistemidir.

Projeyi Çalıştırma Yönergesi

Projeyi yerel bilgisayarınızda test etmek için aşağıdaki adımları sırasıyla takip edebilirsiniz.

1. Projeyi Derleme (Build Verification)
Projenin tüm bağımlılıklarının doğru yüklendiğini ve hatasız derlendiğini doğrulamak için terminale şu komutu yazın:


dotnet build

*Bu komut başarıyla tamamlandığında "Build succeeded" mesajını görmelisiniz.*

 2. Uygulamayı Başlatma (Run Verification)
Uygulamayı ana dizin üzerinden aşağıdaki komutla çalıştırabilirsiniz:


dotnet run --project TravelBooking.Api


Başarılı Çalışma Çıktısı (Expected Output):
Uygulama ayağa kalktığında terminalinizde şu logları görmelisiniz:


info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5092
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.


---

 Olası Hatalar ve Çözümleri
Port Meşgul Hatası (Address already in use)
Uygulamayı başlatırken `address already in use` hatası alıyorsanız, bu durum **5092** portunun arka planda çalışan başka bir işlem tarafından kapatılmadığını gösterir.

**Çözüm (macOS/Linux):**
Terminalden şu komutu kullanarak ilgili portu temizleyebilirsiniz:

kill -9 $(lsof -ti:5092)


---

 Mimari Yapı
Proje aşağıdaki katmanlardan oluşmaktadır:
TravelBooking.Api: Uygulamanın giriş noktası.
TravelBooking.Application: İş mantığı ve servisler.
TravelBooking.Domain: Temel varlıklar.
TravelBooking.Infrastructure:Veritabanı ve dış servisler.


