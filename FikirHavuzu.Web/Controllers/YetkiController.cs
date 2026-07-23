using Microsoft.AspNetCore.Authorization;
using FikirHavuzu.Entities;
using FikirHavuzu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers;
[Authorize(Roles = "Admin")]
public class YetkiController : Controller
{
    private readonly IYetkiService _yetkiService;
    private readonly IKullaniciService _kullaniciService;

    public YetkiController(IYetkiService yetkiService, IKullaniciService kullaniciService)
    {
        _yetkiService = yetkiService;
        _kullaniciService = kullaniciService;
    }

    public async Task<IActionResult> Index()
    {
        var yetkiler = await _yetkiService.HepsiniGetirAsync();
        return View(yetkiler);
    }

    [HttpPost]
    public async Task<IActionResult> Ekle(string ad)
    {
        if (!string.IsNullOrWhiteSpace(ad))
            await _yetkiService.EkleAsync(new Yetki { Ad = ad });

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Ata(int kullaniciId)
    {
        var kullanici = await _kullaniciService.IdIleGetirAsync(kullaniciId);
        if (kullanici is null) return RedirectToAction("Index", "Kullanici");

        ViewBag.Kullanici = kullanici;
        ViewBag.TumYetkiler = await _yetkiService.HepsiniGetirAsync();
        ViewBag.MevcutYetkiler = await _yetkiService.KullaniciYetkileriniGetirAsync(kullaniciId);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> YetkiAta(int kullaniciId, int yetkiId)
    {
        await _yetkiService.YetkiAtaAsync(kullaniciId, yetkiId);
        return RedirectToAction(nameof(Ata), new { kullaniciId });
    }

    [HttpPost]
    public async Task<IActionResult> YetkiKaldir(int kullaniciId, int yetkiId)
    {
        await _yetkiService.YetkiKaldirAsync(kullaniciId, yetkiId);
        return RedirectToAction(nameof(Ata), new { kullaniciId });
    }
}