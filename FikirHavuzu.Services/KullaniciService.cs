using System.Security.Cryptography;
using System.Text;
using FikirHavuzu.Entities;
using FikirHavuzu.Repositories;

namespace FikirHavuzu.Services;

public class KullaniciService : IKullaniciService
{
    private readonly IKullaniciRepository _repository;
    private readonly IYetkiRepository _yetkiRepository;

    public KullaniciService(IKullaniciRepository repository, IYetkiRepository yetkiRepository)
    {
        _repository = repository;
        _yetkiRepository = yetkiRepository;
    }

    public async Task<List<Kullanici>> HepsiniGetirAsync()
        => await _repository.HepsiniGetirAsync();

    public async Task<Kullanici?> IdIleGetirAsync(int id)
        => await _repository.IdIleGetirAsync(id);

    public async Task<bool> EkleAsync(Kullanici kullanici, string sifre)
{
    using var hmac = new HMACSHA512();
    kullanici.SifreSalt = hmac.Key;
    kullanici.SifreHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));
    kullanici.AktifMi = true;
    kullanici.KayitTarihi = DateTime.Now;

    await _repository.EkleAsync(kullanici);
    var eklendi = await _repository.KaydetAsync();
    if (!eklendi) return false;
    await _yetkiRepository.YetkiAtaAsync(kullanici.Id, 3);
    return await _yetkiRepository.KaydetAsync();
}

    public async Task<bool> GuncelleAsync(Kullanici kullanici)
{
    var mevcut = await _repository.IdIleGetirAsync(kullanici.Id);
    if (mevcut is null) return false;

    mevcut.Ad = kullanici.Ad;
    mevcut.Soyad = kullanici.Soyad;
    mevcut.MailAdresi = kullanici.MailAdresi;
    mevcut.TelefonNumarasi = kullanici.TelefonNumarasi;
    mevcut.SicilNumarasi = kullanici.SicilNumarasi;
    mevcut.TCKimlikNumarasi = kullanici.TCKimlikNumarasi;

    _repository.Guncelle(mevcut);
    return await _repository.KaydetAsync();
}

    public async Task<bool> PasifeAlAsync(int id)
    {
        var kullanici = await _repository.IdIleGetirAsync(id);
        if (kullanici is null) return false;

        kullanici.AktifMi = false;
        _repository.Guncelle(kullanici);
        return await _repository.KaydetAsync();
    }
    public async Task<bool> AktifeAlAsync(int id)
{
    var kullanici = await _repository.IdIleGetirAsync(id);
    if (kullanici is null) return false;

    kullanici.AktifMi = true;
    _repository.Guncelle(kullanici);
    return await _repository.KaydetAsync();
}
public async Task<Kullanici?> GirisDogrulaAsync(string mail, string sifre)
{
    var kullanicilar = await _repository.HepsiniGetirAsync();
    var kullanici = kullanicilar.FirstOrDefault(k => k.MailAdresi == mail);

    if (kullanici is null) return null;

    using var hmac = new HMACSHA512(kullanici.SifreSalt);
    var hesaplananHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));

    if (!hesaplananHash.SequenceEqual(kullanici.SifreHash))
        return null;

    return kullanici; 
}

}