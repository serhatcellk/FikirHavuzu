using FikirHavuzu.Entities;

namespace FikirHavuzu.Repositories;

public interface IYetkiRepository
{
    Task<List<Yetki>> HepsiniGetirAsync();
    Task<Yetki?> IdIleGetirAsync(int id);
    Task EkleAsync(Yetki yetki);

    Task<List<KullaniciYetki>> KullaniciYetkileriniGetirAsync(int kullaniciId);
    Task YetkiAtaAsync(int kullaniciId, int yetkiId);
    Task YetkiKaldirAsync(int kullaniciId, int yetkiId);
    Task<bool> AtamaVarMiAsync(int kullaniciId, int yetkiId);

    Task<bool> KaydetAsync();
}