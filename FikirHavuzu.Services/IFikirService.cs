using FikirHavuzu.Entities;

namespace FikirHavuzu.Services;

public interface IFikirService
{
    Task<List<Fikir>> HepsiniGetirAsync();
    Task<List<Fikir>> DurumaGoreGetirAsync(FikirDurumu durum);
    Task<Fikir?> IdIleGetirAsync(int id);
    Task<bool> EkleAsync(Fikir fikir);
    Task<bool> DegerlendirAsync(int id, FikirDurumu durum, string? aciklama, int? puan);
}