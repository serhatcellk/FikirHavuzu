namespace FikirHavuzu.Entities
{
    public class Yetki
    {
        public int Id { get; set; }
        public string Ad { get; set; }   

        public ICollection<KullaniciYetki> KullaniciYetkileri { get; set; } = new List<KullaniciYetki>();
    }
}