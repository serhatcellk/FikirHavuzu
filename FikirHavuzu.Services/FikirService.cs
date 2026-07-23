using FikirHavuzu.Entities;
using FikirHavuzu.Repositories;

namespace FikirHavuzu.Services;

public class FikirService : IFikirService
{
    private readonly IFikirRepository _repository;
    private readonly IKullaniciRepository _kullaniciRepository;

    public FikirService(IFikirRepository repository, IKullaniciRepository kullaniciRepository)
    {
        _repository = repository;
        _kullaniciRepository = kullaniciRepository;
    }
    public async Task<List<Fikir>> HepsiniGetirAsync()
        => await _repository.HepsiniGetirAsync();

    public async Task<List<Fikir>> DurumaGoreGetirAsync(FikirDurumu durum)
    {
        var hepsi = await _repository.HepsiniGetirAsync();
        return hepsi.Where(f => f.Durum == durum).ToList();
    }

    public async Task<Fikir?> IdIleGetirAsync(int id)
        => await _repository.IdIleGetirAsync(id);

    public async Task<bool> EkleAsync(Fikir fikir)
    {
        fikir.Durum = FikirDurumu.Beklemede;
        fikir.OlusturmaTarihi = DateTime.Now;

        await _repository.EkleAsync(fikir);
        return await _repository.KaydetAsync();
    }

    public async Task<bool> DegerlendirAsync(int id, FikirDurumu durum, string? aciklama, int? puan)
    {
        var fikir = await _repository.IdIleGetirAsync(id);
        if (fikir is null) return false;

        // Puan karar verir: 70+ Kabul, altı Red
        if (puan.HasValue && puan.Value >= 70)
            fikir.Durum = FikirDurumu.Kabul;
        else
            fikir.Durum = FikirDurumu.Red;

        fikir.DegerlendirmeAciklamasi = aciklama;
        fikir.Puan = puan;

        // 70+ ve daha önce ödül verilmediyse → sahibe 100 puan
        if (puan.HasValue && puan.Value >= 70 && !fikir.OdulVerildi)
        {
            var sahip = await _kullaniciRepository.IdIleGetirAsync(fikir.KullaniciId);
            if (sahip is not null)
            {
                sahip.OdulPuani += 100;
                _kullaniciRepository.Guncelle(sahip);
            }
            fikir.OdulVerildi = true;
        }

        _repository.Guncelle(fikir);
        return await _repository.KaydetAsync();
    }
}