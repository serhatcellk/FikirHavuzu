using FikirHavuzu.Entities;

namespace FikirHavuzu.Services;

public interface IKullaniciService
{
    Task<List<Kullanici>> HepsiniGetirAsync();
    Task<Kullanici?> IdIleGetirAsync(int id);
    Task<bool> EkleAsync(Kullanici kullanici, string sifre);
    Task<bool> GuncelleAsync(Kullanici kullanici);
    Task<bool> PasifeAlAsync(int id);
    Task<bool> AktifeAlAsync(int id);
    Task<Kullanici?> GirisDogrulaAsync(string mail, string sifre);
    
}