using FikirHavuzu.Entities;

namespace FikirHavuzu.Services;

public interface IYetkiService
{
    Task<List<Yetki>> HepsiniGetirAsync();
    Task<bool> EkleAsync(Yetki yetki);

    Task<List<KullaniciYetki>> KullaniciYetkileriniGetirAsync(int kullaniciId);
    Task<bool> YetkiAtaAsync(int kullaniciId, int yetkiId);
    Task<bool> YetkiKaldirAsync(int kullaniciId, int yetkiId);
}