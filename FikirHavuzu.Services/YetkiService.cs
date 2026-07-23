using FikirHavuzu.Entities;
using FikirHavuzu.Repositories;

namespace FikirHavuzu.Services;

public class YetkiService : IYetkiService
{
    private readonly IYetkiRepository _repository;

    public YetkiService(IYetkiRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Yetki>> HepsiniGetirAsync()
        => await _repository.HepsiniGetirAsync();

    public async Task<bool> EkleAsync(Yetki yetki)
    {
        await _repository.EkleAsync(yetki);
        return await _repository.KaydetAsync();
    }

    public async Task<List<KullaniciYetki>> KullaniciYetkileriniGetirAsync(int kullaniciId)
        => await _repository.KullaniciYetkileriniGetirAsync(kullaniciId);

    public async Task<bool> YetkiAtaAsync(int kullaniciId, int yetkiId)
    {
        // Aynı yetki zaten atanmışsa tekrar ekleme
        if (await _repository.AtamaVarMiAsync(kullaniciId, yetkiId))
            return false;

        await _repository.YetkiAtaAsync(kullaniciId, yetkiId);
        return await _repository.KaydetAsync();
    }

    public async Task<bool> YetkiKaldirAsync(int kullaniciId, int yetkiId)
    {
        await _repository.YetkiKaldirAsync(kullaniciId, yetkiId);
        return await _repository.KaydetAsync();
    }
}