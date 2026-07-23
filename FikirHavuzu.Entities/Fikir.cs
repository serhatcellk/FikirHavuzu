namespace FikirHavuzu.Entities
{
    public class Fikir
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public bool OdulVerildi { get; set; } = false;

        // Fikri giren kullanıcı
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        // Değerlendirme
        public FikirDurumu Durum { get; set; } = FikirDurumu.Beklemede;
        public string? DegerlendirmeAciklamasi { get; set; }
        public int? Puan { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }

    public enum FikirDurumu
    {
        Beklemede = 0,
        Kabul = 1,
        Red = 2,
        Uygulandi = 3
    }
    
}