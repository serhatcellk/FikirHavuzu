using Microsoft.AspNetCore.Authorization;
using FikirHavuzu.Entities;
using FikirHavuzu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers;

[Authorize]
public class FikirController : Controller
{
    private readonly IFikirService _fikirService;
    private readonly IKullaniciService _kullaniciService;

    public FikirController(IFikirService fikirService, IKullaniciService kullaniciService)
    {
        _fikirService = fikirService;
        _kullaniciService = kullaniciService;
    }

    // Listeleme + filtreleme
    public async Task<IActionResult> Index(FikirDurumu? durum)
    {
        var fikirler = durum.HasValue
            ? await _fikirService.DurumaGoreGetirAsync(durum.Value)
            : await _fikirService.HepsiniGetirAsync();

        ViewBag.SeciliDurum = durum;
        return View(fikirler);
    }
    // Ekleme formu (GET)
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Ekle(Fikir fikir)
    {
        ModelState.Remove(nameof(Fikir.Kullanici));
        ModelState.Remove(nameof(Fikir.KullaniciId));

        if (!ModelState.IsValid)
            return View(fikir);

        //  giris yapan kullanic,
        var kullaniciId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        fikir.KullaniciId = kullaniciId;

        await _fikirService.EkleAsync(fikir);
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "Admin,Degerlendirici")]
    public async Task<IActionResult> Degerlendir(int id)
    {
        var fikir = await _fikirService.IdIleGetirAsync(id);
        if (fikir is null) return RedirectToAction(nameof(Index));
        return View(fikir);
    }

    [Authorize(Roles = "Admin,Degerlendirici")]
    [HttpPost]
    public async Task<IActionResult> Degerlendir(int id, FikirDurumu durum, string? aciklama, int? puan)
    {
        await _fikirService.DegerlendirAsync(id, durum, aciklama, puan);
        return RedirectToAction(nameof(Index));
    }
}