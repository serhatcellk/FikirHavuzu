using FikirHavuzu.Entities;

namespace FikirHavuzu.Repositories;

public interface IKullaniciRepository
{
    Task<List<Kullanici>> HepsiniGetirAsync();
    Task<Kullanici?> IdIleGetirAsync(int id);
    Task EkleAsync(Kullanici kullanici);
    void Guncelle(Kullanici kullanici);
    Task<bool> KaydetAsync();
}