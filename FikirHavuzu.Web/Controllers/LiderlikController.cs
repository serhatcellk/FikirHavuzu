using FikirHavuzu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers;

[Authorize]
public class LiderlikController : Controller
{
    private readonly IKullaniciService _kullaniciService;

    public LiderlikController(IKullaniciService kullaniciService)
    {
        _kullaniciService = kullaniciService;
    }

    public async Task<IActionResult> Index()
    {
        var kullanicilar = await _kullaniciService.HepsiniGetirAsync();
        var siralama = kullanicilar
            .OrderByDescending(k => k.OdulPuani)
            .ToList();
        return View(siralama);
    }
}