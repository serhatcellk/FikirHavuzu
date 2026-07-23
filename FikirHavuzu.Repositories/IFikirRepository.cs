using FikirHavuzu.Entities;

namespace FikirHavuzu.Repositories;

public interface IFikirRepository
{
    Task<List<Fikir>> HepsiniGetirAsync();
    Task<Fikir?> IdIleGetirAsync(int id);
    Task EkleAsync(Fikir fikir);
    void Guncelle(Fikir fikir);
    Task<bool> KaydetAsync();
}