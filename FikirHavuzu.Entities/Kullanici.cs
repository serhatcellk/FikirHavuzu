namespace FikirHavuzu.Entities
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string MailAdresi { get; set; }
        public string TelefonNumarasi { get; set; }
        public string SicilNumarasi { get; set; }
        public string TCKimlikNumarasi { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }
        public bool AktifMi { get; set; } = true;
        public ICollection<KullaniciYetki> KullaniciYetkileri { get; set; } = new List<KullaniciYetki>();
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public int OdulPuani { get; set; } = 0;
    }
}