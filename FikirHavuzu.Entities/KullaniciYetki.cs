namespace FikirHavuzu.Entities
{
    public class KullaniciYetki
    {
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public int YetkiId { get; set; }
        public Yetki Yetki { get; set; }
    }
}