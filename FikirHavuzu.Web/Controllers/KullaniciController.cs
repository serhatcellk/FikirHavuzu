using Microsoft.AspNetCore.Authorization;
using FikirHavuzu.Entities;
using FikirHavuzu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers;

[Authorize(Roles = "Admin")]

public class KullaniciController : Controller
{
    private readonly IKullaniciService _kullaniciService;

    public KullaniciController(IKullaniciService kullaniciService)
    {
        _kullaniciService = kullaniciService;
    }

    public async Task<IActionResult> Index(string? arama)
    {
        var kullanicilar = await _kullaniciService.HepsiniGetirAsync();

        if (!string.IsNullOrWhiteSpace(arama))
        {
            arama = arama.Trim().ToLower();
            kullanicilar = kullanicilar
                .Where(k => k.Ad.ToLower().Contains(arama)
                         || k.Soyad.ToLower().Contains(arama)
                         || k.MailAdresi.ToLower().Contains(arama))
                .ToList();
        }

        ViewBag.Arama = arama;
        return View(kullanicilar);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Ekle()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Ekle(Kullanici kullanici, string sifre)
    {
        ModelState.Remove(nameof(Kullanici.SifreHash));
        ModelState.Remove(nameof(Kullanici.SifreSalt));

        if (!ModelState.IsValid)
            return View(kullanici);

        await _kullaniciService.EkleAsync(kullanici, sifre);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Duzenle(int id)
    {
        var kullanici = await _kullaniciService.IdIleGetirAsync(id);
        if (kullanici is null) return RedirectToAction(nameof(Index));
        return View(kullanici);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Duzenle(Kullanici kullanici)
    {
        ModelState.Remove(nameof(Kullanici.SifreHash));
        ModelState.Remove(nameof(Kullanici.SifreSalt));

        if (!ModelState.IsValid)
            return View(kullanici);

        await _kullaniciService.GuncelleAsync(kullanici);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PasifeAl(int id)
    {
        await _kullaniciService.PasifeAlAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AktifeAl(int id)
    {
        await _kullaniciService.AktifeAlAsync(id);
        return RedirectToAction(nameof(Index));
    }
}